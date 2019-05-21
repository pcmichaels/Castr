using System;
using System.Collections.Generic;
using System.Text;

namespace Castr.Exceptions
{
    public class CastingException : Exception
    {
        public CastingException(string message) : base(message)
        {

        }
    }
}
