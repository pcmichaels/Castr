using Castr.Exceptions;
using Castr.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
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

                fieldIdx = AssignValue(fields, newObject, fieldIdx, prop);
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
                fieldIdx = AssignValue(fields, newObject, fieldIdx, prop);
            }

            // Unbox and return
            return (T)newObject;
        }

        private int AssignValue(string[] fields, object newObject, int fieldIdx, System.Reflection.PropertyInfo prop)
        {
            string value = fields[fieldIdx];
            if (typeof(DateTime).IsAssignableFrom(prop.PropertyType))
            {
                DateTime newValue;
                if (DateTime.TryParse(
                    value, _csvOptions.Culture, DateTimeStyles.None, out newValue))
                {
                    prop.SetValue(newObject, newValue, null);
                }
                else
                {
                    if (value.Length == 8)
                    {
                        int year = int.Parse(value.Substring(0, 4));
                        int month = int.Parse(value.Substring(4, 2));
                        int day = int.Parse(value.Substring(6, 2));
                        newValue = new DateTime(year, month, day);
                    }
                    else if (value.Length == 6)
                    {
                        // ToDo: move this into an injected dependency
                        int year = int.Parse($"{DateTime.Now.Year.ToString().Substring(0,2)}{value.Substring(0, 2)}");
                        int month = int.Parse(value.Substring(2, 2));
                        int day = int.Parse(value.Substring(4, 2));
                        newValue = new DateTime(year, month, day);
                    }

                    prop.SetValue(newObject, newValue, null);
                }
            }
            else
            {
                var newValue = Convert.ChangeType(value, prop.PropertyType);
                prop.SetValue(newObject, newValue, null);
            }            

            return ++fieldIdx;
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
                        AssignValue(fields, newObject, i, prop);                        
                        break;
                    }
                }                
            }

            // Unbox and return
            return (T)newObject;
        }

    }
}
