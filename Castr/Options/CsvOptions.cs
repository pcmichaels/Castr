using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Castr.Options
{
    public class CsvOptions
    {
        public CsvOptions()
        {

        }

        public CsvOptions(bool includesHeaders = false,
            bool strictHeaderCountMatching = false,
            bool strictHeaderNameMatching = false,
            string delimiter = ",",
            string culture = "")
        {
            IncludesHeaders = includesHeaders;
            StrictHeaderCountMatching = strictHeaderCountMatching;
            StrictHeaderNameMatching = strictHeaderNameMatching;
            Delimiter = delimiter;

            if (string.IsNullOrWhiteSpace(culture))
            {
                Culture = CultureInfo.CurrentCulture;
            }
            else
            {
                Culture = CultureInfo.GetCultureInfo(culture);
            }
        }

        public bool IncludesHeaders { get; set; }
        public bool StrictHeaderCountMatching { get; set; }
        public bool StrictHeaderNameMatching { get; set; }
        public bool MatchByHeader { get; set; }
        public string Delimiter { get; set; }
        public CultureInfo Culture { get; set; } = CultureInfo.CurrentCulture;
    }
}
