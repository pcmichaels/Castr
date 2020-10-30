using System;
using System.Collections.Generic;
using System.Text;

namespace Castr.Exceptions
{
    public class InvalidFieldException : Exception 
    {
        public InvalidFieldException(string message) : base(message) { }
    }
}
