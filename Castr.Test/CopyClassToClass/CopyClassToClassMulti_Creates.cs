using Castr.Class;
using Castr.Test.TestData;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Castr.Test.CopyClassToClass
{
    public class CopyClassToClassMulti_Creates
    {
        [Fact]
        public void CopyClassToClassMulti_SingleValue()
        {
            // Arrange
            var listOfClasses = new List<OrderedSimpleTestClassMultiType>()
            {
                new OrderedSimpleTestClassMultiType()
                {
                    Property1 = "test",
                    Property2 = "test2"
                }
            };

            var castrClass = new CastrClassMulti<OrderedSimpleTestClassMultiType>(listOfClasses);

            // Act
            var newClassList = castrClass.CastAsClassMulti<SingleValue>();

            /// Assert
            Assert.Single(newClassList);
            Assert.Equal("test", newClassList.Single().Property1);
        }

        [Fact]
        public void CopyClassToClassMulti_MultipleValues()
        {
            // Arrange
            var listOfClasses = new List<OrderedSimpleTestClassMultiType>()
            {
                new OrderedSimpleTestClassMultiType()
                {
                    Property1 = "test",
                    Property2 = "test2"
                },
                new OrderedSimpleTestClassMultiType()
                {
                    Property1 = "qwerty",
                    Property2 = "qaswedfr"
                },
                new OrderedSimpleTestClassMultiType()
                {
                    Property1 = "third",
                    Property2 = "_third"
                }
            };

            var castrClass = new CastrClassMulti<OrderedSimpleTestClassMultiType>(listOfClasses);

            // Act
            var newClassList = castrClass.CastAsClassMulti<SingleValue>();

            /// Assert
            Assert.Equal(3, newClassList.Count());
            Assert.Equal("test", newClassList.First().Property1);
            Assert.Equal("qwerty", newClassList.Skip(1).First().Property1);
            Assert.Equal("third", newClassList.Skip(2).First().Property1);
        }


        [Fact]
        public void CopyClassToClassMulti_ContainsList()
        {
            // Arrange
            var sourceClassList = new List<ClassContainsListOfClasses>()
            {
                new ClassContainsListOfClasses()
                {
                    SimpleTestClasses = new List<OrderedSimpleTestClassMultiType>()
                    {
                        new OrderedSimpleTestClassMultiType() {Property1 = "test"}
                    }
                }
            };

            var castrClass = new CastrClassMulti<ClassContainsListOfClasses>(sourceClassList);

            // Act
            var newClassList = castrClass.CastAsClassMulti<ClassContainsListOfClasses>();

            /// Assert
            Assert.Single(newClassList);
            Assert.Equal("test", newClassList.First().SimpleTestClasses.First().Property1);
        }

        [Fact]
        public void CopyClassToClass_ContainsEmptyList()
        {
            // Arrange
            var sourceClassList = new List<ClassContainsListOfClasses>();

            var castrClass = new CastrClassMulti<ClassContainsListOfClasses>(sourceClassList);

            // Act
            var newClass = castrClass.CastAsClassMulti<ClassContainsListOfClasses>();

            /// Assert
            Assert.Empty(newClass);
        }

    }
}
