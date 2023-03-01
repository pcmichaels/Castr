using Castr.Test.TestData;
using System;
using System.Collections.Generic;
using Xunit;
using Castr.Extensions;

namespace Castr.Test.DictionaryConvert
{

    public class ConvertFromDictionary
    {
        [Fact]
        public void ConvertFromDictionary_Converts()
        {
            var dict = new Dictionary<string, string>()
            {
                { "key", "field1, field2, field3" },
                { "key2", "field1data, field2data, field3data" }
            };            

            // Act
            string result = dict.AsCSV();

            // Assert
            Assert.NotEmpty(result);

            string[] split = result.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(2, split.Length);

            string[] splitLine = split[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(4, splitLine.Length);
        }
    }
}