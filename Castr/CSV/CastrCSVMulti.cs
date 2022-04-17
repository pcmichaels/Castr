using Castr.Exceptions;
using Castr.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castr.CSV
{
    public class CastrCSVMulti : CastrCSVBase, ICastrMulti, ICSV, IDisposable
    {
        public CastrCSVMulti(string delimiter, bool includesHeaders)
            : base(new CsvOptions(includesHeaders: includesHeaders, delimiter: delimiter)) { }

        public CastrCSVMulti(string csv, string delimiter) 
            : base(csv, new CsvOptions(delimiter: delimiter)) { }

        public CastrCSVMulti(string csv, string delimiter, bool includesHeaders)
            :base(csv, new CsvOptions(includesHeaders: includesHeaders, delimiter: delimiter))
        { }

        public CastrCSVMulti(string csv, CsvOptions csvOptions)
            : base(csv, csvOptions)
        { }

        public IEnumerable<T> CastAsClassMulti<T>() where T : class
        {
            int rowCount = EnsureFileIsSplit();
            var classList = new List<T>();            

            foreach (var data in _data)
            {
                classList.Add(_csvOptions.MatchByHeader 
                    ? CastAsStructSingleInstanceByHeaders<T>(data, _headers)
                    : CastAsStructSingleInstance<T>(data));
            }

            return classList;
        }

        public IEnumerable<T> CastAsStructMulti<T>() where T : struct
        {
            int rowCount = EnsureFileIsSplit();
            var classList = new List<T>();

            foreach (var data in _data)
            {
                classList.Add(_csvOptions.MatchByHeader
                    ? CastAsStructSingleInstanceByHeaders<T>(data, _headers)
                    : CastAsStructSingleInstance<T>(data));
            }

            return classList;
        }

        public IEnumerable<string[]> GetRawData()
        {
            int rowCount = EnsureFileIsSplit();
            return _data;
        }

        public void Dispose()
        {
            _data = null;
        }

        public string ExtractField(string fieldName, string[] data)
        {
            int? idx = GetHeaderIndex(fieldName);
            if (idx == null || data.Length < idx) return null;

            return data[idx.Value];
        }

        private int? GetHeaderIndex(string fieldName)
        {
            int rowCount = EnsureFileIsSplit();
                       
            int idx = Array.FindIndex(_headers, a => a.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
            if (idx == -1) return null;
            
            return idx;
        }

        public T ExtractField<T>(string fieldName, string[] data)
        {
            string result = ExtractField(fieldName, data);
            var fieldResult = (T)Convert.ChangeType(result, typeof(T));
            return fieldResult;
        }

        public string[] GetOptionsFor(string fieldName)
        {
            int? idx = GetHeaderIndex(fieldName);
            if (idx == null) return null;

            List<string> options = new List<string>();

            foreach (var data in _data)
            {
                if (idx > data.Length) continue;
                var foundData = data[idx.Value];

                if (options.Contains(foundData)) continue;
                options.Add(foundData);
            }

            return options.ToArray();
        }

        public string CastAsCSV<T>(IEnumerable<T> toConvert) where T : class
        {
            var csvOut = new StringBuilder();

            var properties = typeof(T).GetProperties();

            if (_csvOptions.IncludesHeaders)
            {                
                csvOut.AppendLine(string.Join(_csvOptions.Delimiter, 
                    properties.Select(a => a.Name)));
            }

            foreach (var eachClass in toConvert)
            {
                string csvLine = string.Empty;
                foreach (var property in properties)
                {
                    var existingPropertyInfo = typeof(T).GetProperty(property.Name);
                    if (existingPropertyInfo == null || !existingPropertyInfo.CanRead) continue;
                    var value = existingPropertyInfo.GetValue(eachClass);

                    if (csvLine.Length > 0) csvLine += $",{value.ToString()}";
                    else csvLine = value.ToString();
                }

                csvOut.AppendLine(csvLine);
            }

            return csvOut.ToString();
        }
    }
}
