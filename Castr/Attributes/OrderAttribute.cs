using System;
using System.Runtime.CompilerServices;

namespace Castr.Attributes
{
    // https://stackoverflow.com/questions/9062235/get-properties-in-order-of-declaration-using-reflection
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class OrderAttribute : Attribute
    {
        private readonly int _order;

        public OrderAttribute([CallerLineNumber] int order = 0)
        {
            _order = order;
        }

        public int Order { get { return _order; } }
    }
}
