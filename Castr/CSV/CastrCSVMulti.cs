using Castr.Exceptions;
using Castr.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Castr.CSV
{
    public class CastrCSVMulti : CastrCSVBase, ICastrMulti, IDisposable
    {
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

    }
}
