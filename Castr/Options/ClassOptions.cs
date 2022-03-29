using System;
using System.Collections.Generic;
using System.Text;

namespace Castr.Options
{
    public class ClassOptions
    {
        public bool IsStrict { get; set; }
        public bool PropertyNameMustMatch { get; set; } = false;
        public bool PropertyNameRemoveUnderscores { get; set; } = false;
    }
}
