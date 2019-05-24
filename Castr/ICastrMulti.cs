using System;
using System.Collections.Generic;
using System.Text;

namespace Castr
{
    public interface ICastrMulti
    {
        IEnumerable<T> CastAsClassMulti<T>();
        IEnumerable<T> CastAsStructMulti<T>();
    }
}
