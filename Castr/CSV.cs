using Castr.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CastDataAs
{
    public class CSV : ICastr, IDisposable
    {
        private string _csv;
        private string _delimiter;
        private readonly bool _includesHeaders;
        private string[] _headers = null;
        private List<string[]> _data = null;
        private string[] _newLineDelimiter = new [] { Environment.NewLine };

        public CSV(string csv, string delimiter) : this(csv, delimiter, false) { }

        public CSV(string csv, string delimiter, bool includesHeaders)
        {
            _csv = csv;
            _delimiter = delimiter;            
            _includesHeaders = includesHeaders;
        }

        private List<string[]> SplitFile()
        {
            _data = new List<string[]>();

            string[] lines = _csv.Split(_newLineDelimiter, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] fields = line.Split(_delimiter.ToCharArray());

                if (_includesHeaders && _headers == null)
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

            return 0;
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

        private static T CastAsClassSingleInstance<T>(string[] fields)
        {
            var newObject = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();
            int fieldIdx = 0;

            foreach (var prop in properties)
            {
                if (fieldIdx >= fields.Length) break;
                prop.SetValue(newObject, fields[fieldIdx++], null);
            }

            return newObject;
        }

        public T CastAsStruct<T>()
        {
            EnsureFileIsSplit();

            int rowCount = EnsureFileIsSplit();

            if (rowCount != 1)
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
