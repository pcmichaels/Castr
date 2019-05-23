using Castr.Exceptions;
using Castr.Options;
using Castr.Test.TestData;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Castr.Test.ClassToSubClass
{
    public class ClassToSubClass_Creates
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ConvertClassToSubClass_Converts(bool isStrict)
        {
            // Arrange
            var parentClass = new SimpleTestClass();
            parentClass.Property1 = "test";
            var castrClass = new CastrClass<SimpleTestClass>(
                parentClass, new Options.ClassOptions()
                {
                    IsStrict = isStrict
                });

            // Act
            var childClass = castrClass.CastAsClass<SimpleTestSubClass>();

            // Assert
            Assert.Equal("test", childClass.Property1);
            Assert.True(string.IsNullOrWhiteSpace(childClass.NewProperty));
        }

        [Fact]
        public void ConvertClassToOtherClass_Strict_Throws()
        {
            // Arrange
            var parentClass = new SimpleTestClass2();
            parentClass.Property1 = "test";
            var castrClass = new CastrClass<SimpleTestClass2>(
                parentClass, new ClassOptions()
                {
                    IsStrict = true
                });

            // Act
            void PerformCast()
            {
                var childClass = castrClass.CastAsClass<SimpleTestSubClass>();
            }

            // Assert
            Assert.Throws<CastingException>((Action)PerformCast);
        }

        [Fact]
        public void ConvertClassToOtherClass_NotStrict_Converts()
        {
            // Arrange
            var parentClass = new SimpleTestClass2();
            parentClass.Property1 = "test";
            var castrClass = new CastrClass<SimpleTestClass2>(
                parentClass, new ClassOptions()
                {
                    IsStrict = false
                });

            // Act
            var childClass = castrClass.CastAsClass<SimpleTestSubClass>();

            // Assert
            Assert.Equal("test", childClass.Property1);
            Assert.True(string.IsNullOrWhiteSpace(childClass.NewProperty));

        }
    }
}
