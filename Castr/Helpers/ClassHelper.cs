using System;
using System.Collections.Generic;
using System.Text;

namespace Castr.Helpers
{
    public static class ClassHelper
    {
        internal static bool AreClassesRelated<TExistingClass, TNewClass>() =>
            typeof(TNewClass).IsSubclassOf(typeof(TExistingClass))
             || typeof(TExistingClass).IsSubclassOf(typeof(TNewClass));        
    }
}
