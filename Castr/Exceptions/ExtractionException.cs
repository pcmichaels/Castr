using System;
using System.Collections.Generic;
using System.Text;

namespace Castr.Exceptions
{
    public class ExtractionException : Exception
    {
        public ExtractionException(string message) : base(message) { }
    }
}
