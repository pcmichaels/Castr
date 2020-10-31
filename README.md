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

### Analysing a CSV without having a statically typed class

Castr also supports extracting and processing the data from a CSV without having a statically 
typed class to cast the data to.  For example:

```csharp
string csvData = File.ReadAllText("Data/stats.csv");
var csv = new Castr.CSV.CastrCSVMulti(csvData, ",", true);

var dataList = csv.GetRawData();

foreach (var data in dataList)
{   
    string result = csv.ExtractField("total_corners", data);
    DoSomethingWithData(result);
}
```

This only works where there are header fields, otherwise, you can simply process the raw data.
For example:

```csharp
string csvData = File.ReadAllText("Data/stats.csv");
var csv = new Castr.CSV.CastrCSVMulti(csvData, ",", true);

var dataList = csv.GetRawData();
foreach (var data in dataList)
{   
    string result = data[3];
    DoSomethingWithData(result);
}

```
