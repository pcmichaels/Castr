using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Castr.Class
{
    public class CastrClassMulti<TExistingClass> : ICastrMulti
    {
        private readonly IEnumerable<TExistingClass> _existingClassList;

        public CastrClassMulti(IEnumerable<TExistingClass> existingClassList)
        {
            _existingClassList = existingClassList;
        }

        public IEnumerable<T> CastAsClassMulti<T>() where T : class
        {
            var resultList = new List<T>();
            foreach (var item in _existingClassList)
            {
                var castr = new CastrClass<TExistingClass>(item);
                var result = castr.CastAsClass<T>();
                resultList.Add(result);
            }

            return resultList;
        }

        public IEnumerable<T> CastAsStructMulti<T>() where T : struct
        {
            var resultList = new List<T>();
            foreach (var item in _existingClassList)
            {
                var castr = new CastrClass<TExistingClass>(item);
                var result = castr.CastAsStruct<T>();
                resultList.Add(result);
            }

            return resultList;

        }

    }
}
