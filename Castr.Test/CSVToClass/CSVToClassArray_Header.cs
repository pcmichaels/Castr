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
            string csvData = $"Property1,Property2,Property3{Environment.NewLine}this,is,data{Environment.NewLine}this,is,moredata";
            var csv = new CastrCSVMulti(csvData, ",", true);

            // Act
            var newClassEnumerable = csv.CastAsClassMulti<SimpleTestClass>();

            // Assert
            Assert.Single(newClassEnumerable);
            Assert.Equal("this", newClassEnumerable.First().Property1);
            Assert.Equal("is", newClassEnumerable.First().Property2);
            Assert.Equal("data", newClassEnumerable.First().Property3);
        }


    }
}
