﻿using Castr.Exceptions;
using Castr.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Castr.CSV
{
    public class CastrCSV : CastrCSVBase, ICastr, IDisposable
    {

        public CastrCSV(string csv, string delimiter) 
            : base(csv, new CsvOptions(delimiter: delimiter)) { }

        public CastrCSV(string csv, string delimiter, bool includesHeaders)
            : base(csv, new CsvOptions(includesHeaders: includesHeaders, delimiter: delimiter))
        { }

        public CastrCSV(string csv, CsvOptions csvOptions)
            : base (csv, csvOptions)
        { }

        public T CastAsClass<T>() where T : class
        {
            int rowCount = EnsureFileIsSplit();

            if (rowCount != 1)
            {
                throw new InvalidSourceDataException("CastAsClass expects a single data row");
            }

            var data = _data.Single();
            return CastAsClassSingleInstance<T>(data);
        }

        public T CastAsStruct<T>() where T : struct
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

        public void Dispose()
        {
            _data = null;
        }

        public T ExtractField<T>(string name)
        {
            if (!_csvOptions.IncludesHeaders)
            {
                throw new ExtractionException("Cannot extract field by name without headers");
            }

            int rowCount = EnsureFileIsSplit();

            var data = _data.Single();
            var index = _headers.TakeWhile(a => !(a == name)).Count();
            return (T)Convert.ChangeType(data[index], typeof(T));
        }

        public Dictionary<string, object> AsDictionary()
        {
            throw new NotImplementedException("Currently unsupported for CSV type");
        }
    }
}
