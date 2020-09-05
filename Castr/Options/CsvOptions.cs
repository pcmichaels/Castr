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

        /// <summary>
        /// Flag indicating that the header row is included in the data
        /// </summary>
        public bool IncludesHeaders { get; set; }

        /// <summary>
        /// Ensures that the count of headers is the same as those in the class
        /// </summary>
        public bool StrictHeaderCountMatching { get; set; }

        /// <summary>
        /// If a header is present, switching this on will cause a runtime
        /// exception where the headers are not in the same order as the class.
        /// Overrides MatchByHeader.
        /// </summary>
        public bool StrictHeaderNameMatching { get; set; }

        /// <summary>
        /// Try to match properties based on the header value for those properties.
        /// Do not use with StrictHeaderNameMatching
        /// </summary>
        public bool MatchByHeader { get; set; }

        /// <summary>
        /// Determine how the elements of the CSV are separated
        /// Defaults to a comma (,)
        /// </summary>
        public string Delimiter { get; set; }
        public CultureInfo Culture { get; set; } = CultureInfo.CurrentCulture;
    }
}
