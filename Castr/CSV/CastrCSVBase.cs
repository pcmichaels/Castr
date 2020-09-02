using Castr.Exceptions;
using Castr.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Castr.CSV
{
    public abstract class CastrCSVBase
    {
        protected string _csv;
        protected string[] _headers = null;
        protected List<string[]> _data = null;
        protected string[] _newLineDelimiter = new[] { Environment.NewLine };
        
        protected CsvOptions _csvOptions = new CsvOptions();

        public CastrCSVBase(string csv, CsvOptions csvOptions)
        {
            _csv = csv;
            _csvOptions = csvOptions;
        }


        protected List<string[]> SplitFile()
        {
            _data = new List<string[]>();

            string[] lines = _csv.Split(_newLineDelimiter, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] fields = line.Split(_csvOptions.Delimiter.ToCharArray());

                if (_csvOptions.IncludesHeaders && _headers == null)
                {
                    _headers = fields;
                }
                else
                {
                    _data.Add(fields);
                }
            }

            return _data;
        }

        protected int EnsureFileIsSplit()
        {
            if (_data == null)
            {
                var splitResult = SplitFile();
                return splitResult.Count;
            }

            return _data.Count;
        }

        protected T CastAsClassSingleInstance<T>(string[] fields)
        {
            var newObject = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();
            int fieldIdx = 0;

            foreach (var prop in properties)
            {
                if (fieldIdx >= fields.Length) break;

                if (_csvOptions.StrictHeaderNameMatching && _headers != null
                    && _headers[fieldIdx] != prop.Name)
                {
                    throw new CastingException($"Field {prop.Name} does not match header {_headers[fieldIdx]}");
                }

                prop.SetValue(newObject, fields[fieldIdx++], null);
            }

            if (_csvOptions.StrictHeaderCountMatching
                && _headers != null && fieldIdx < _headers.Length)
            {
                throw new CastingException($"Expected {_headers.Length} fields but found {fieldIdx}");
            }

            return newObject;
        }

        protected T CastAsStructSingleInstance<T>(string[] fields)
        {
            // Have to box the reference first
            var newObject = (object)Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();
            int fieldIdx = 0;

            foreach (var prop in properties)
            {
                if (fieldIdx >= fields.Length) break;
                prop.SetValue(newObject, fields[fieldIdx++], null);
            }

            // Unbox and return
            return (T)newObject;
        }

        protected T CastAsStructSingleInstanceByHeaders<T>(string[] fields, string[] headers)
        {
            if (fields.Length != headers.Length)
            {
                throw new ArgumentException($"Field count ({fields.Length}) must match header count ({headers.Length})");
            }

            // Have to box the reference first
            var newObject = (object)Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();            

            foreach (var prop in properties)
            {
                for (int i = 0; i < headers.Length; i++)
                {                    
                    // Strip spaces from the names before matching
                    string headerName = Regex.Replace(headers[i], @"\s+", "");
                    string propName = Regex.Replace(prop.Name, @"\s+", "");                    

                    if (headerName.Equals(propName, StringComparison.OrdinalIgnoreCase))                                        
                    {
                        prop.SetValue(newObject, fields[i], null);
                        break;
                    }
                }                
            }

            // Unbox and return
            return (T)newObject;
        }

    }
}
