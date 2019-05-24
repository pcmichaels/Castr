# Castr

Popular Data Conversion Helpers

## Usage

### To convert a class to child class

```csharp
var parentClass = new SimpleTestClass();
parentClass.Property1 = "test";
var castrClass = new CastrClass<SimpleTestClass>(
    parentClass, new Options.ClassOptions()
    {
        IsStrict = true
    });

var childClass = castrClass.CastAsClass<SimpleTestSubClass>();
```

Turning Strict off enables this to be used between any two classes that share property names.


### Convert a CSV to a class (singular)

```csharp
string csvData = File.ReadAllText(@"c:\txt.csv");
var csv = new CastrCSV(csvData, ",", true);

// Act
var newClass = csv.CastAsClass<SimpleTestClass>();
```

### Convert a multi line CSV to a class list)

```csharp
string csvData = File.ReadAllText(@"c:\txt.csv");
var csv = new CastrCSVMulti(csvData, ",", true);

// Act
var newClassEnumerable = csv.CastAsClassMulti<SimpleTestClass>();

```

