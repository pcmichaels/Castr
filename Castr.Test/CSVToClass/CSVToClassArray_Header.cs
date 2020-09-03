using Castr.Test.TestData;
using Castr.Exceptions;
using Castr.Options;
using System;
using Xunit;
using Castr.CSV;
using System.Linq;

namespace Castr.Test.CSVToClass
{
    public class CSVToClassArray_Header
    {
        [Fact]
        public void BasicCsvToClassEnumerable_ConvertsSingle()
        {
            // Arrange
            string csvData = $"Property1,Property2,Property3{Environment.NewLine}this,is,data";
            var csv = new CastrCSVMulti(csvData, ",", true);

            // Act
            var newClassEnumerable = csv.CastAsClassMulti<SimpleTestClassMultiType>();

            // Assert
            Assert.Single(newClassEnumerable);
            Assert.Equal("this", newClassEnumerable.First().Property1);
            Assert.Equal("is", newClassEnumerable.First().Property2);
            Assert.Equal("data", newClassEnumerable.First().Property3);
        }

        [Fact]
        public void BasicCsvToClassEnumerable_SemiColonSeparator_ConvertsSingle()
        {
            // Arrange
            string csvData = $"Property1;Property2;Property3{Environment.NewLine}this;is;data";
            var csv = new CastrCSVMulti(csvData, ";", true);

            // Act
            var newClassEnumerable = csv.CastAsClassMulti<SimpleTestClass>();

            // Assert
            Assert.Single(newClassEnumerable);
            Assert.Equal("this", newClassEnumerable.First().Property1);
            Assert.Equal("is", newClassEnumerable.First().Property2);
            Assert.Equal("data", newClassEnumerable.First().Property3);
        }

        [Fact]
        public void BasicCsvToClassEnumerable_ConvertsMultiLine()
        {
            // Arrange
            string csvData = $"Property1,Property2,Property3{Environment.NewLine}"
                + $"this,is,data{Environment.NewLine}" 
                + $"second,line,ofdata{Environment.NewLine}"
                + $"thirdline,of,data{Environment.NewLine}";
            var csv = new CastrCSVMulti(csvData, ",", true);

            // Act
            var newClassEnumerable = csv.CastAsClassMulti<SimpleTestClassMultiType>();

            // Assert
            Assert.Equal(3, newClassEnumerable.Count());
            Assert.Equal("this", newClassEnumerable.First().Property1);
            Assert.Equal("is", newClassEnumerable.First().Property2);
            Assert.Equal("data", newClassEnumerable.First().Property3);
        }

        [Fact]
        public void HeaderNamesHaveSpaces_ConvertsSingle()
        {
            // Arrange
            string csvData = $"Property 1,Property 2,Property three{Environment.NewLine}this,is,data";
            var csv = new CastrCSVMulti(csvData, new CsvOptions()
            {
                Delimiter = ",",
                IncludesHeaders = true,
                MatchByHeader = true                
            });

            // Act
            var newClassEnumerable = csv.CastAsClassMulti<SimpleTestClassMultiType>();

            // Assert
            Assert.Single(newClassEnumerable);
            Assert.Equal("this", newClassEnumerable.First().Property1);
            Assert.Equal("is", newClassEnumerable.First().Property2);
            Assert.Equal("data", newClassEnumerable.First().PropertyThree);
        }

    }
}
