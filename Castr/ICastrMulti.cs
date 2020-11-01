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

        /// <summary>
        /// Find the specified field name in the data
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="data"></param>
        /// <returns>
        /// String containing the specified data, or null
        /// </returns>
        public string ExtractField(string fieldName, string[] data);

        /// <summary>
        /// Find the specified field name in the data
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="data"></param>
        /// <returns>
        /// A generic type containing the specified data, or null
        /// </returns>
        public T ExtractField<T>(string fieldName, string[] data);

        /// <summary>
        /// Traverses the entire data set to determine available options
        /// May be slow
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns>
        /// A list of options based on existing data for the field in question
        /// </returns>
        public string[] GetOptionsFor(string fieldName);
    }
}
