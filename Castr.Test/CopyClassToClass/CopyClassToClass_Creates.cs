using Castr.Test.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Castr.Test.CopyClassToClass
{
    public class CopyClassToClass_Creates
    {
        [Fact]
        public void CopyClassToClass_SingleValue()
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
                    IsStrict = false
                });

            // Act
            var newClass = castrClass.CastAsClass<SingleValue>();

            /// Assert
            Assert.Equal("test", newClass.Property1);
        }



        [Fact]
        public void CopyClassToClass_ContainsList()
        {
            // Arrange
            var sourceClass = new ClassContainsListOfClasses();
            sourceClass.SimpleTestClasses = new List<OrderedSimpleTestClassMultiType>()
            {
                new OrderedSimpleTestClassMultiType() {Property1 = "test"}
            };

            var castrClass = new CastrClass<ClassContainsListOfClasses>(
                sourceClass, new Options.ClassOptions() { });

            // Act
            var newClass = castrClass.CastAsClass<ClassContainsListOfClasses>();

            /// Assert
            Assert.Equal("test", newClass.SimpleTestClasses.First().Property1);
        }

        [Fact]
        public void CopyClassToClass_ContainsEmptyList()
        {
            // Arrange
            var sourceClass = new ClassContainsListOfClasses();
            sourceClass.SimpleTestClasses = new List<OrderedSimpleTestClassMultiType>();

            var castrClass = new CastrClass<ClassContainsListOfClasses>(
                sourceClass, new Options.ClassOptions() { });

            // Act
            var newClass = castrClass.CastAsClass<ClassContainsListOfClasses>();

            /// Assert
            Assert.Empty(newClass.SimpleTestClasses);
        }

    }
}
