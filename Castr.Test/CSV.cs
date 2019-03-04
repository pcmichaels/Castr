using CastDataAs.Test.TestData;
using System;
using Xunit;

namespace CastDataAs.Test
{
    public class ConvertsToStruct
    {
        [Fact]
        public void BasicCsvToStruct_Converts()
        {
            // Arrange
            string csvData = "this,is,data";
            var csv = new CSV(csvData, ",");

            // Act
            var newStruct = csv.CastAsStruct<SimpleTestStruct>();

            // Assert
            Assert.Equal("this", newStruct.Field1);
            Assert.Equal("is", newStruct.Field2);
            Assert.Equal("data", newStruct.Field3);
        }
    }
}
