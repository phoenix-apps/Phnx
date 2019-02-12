# Phnx

This library contains the core of Phnx, and all the most common classes and libraries.

# Features

* Ordinals
* Comparing Generics `<T>`
* Converting Generics `<T>`
* Error Messages
* ArgumentLessThanZeroException
* Extensions for the following:
> * Async extensions for `Action`
> * `bool`
> * `char`
> * `object`
> * `string`
> * `Type`

# Quick Examples

For a full list of all things possible with the Phnx core, please check the [API reference guide](https://phoenix-apps.github.io/Phnx-Wiki/api/Phnx.html)

## Comparer
```cs
// Example Comparer
Comparer<string> stringComparer = ComparerHelpers.DefaultComparer<string>();

int compareResult = stringComparer.Compare("A2", "B7");

// Output: -1
Console.WriteLine(compareResult);
```

## Object Extensions

#### SingleToIEnumerable
```cs
int startOfCollection = 12;

// Creates a new collection starting with a single item
IEnumerable<int> collection = startOfCollection.SingleToIEnumerable();

// Output: 12
foreach(int item in collection)
{
    Console.WriteLine(item);
}
```

#### Chain

```cs
StringBuilder stringBuilder = new StringBuilder();

stringBuilder
    .Chain(sb => sb.Append("a"))
    .Chain(sb => sb.Append("b"))
    .Chain(sb => sb.Append("c"))
    .Chain(sb => sb.Append("d"));

// Output: abcd
Console.WriteLine(stringBuilder);
```

## TimeSpan Extensions
```cs
// Format TimeSpan as readable string
var sampleTime = new TimeSpan(12, 5, 1, 17, 24);

TimeComponents timeComponentsToShow =
    TimeComponents.Hours |
    TimeComponents.Minutes;

string sampleTimeFormatted = sampleTime.ToString(timeComponentsToShow);

// Output: 05:01
Console.WriteLine(sampleTimeFormatted);
```

## DateTime Extensions
```cs
// Format as short date in a given culture
DateTimeFormatInfo ukDateTimeFormat = CultureInfo.GetCultureInfo("en-GB").DateTimeFormat;
DateTime sampleDate = new DateTime(2012, 9, 4, 12, 15, 36);

string shortUkDate = sampleDate.AsDateString(ukDateTimeFormat);

// Output: 04/09/2012
Console.WriteLine(shortUkDate);
```