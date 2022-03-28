using Castr.Test.TestData;
using System;
using Xunit;

namespace Castr.Test.DictionaryConvert
{

    public class ConvertToDictionary
    {
        [Fact]
        public void ConvertToDictionary_Converts()
        {
            var testClass = new OrderedSimpleTestClassMultiType()
            {
                BoolProperty = false,
                DateProperty = new DateTime(2021, 01, 07),
                Property1 = "qwerty"
            };
            var castrClass = new CastrClass<OrderedSimpleTestClassMultiType>(
                testClass, new Options.ClassOptions());

            // Act
            var result = castrClass.CastAsDictionary();

            // Assert
            Assert.Equal("qwerty", result["Property1"]);
            Assert.False(Convert.ToBoolean(result["BoolProperty"]));
            Assert.Equal(0, DateTime.Compare(Convert.ToDateTime(result["DateProperty"]), 
                                             new DateTime(2021, 01, 07)));

        }
    }
}