using System;
using System.Collections.Generic;
using System.Text;

namespace Castr
{
    public interface ICastrMulti
    {
        IEnumerable<T> CastAsClassMulti<T>() where T: class;
        IEnumerable<T> CastAsStructMulti<T>() where T: struct;
    }
}
