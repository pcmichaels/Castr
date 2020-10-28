using System;
using System.Collections.Generic;
using System.Text;

namespace Castr
{
    public interface ICastr
    {
        T CastAsClass<T>() where T : class;
        T CastAsStruct<T>() where T : struct;

        T ExtractField<T>(string name);
    }
}
