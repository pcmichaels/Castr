using System;
using System.Collections.Generic;
using System.Text;

namespace Castr.Exceptions
{
    public class ConversionException : Exception
    {
        public ConversionException(string message) : base(message)
        {

        }
    }
}
