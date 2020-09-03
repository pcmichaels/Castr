﻿using Castr.Test.TestData;
using Castr.Exceptions;
using Castr.Options;
using System;
using Xunit;
using Castr.CSV;

namespace Castr.Test.CSVToClass
{
    public class CSVToClass_Header
    {
        [Fact]
        public void BasicCsvToClass_Converts()
        {
            // Arrange
            string csvData = $"Property1,Property2,Property3{Environment.NewLine}this,is,data";
            var csv = new CastrCSV(csvData, ",", true);

            // Act
            var newClass = csv.CastAsClass<SimpleTestClassMultiType>();

            // Assert
            Assert.Equal("this", newClass.Property1);
            Assert.Equal("is", newClass.Property2);
            Assert.Equal("data", newClass.Property3);
        }

        [Fact]
        public void BasicCsvToClass_Numbers_Converts()
        {
            // Arrange
            string csvData = $"Property1,Property2,Property3,PropertyThree,NumberPropertyOne,NumberPropertyTwo,DateProperty,DateProperty2" +
                $"{Environment.NewLine}x,y,x,x3,1.032,1,1/3/2020,2020-03-02";
            var csv = new CastrCSV(csvData, ",", true);

            // Act
            var newClass = csv.CastAsClass<SimpleTestClassMultiType>();

            // Assert
            Assert.Equal(1.032m, newClass.NumberPropertyOne);
            Assert.Equal(1, newClass.NumberPropertyTwo);            
        }

        [Fact]
        public void BasicCsvToClass_Dates_Converts()
        {
            // Arrange
            string csvData = $"Property1,Property2,Property3,PropertyThree,NumberPropertyOne,NumberPropertyTwo,DateProperty,DateProperty2" +
                $"{Environment.NewLine}x,y,x,x3,1,2,1/3/2020,2020-03-02";
            var csv = new CastrCSV(csvData, new CsvOptions()
            {
                Delimiter = ",",
                IncludesHeaders = true,
                Culture = new System.Globalization.CultureInfo("en-GB")
            });

            // Act
            var newClass = csv.CastAsClass<SimpleTestClassMultiType>();

            // Assert
            Assert.Equal(new DateTime(2020, 03, 01), newClass.DateProperty);
            Assert.Equal(new DateTime(2020, 03, 02), newClass.DateProperty2);
        }

        [Fact]
        public void BasicCsvToClass_TooManyFields_Converts()
        {
            // Arrange
            string csvData = $"Property1,Property2,Property3{Environment.NewLine}this,is,data,too,much,data";
            var csv = new CastrCSV(csvData, ",", true);

            // Act
            var newClass = csv.CastAsClass<SimpleTestClass>();

            // Assert
            Assert.Equal("this", newClass.Property1);
            Assert.Equal("is", newClass.Property2);
            Assert.Equal("data", newClass.Property3);
        }

        [Fact]
        public void BasicCsvToClass_TooManyHeadersForClass_Strict_Throws()
        {
            // Arrange
            string csvData = $"Property1,Property2,Property3,Property4{Environment.NewLine}this,is,data,too,much,data";
            var csv = new CastrCSV(csvData, new CsvOptions(true, true, true, ","));

            void Act()
            {
                // Act
                var newClass = csv.CastAsClass<SimpleTestClassMultiType>();
            }

            // Assert
            _ = Assert.Throws<CastingException>((Action)Act);
        }

        [Fact]
        public void BasicCsvToClass_InvalidHeadersForClass_Strict_Throws()
        {
            // Arrange
            string csvData = $"Property1,Property2,Aardvark{Environment.NewLine}this,is,data";
            var csv = new CastrCSV(csvData, new CsvOptions(true, true, true, ","));

            void Act()
            {
                // Act
                var newClass = csv.CastAsClass<SimpleTestClassMultiType>();
            }

            // Assert
            _ = Assert.Throws<CastingException>((Action)Act);
        }

        [Fact]
        public void BasicCsvToClass_TooFewFields_Converts()
        {
            // Arrange
            string csvData = $"Property1,Property2,Property3{Environment.NewLine}this,is";
            var csv = new CastrCSV(csvData, ",", true);

            // Act
            var newClass = csv.CastAsClass<SimpleTestClassMultiType>();

            // Assert
            Assert.Equal("this", newClass.Property1);
            Assert.Equal("is", newClass.Property2);
            Assert.Null(newClass.Property3);
        }

        [Fact]
        public void BasicCsvToClass_TooFewHeaders_Converts()
        {
            // Arrange
            string csvData = $"Property1,Property2{Environment.NewLine}this,is";
            var csv = new CastrCSV(csvData, ",", true);

            // Act
            var newClass = csv.CastAsClass<SimpleTestClassMultiType>();

            // Assert
            Assert.Equal("this", newClass.Property1);
            Assert.Equal("is", newClass.Property2);
            Assert.Null(newClass.Property3);
        }

    }
}
