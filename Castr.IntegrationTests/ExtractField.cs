using Castr.Exceptions;
using Castr.IntegrationTests.Models;
using Castr.Options;
using System;
using System.IO;
using Xunit;

namespace Castr.IntegrationTests
{
    public class ExtractField
    {
        [Fact]
        public void ExtractSingleField()
        {
            string csvData = File.ReadAllText("Data/stats.csv");
            var csv = new Castr.CSV.CastrCSVMulti(csvData, ",", true);

            var sampleDataList = csv.CastAsClassMulti<Stats>();

            foreach (var sampleData in sampleDataList)
            {
                var castr = new CastrClass<Stats>(
                    sampleData, new ClassOptions()
                    {
                        IsStrict = false
                    });
                float result = castr.ExtractField<float>("Total_corners");

                Assert.Equal(7, result);
                break;
            }
        }

        [Fact]
        public void ExtractSingleField_InvalidColumnName()
        {
            string csvData = File.ReadAllText("Data/stats.csv");
            var csv = new Castr.CSV.CastrCSVMulti(csvData, ",", true);

            var sampleDataList = csv.CastAsClassMulti<Stats>();

            foreach (var sampleData in sampleDataList)
            {
                var castr = new CastrClass<Stats>(
                    sampleData, new ClassOptions()
                    {
                        IsStrict = false
                    });
                Assert.Throws<InvalidFieldException>(() =>
                {
                    float result = castr.ExtractField<float>("total_corners");
                });
            }
        }

    }
}
