using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Castr.Conversion
{
    public static class ConverterFactory
    {
        public static IConverter GetConverter(Type propertyType)
        {
            if (typeof(DateTime).IsAssignableFrom(propertyType))
            {
                return new DateConverter();
            }
            else if (typeof(bool).IsAssignableFrom(propertyType))
            {
                return new BoolConverter();
            }
            else
            {
                return new DefaultConverter();
            }
        }
    }
}
