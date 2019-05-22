using System;
using System.Collections.Generic;
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
            string delimiter = ",")
        {
            IncludesHeaders = includesHeaders;
            StrictHeaderCountMatching = strictHeaderCountMatching;
            StrictHeaderNameMatching = strictHeaderNameMatching;
            Delimiter = delimiter;
        }

        public bool IncludesHeaders { get; set; }
        public bool StrictHeaderCountMatching { get; set; }
        public bool StrictHeaderNameMatching { get; set; }
        public string Delimiter { get; set; }
    }
}
