using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Castr.Conversion
{
    public class DateConverter : IConverter
    {
        public object Convert(string value, Type type) =>
            Convert(value, CultureInfo.CurrentCulture, type);

        public object Convert(string value, CultureInfo culture, Type type)
        {
            DateTime newValue;
            if (DateTime.TryParse(
                value, culture, DateTimeStyles.None, out newValue))
            {
                return newValue;
            }
            else
            {
                if (value.Length == 8)
                {
                    int year = int.Parse(value.Substring(0, 4));
                    int month = int.Parse(value.Substring(4, 2));
                    int day = int.Parse(value.Substring(6, 2));
                    newValue = new DateTime(year, month, day);
                }
                else if (value.Length == 6)
                {
                    // ToDo: move this into an injected dependency
                    int year = int.Parse($"{DateTime.Now.Year.ToString().Substring(0, 2)}{value.Substring(0, 2)}");
                    int month = int.Parse(value.Substring(2, 2));
                    int day = int.Parse(value.Substring(4, 2));
                    newValue = new DateTime(year, month, day);
                }

                return newValue;
            }
        }
    }
}
