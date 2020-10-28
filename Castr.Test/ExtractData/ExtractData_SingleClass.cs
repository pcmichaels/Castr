using Castr.Test.TestData;
using Xunit;

namespace Castr.Test.ExtractData
{    
    public class ExtractData_SingleClass
    {
        [Fact]
        public void CopyClassToClass_SingleValue()
        {
            // Arrange
            var sourceClass = new OrderedSimpleTestClassMultiType()
            {
                Property2 = "test2",
                Property1 = "test",                
                Property3 = "test3"
            };

            var castrClass = new CastrClass<OrderedSimpleTestClassMultiType>(
                sourceClass, new Options.ClassOptions()
                {
                    IsStrict = false
                });

            // Act
            string data = castrClass.ExtractField<string>("Property2");

            /// Assert
            Assert.Equal("test2", data);
        }

        [Fact]
        public void CopyClassToClass_SingleValue_Strict()
        {
            // Arrange
            var sourceClass = new OrderedSimpleTestClassMultiType()
            {
                Property1 = "test",
                Property2 = "test2"
            };

            var castrClass = new CastrClass<OrderedSimpleTestClassMultiType>(
                sourceClass, new Options.ClassOptions()
                {
                    IsStrict = true
                });

            // Act
            string data = castrClass.ExtractField<string>("Property2");

            /// Assert
            Assert.Equal("test2", data);
        }

    }
}
