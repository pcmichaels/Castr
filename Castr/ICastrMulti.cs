using System;
using System.Collections.Generic;
using System.Text;

namespace Castr
{
    public interface ICastrMulti
    {
        /// <summary>
        /// Creates an enumerable list of Class<T> based on the CSV data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> CastAsClassMulti<T>() where T: class;

        /// <summary>
        /// Creates an enumerable list of Struct<T> based on the CSV data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> CastAsStructMulti<T>() where T: struct;

        /// <summary>
        /// Return the raw data
        /// </summary>
        /// <returns>
        /// An enumerable list of string arrays
        /// </returns>
        public IEnumerable<string[]> GetRawData();
    }
}
