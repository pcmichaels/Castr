using Castr.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Castr.Conversion
{
    public class BoolConverter : IConverter
    {
        public object Convert(string value, Type type) =>
            Convert(value, CultureInfo.CurrentCulture, type);

        public object Convert(string value, CultureInfo culture, Type type)
        {
            var cleanText = Regex.Replace(value, @"[^0-9a-zA-Z]+", "");
            if (string.IsNullOrWhiteSpace(cleanText))
            {
                return false;
            }

            bool newValue;
            if (bool.TryParse(
                cleanText, out newValue))
            {
                return newValue;
            }
            else
            {
                int intVal;
                if (int.TryParse(cleanText, out intVal))
                {
                    return System.Convert.ToBoolean(intVal);
                }                

                throw new ConversionException($"Unable to convert {value} to boolean");
            }
        }
    }
}
