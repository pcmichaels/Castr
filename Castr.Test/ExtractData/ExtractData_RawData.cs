using Castr.CSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Castr.Test.ExtractData
{
    public class ExtractData_RawData
    {
        [Fact]
        public void ExtractRawData_DataIsExtracted()
        {
            // Arrange
            string csvData = $"Property1,Property2,Property3{Environment.NewLine}this,is,data{Environment.NewLine}so,is,this";
            var csv = new CastrCSVMulti(csvData, ",", true);

            // Act
            var rawData = csv.GetRawData();

            // Assert
            Assert.NotNull(rawData);
        }

        [Fact]
        public void ExtractRawData_DataIsCorrect()
        {
            // Arrange
            string csvData = $"Property1,Property2,Property3{Environment.NewLine}this,is,data{Environment.NewLine}so,is,this";
            var csv = new CastrCSVMulti(csvData, ",", true);

            // Act
            var rawData = csv.GetRawData();

            // Assert
            Assert.Equal(2, rawData.Count());
            foreach (var data in rawData)
            {
                Assert.Equal(3, data.Length);
            }
        }
    }
}
