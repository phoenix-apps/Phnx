# Phnx.Data

This library contains tools to help you manage data sets, particularly at scale. This contains features like lazy loading, repositories, database seeds, and escaping data strings

# Quick Examples

For a full list of all things possible with Phnx.Data, please check the [API reference guide](https://phoenix-apps.github.io/Phnx-Wiki/api/Phnx.Data.html)

## Escaped

```cs
string originalText = "Example data string with an_underscore";

string escapedText = Escaped.Escape(originalText, '_', ' ');

// Output: Example_ data_ string_ with_ an__underscore
Console.WriteLine(escapedText);

string unescapedText = Escaped.Unescape(originalText, '_', ' ');

// Output: Example data string with an_underscore
Console.WriteLine(unescapedText);
```

## Lazy Database

```cs
// Set up lazy database with tables, and a 30 seconds maximum lifespan for each cached item
LazyDatabase database = new LazyDatabase(TimeSpan.FromSeconds(30));

// Adds a table for <Guid> which generates a random GUID each time a record is requested
database.TryAddTable<int, Guid>(() => Guid.NewGuid()));

// Output: Total tables configured: 1
Console.WriteLine("Total tables configured: " database.TotalTablesCount);

// Calls to the get method for the table on the 1st load
if (database.TryGet(1, out Guid guidFound))
{
    // Output (example): 7ee6b8b0-2c39-4d2c-a0a7-1c9fe62f202f
    Console.WriteLine(guidFound);
}

// Loads from cache on subsequent loads
if (database.TryGet(1, out guidFound))
{
    // Output (example): 7ee6b8b0-2c39-4d2c-a0a7-1c9fe62f202f
    Console.WriteLine(guidFound);
}

// Updates the entry in the cache to a new guid, resetting the lifespan to 30 seconds for this item
database.TryAddOrUpdate(1, Guid.NewGuid());

// Clears all of the cache, and all configured tables
database.Clear();
```