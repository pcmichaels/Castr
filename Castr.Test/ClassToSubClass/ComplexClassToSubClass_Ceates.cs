using Castr.Exceptions;
using Castr.Options;
using Castr.Test.TestData;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Castr.Test.ClassToSubClass
{
    public class ComplexClassToSubClass_Creates
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ConvertComplexClassToSubClass_ConvertsDeep(bool isStrict)
        {
            // Arrange
            var parentClass = new ComplexTestClassParent()
            {
                SimpleTestClass = new SimpleTestClass()
                {
                    Property1 = "test",
                    Property2 = "test2",
                    Property3 = "test3"
                }
            };
            parentClass.Property1 = "test";
            var castrClass = new CastrClass<ComplexTestClassParent>(
                parentClass, new Options.ClassOptions()
                {
                    IsStrict = isStrict
                });

            // Act
            var childClass = castrClass.CastAsClass<ComplexTestClass>();

            // Assert
            Assert.Equal("test", childClass.Property1);
            Assert.True(string.IsNullOrWhiteSpace(childClass.NewProperty));
            Assert.Equal("test", childClass.SimpleTestClass.Property1);
            Assert.Equal("test2", childClass.SimpleTestClass.Property2);
            Assert.Equal("test3", childClass.SimpleTestClass.Property3);

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ConvertComplexClassToSubClass_DisposeOriginal_ConvertsDeep(bool isStrict)
        {
            // Arrange
            var parentClass = new ComplexTestClassParent()
            {
                SimpleTestClass = new SimpleTestClass()
                {
                    Property1 = "test",
                    Property2 = "test2",
                    Property3 = "test3"
                }
            };
            parentClass.Property1 = "test";
            var castrClass = new CastrClass<ComplexTestClassParent>(
                parentClass, new Options.ClassOptions()
                {
                    IsStrict = isStrict
                });

            var childClass = castrClass.CastAsClass<ComplexTestClass>();

            // Act
            parentClass = null;

            // Assert
            Assert.Equal("test", childClass.Property1);
            Assert.True(string.IsNullOrWhiteSpace(childClass.NewProperty));
            Assert.Equal("test", childClass.SimpleTestClass.Property1);
            Assert.Equal("test2", childClass.SimpleTestClass.Property2);
            Assert.Equal("test3", childClass.SimpleTestClass.Property3);

        }


    }
}
