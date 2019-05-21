using System;
using System.Collections.Generic;
using System.Text;

namespace Castr.Exceptions
{
    public class InvalidSourceDataException : Exception
    {
        public InvalidSourceDataException(string message) : base(message)
        {
        }
    }
}
