using Castr.CSV;
using Castr.Test.TestData;
using Xunit;

namespace Castr.Test.CSVToClass
{
    public class CSVToClass_NoHeader
    {
        [Fact]
        public void BasicCsvToClass_Converts()
        {
            // Arrange
            string csvData = "this,is,data";
            var csv = new CastrCSV(csvData, ",");

            // Act
            var newClass = csv.CastAsClass<SimpleTestClassMultiType>();

            // Assert
            Assert.Equal("this", newClass.Property1);
            Assert.Equal("is", newClass.Property2);
            Assert.Equal("data", newClass.Property3);
        }

        [Fact]
        public void BasicCsvToClass_TooManyFields_Converts()
        {
            // Arrange
            string csvData = "this,is,data,too,much,data";
            var csv = new CastrCSV(csvData, ",");

            // Act
            var newClass = csv.CastAsClass<SimpleTestClass>();

            // Assert
            Assert.Equal("this", newClass.Property1);
            Assert.Equal("is", newClass.Property2);
            Assert.Equal("data", newClass.Property3);
        }

        [Fact]
        public void BasicCsvToClass_TooFewFields_Converts()
        {
            // Arrange
            string csvData = "this,is";
            var csv = new CastrCSV(csvData, ",");

            // Act
            var newClass = csv.CastAsClass<SimpleTestClassMultiType>();

            // Assert
            Assert.Equal("this", newClass.Property1);
            Assert.Equal("is", newClass.Property2);
            Assert.Null(newClass.Property3);
        }

    }
}
