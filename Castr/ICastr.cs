using System;
using System.Collections.Generic;
using System.Text;

namespace Castr
{
    public interface ICastr
    {
        T CastAsClass<T>();
        T CastAsStruct<T>();
    }
}
