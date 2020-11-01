using Castr.IntegrationTests.Models;
using Castr.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Castr.IntegrationTests
{
    public class GetOptionsFor
    {
        [Fact]
        public void ExtractSingleField()
        {
            string csvData = File.ReadAllText("Data/stats.csv");
            var csv = new Castr.CSV.CastrCSVMulti(csvData, ",", true);

            var options = csv.GetOptionsFor("home_team_name");
            Assert.Equal(20, options.Length);

            var optionsStadium = csv.GetOptionsFor("stadium_name");
            Assert.Equal(20, options.Length);
        }


    }
}
