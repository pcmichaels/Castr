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

        public IEnumerable<T> CastAsClassMulti<T>()
        {
            throw new NotImplementedException();
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

        public IEnumerable<T> CastAsStructMulti<T>()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _data = null;
        }

    }
}
