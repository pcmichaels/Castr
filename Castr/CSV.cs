using Castr.Exceptions;
using Castr.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CastDataAs
{
    public class CSV : ICastr, IDisposable
    {
        private string _csv;
        private string[] _headers = null;
        private List<string[]> _data = null;
        private string[] _newLineDelimiter = new [] { Environment.NewLine };

        private CsvOptions _csvOptions = new CsvOptions();

        public CSV(string csv, string delimiter) 
            : this(csv, new CsvOptions(delimiter: delimiter)) { }

        public CSV(string csv, string delimiter, bool includesHeaders)
            :this(csv, new CsvOptions(includesHeaders: includesHeaders, delimiter: delimiter))
        { }

        public CSV(string csv, CsvOptions csvOptions)
        {
            _csv = csv;
            _csvOptions = csvOptions;
        }

        private List<string[]> SplitFile()
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

        private int EnsureFileIsSplit()
        {
            if (_data == null)
            {
                var splitResult = SplitFile();
                return splitResult.Count;
            }

            return _data.Count;
        }

        public T CastAsClass<T>()
        {
            int rowCount = EnsureFileIsSplit();

            if (rowCount != 1)
            {
                throw new InvalidSourceDataException("CastAsClass expects a single data row");
            }

            var data = _data.Single();
            return CastAsClassSingleInstance<T>(data);
        }

        private T CastAsClassSingleInstance<T>(string[] fields)
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

        public T CastAsStruct<T>()
        {
            int rowCount = EnsureFileIsSplit();

            if (rowCount == 0)
            {
                throw new InvalidSourceDataException("CastAsStruct expects data");
            }
            else if (rowCount > 1)
            {
                throw new InvalidSourceDataException("CastAsStruct expects a single data row");
            }

            var data = _data.Single();

            return CastAsStructSingleInstance<T>(data);
        }

        private T CastAsStructSingleInstance<T>(string[] fields)
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

        public void Dispose()
        {
            _data = null;
        }

    }
}
