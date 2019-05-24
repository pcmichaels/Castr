using Castr.Exceptions;
using Castr.Options;
using Castr.Test.TestData;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Castr.Test.ClassToSubClass
{
    public class RecursiveComplexClassToSubClass_Creates
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ConvertRecursiveComplexClassToSubClass_ConvertsDeep(bool isStrict)
        {
            // Arrange
            var parentClass = new RecursiveComplexTestClassParent()
            {
                RecursiveComplexTestClassProperty = new RecursiveComplexTestClass()
                {
                    Property1 = "testProp1",
                    RecursiveComplexTestClassProperty = new RecursiveComplexTestClass()
                    {
                        Property1 = "testPropRecursive"
                    }
                },
                Property1 = "qqq"
            };
            
            var castrClass = new CastrClass<RecursiveComplexTestClassParent>(
                parentClass, new Options.ClassOptions()
                {
                    IsStrict = isStrict
                });

            // Act
            var childClass = castrClass.CastAsClass<RecursiveComplexTestClass>();

            // Assert
            Assert.Equal("qqq", childClass.Property1);
            Assert.True(string.IsNullOrWhiteSpace(childClass.NewProperty));
            Assert.Equal("testProp1", childClass.RecursiveComplexTestClassProperty.Property1);
            Assert.Equal("testPropRecursive", childClass.RecursiveComplexTestClassProperty.RecursiveComplexTestClassProperty.Property1);
            

        }

    }
}
