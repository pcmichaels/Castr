using Castr.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Castr.Test.TestData
{
    public class OrderedSimpleTestClassMultiType
    {
        [Order]
        public string Property1 { get; set; }

        [Order]
        public string Property2 { get; set; }

        [Order]
        public string Property3 { get; set; }

        [Order]
        public string PropertyThree { get; set; }

        [Order]
        public decimal NumberPropertyOne { get; set; }

        [Order]
        public int NumberPropertyTwo { get; set; }

        [Order]
        public DateTime DateProperty { get; set; }

        [Order]
        public DateTime DateProperty2 { get; set; }

        [Order]
        public bool BoolProperty { get; set; }
    }
}
