using Castr.Test.TestData;
using Castr.Exceptions;
using Castr.Options;
using System;
using Xunit;
using Castr.CSV;
using System.Linq;
using System.Collections.Generic;

namespace Castr.Test.ClassToCSV
{
    public class ClassToCSV_Header
    {
        [Fact]
        public void BasicClassToCSV_IncludeHeaders_Converts()
        {
            // Arrange
            var testClassList = new List<SimpleTestClass>()
            {
                new SimpleTestClass(){ Property1 = "aa", Property2 = "bb", Property3 = "ss" },
                new SimpleTestClass(){ Property1 = "bb", Property2 = "dfdf", Property3 = "dfdf" },
                new SimpleTestClass(){ Property1 = "cc", Property2 = "5445", Property3 = "ghgh" }
            };
            var csv = new CastrCSVMulti(",", true);

            // Act
            string csvString = csv.CastAsCSV<SimpleTestClass>(testClassList);

            // Assert
            var parsedCsvString = csvString.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(4, parsedCsvString.Length);
        }

        [Fact]
        public void BasicClassToCSV_NoHeaders_Converts()
        {
            // Arrange
            var testClassList = new List<SimpleTestClass>()
            {
                new SimpleTestClass(){ Property1 = "aa", Property2 = "bb", Property3 = "ss" },
                new SimpleTestClass(){ Property1 = "bb", Property2 = "dfdf", Property3 = "dfdf" },
                new SimpleTestClass(){ Property1 = "cc", Property2 = "5445", Property3 = "ghgh" }
            };
            var csv = new CastrCSVMulti(",", false);

            // Act
            string csvString = csv.CastAsCSV<SimpleTestClass>(testClassList);

            // Assert
            var parsedCsvString = csvString.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(3, parsedCsvString.Length);
        }

    }
}
