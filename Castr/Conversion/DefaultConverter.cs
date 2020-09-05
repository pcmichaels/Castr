using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Castr.Conversion
{
    public class DefaultConverter : IConverter
    {
        public object Convert(string value, Type type)
        {
            return System.Convert.ChangeType(value, type);
        }

        public object Convert(string value, CultureInfo culture, Type type)
        {
            return Convert(value, type);
        }
    }
}
