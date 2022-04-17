using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Castr
{
    public interface ICSV
    {
        public string CastAsCSV<T>(IEnumerable<T> toConvert)
            where T : class;
    }
}
