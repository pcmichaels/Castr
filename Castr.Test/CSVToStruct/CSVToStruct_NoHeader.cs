using CastDataAs.Test.TestData;
using Castr.CSV;
using System;
using Xunit;

namespace Castr.Test.CSVToStruct
{
    public class CSVToStruct_NoHeader
    {
        [Fact]
        public void BasicCsvToStruct_Converts()
        {
            // Arrange
            string csvData = "this,is,data";
            var csv = new CastrCSV(csvData, ",", false);

            // Act
            var newStruct = csv.CastAsStruct<SimpleTestStruct>();

            // Assert
            Assert.Equal("this", newStruct.Property1);
            Assert.Equal("is", newStruct.Property2);
            Assert.Equal("data", newStruct.Property3);
        }

        [Fact]
        public void BasicCsvToStruct_TooManyFields_Converts()
        {
            // Arrange
            string csvData = "this,is,data,too,much,data";
            var csv = new CastrCSV(csvData, ",");

            // Act
            var newStruct = csv.CastAsStruct<SimpleTestStruct>();

            // Assert
            Assert.Equal("this", newStruct.Property1);
            Assert.Equal("is", newStruct.Property2);
            Assert.Equal("data", newStruct.Property3);
        }

        [Fact]
        public void BasicCsvToStruct_TooFewFields_Converts()
        {
            // Arrange
            string csvData = "this,is";
            var csv = new CastrCSV(csvData, ",");

            // Act
            var newStruct = csv.CastAsStruct<SimpleTestStruct>();

            // Assert
            Assert.Equal("this", newStruct.Property1);
            Assert.Equal("is", newStruct.Property2);
            Assert.Null(newStruct.Property3);
        }

    }
}
