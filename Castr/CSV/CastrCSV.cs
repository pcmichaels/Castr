using Castr.Exceptions;
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
            return base.CastAsClassSingleInstance<T>(data);
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

            return base.CastAsStructSingleInstance<T>(data);
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

        public Dictionary<string, object> CastAsDictionary()
        {
            if (!_csvOptions.IncludesHeaders)
            {
                throw new ExtractionException("Cannot create dictionary without headers");
            }

            int rowCount = EnsureFileIsSplit();
            if (rowCount != 1)
            {
                throw new InvalidSourceDataException("CastAsDictionary expects a single data row");
            }

            Dictionary<string, object> returnDictionary = new Dictionary<string, object>();

            var data = _data.Single();
            for (int i = 0; i < rowCount; i++)
            {
                returnDictionary.Add(_headers[i], data[i]);
            }
            return returnDictionary;
        }
    }
}
