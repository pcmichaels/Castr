using Castr.Test.TestData;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Castr.Test.ClassToDictionary
{
    public class ClassToDictionary_AllProperties
    {
        [Fact]
        public void ToDictionary_AllProperties()
        {
            // Arrange
            var testClass = new OrderedSimpleTestClassMultiType();
            testClass.Property1 = "test";
            testClass.DateProperty = new DateTime(2021, 01, 05);
            testClass.NumberPropertyOne = 2354;
            var castrClass = new CastrClass<OrderedSimpleTestClassMultiType>(testClass);

            // Act
            var dict = castrClass.AsDictionary();

            // Assert
            Assert.Equal(9, dict.Count);
            Assert.Equal(new DateTime(2021, 01, 05), dict["DateProperty"]);
            Assert.Equal(2354m, dict["NumberPropertyOne"]);
        }

    }
}
