using Castr.Test.TestData;
using Castr.Exceptions;
using Castr.Options;
using System;
using Xunit;

namespace Castr.Test.CSVToClass
{
    public class CSVToClass_Header
    {
        [Fact]
        public void BasicCsvToClass_Converts()
        {
            // Arrange
            string csvData = $"Property1,Property2,Property3{Environment.NewLine}this,is,data";
            var csv = new CastrCSV(csvData, ",", true);

            // Act
            var newClass = csv.CastAsClass<SimpleTestClass>();

            // Assert
            Assert.Equal("this", newClass.Property1);
            Assert.Equal("is", newClass.Property2);
            Assert.Equal("data", newClass.Property3);
        }

        [Fact]
        public void BasicCsvToClass_TooManyFields_Converts()
        {
            // Arrange
            string csvData = $"Property1,Property2,Property3{Environment.NewLine}this,is,data,too,much,data";
            var csv = new CastrCSV(csvData, ",", true);

            // Act
            var newClass = csv.CastAsClass<SimpleTestClass>();

            // Assert
            Assert.Equal("this", newClass.Property1);
            Assert.Equal("is", newClass.Property2);
            Assert.Equal("data", newClass.Property3);
        }

        [Fact]
        public void BasicCsvToClass_TooManyHeadersForClass_Strict_Throws()
        {
            // Arrange
            string csvData = $"Property1,Property2,Property3,Property4{Environment.NewLine}this,is,data,too,much,data";
            var csv = new CastrCSV(csvData, new CsvOptions(true, true, true, ","));

            void Act()
            {
                // Act
                var newClass = csv.CastAsClass<SimpleTestClass>();
            }

            // Assert
            _ = Assert.Throws<CastingException>((Action)Act);
        }

        [Fact]
        public void BasicCsvToClass_InvalidHeadersForClass_Strict_Throws()
        {
            // Arrange
            string csvData = $"Property1,Property2,Aardvark{Environment.NewLine}this,is,data";
            var csv = new CastrCSV(csvData, new CsvOptions(true, true, true, ","));

            void Act()
            {
                // Act
                var newClass = csv.CastAsClass<SimpleTestClass>();
            }

            // Assert
            _ = Assert.Throws<CastingException>((Action)Act);
        }

        [Fact]
        public void BasicCsvToClass_TooFewFields_Converts()
        {
            // Arrange
            string csvData = $"Property1,Property2,Property3{Environment.NewLine}this,is";
            var csv = new CastrCSV(csvData, ",", true);

            // Act
            var newClass = csv.CastAsClass<SimpleTestClass>();

            // Assert
            Assert.Equal("this", newClass.Property1);
            Assert.Equal("is", newClass.Property2);
            Assert.Null(newClass.Property3);
        }

        [Fact]
        public void BasicCsvToClass_TooFewHeaders_Converts()
        {
            // Arrange
            string csvData = $"Property1,Property2{Environment.NewLine}this,is";
            var csv = new CastrCSV(csvData, ",", true);

            // Act
            var newClass = csv.CastAsClass<SimpleTestClass>();

            // Assert
            Assert.Equal("this", newClass.Property1);
            Assert.Equal("is", newClass.Property2);
            Assert.Null(newClass.Property3);
        }

    }
}
