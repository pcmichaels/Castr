using System;
using System.Collections.Generic;
using System.Text;

namespace Castr.Extensions
{
    public static class CastrExtensions
    {
        public static Dictionary<string, object> AsDictionary<T>(this T classToConvert)
        {
            var castr = new CastrClass<T>(classToConvert);
            var dict = castr.AsDictionary();
            return dict;
        }

        public static string AsCSV(this Dictionary<string, string> dict) 
        { 
            var sb = new StringBuilder();

            foreach (var itm in dict) 
            {
                sb.AppendLine($"{itm.Key},{itm.Value}");
            }
            return sb.ToString();
        }
    }
}
