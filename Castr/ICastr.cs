using System;
using System.Collections.Generic;
using System.Text;

namespace CastDataAs
{
    public interface ICastr
    {
        T CastAsClass<T>();
        T CastAsStruct<T>();
    }
}
