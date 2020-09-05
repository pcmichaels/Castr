using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Castr.Conversion
{
    public interface IConverter
    {
        object Convert(string value, Type type);
        object Convert(string value, CultureInfo culture, Type type);
    }
}
