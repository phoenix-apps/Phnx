# Phnx.IO.Json

This library contains tools to help you stream JSON data

# Quick Examples

For a full list of all things possible with Phnx.IO.Json, please check the [API reference guide](https://phoenix-apps.github.io/Phnx-Wiki/api/Phnx.IO.Json.html)

## Json Stream Reader

```cs
// Use the stream you want to read from
byte[] jsonAsBytes = Encoding.UTF8.GetBytes(
    "{id: 5, value: \"Sample\"}" +
    "{id: 17, value: \"Sample 2\"}");
MemoryStream inMemoryData = new MemoryStream(jsonAsBytes);
TextReader dataSource = new StreamReader(inMemoryData);

using (JsonStreamReader reader = new JsonStreamReader(dataSource, true))
{
    string rawJson = reader.ReadJson(Formatting.Indented);

    /* Output:
    {
        id: 5,
        value: "Sample"
    }
    */
    Console.WriteLine(rawJson);

    rawJson = reader.ReadJson();

    // Output: { id: 17, value: "Sample 2" }
    Console.WriteLine(rawJson);
}
```

## Json Stream Writer

```cs
MemoryStream inMemoryData = new MemoryStream();
TextReader dataOutput = new StreamReader(inMemoryData);

using (JsonStreamWriter writer = new JsonStreamWriter(dataOutput, true))
{
    // Write raw JSON
    writer.Write("{id: 5, value: \"Sample\"}");

    // Write any object
    writer.Write(new
    {
        id = 17,
        value = "Sample 2"
    });

    // Auto-flushes data, and disposes the dataOutput stream
}
```