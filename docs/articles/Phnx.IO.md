# `Phnx.IO`

This library contains tools to help you stream data

# Quick Examples

For a full list of all things possible with `Phnx.IO`, please check the [API reference guide](https://phoenix-apps.github.io/Phnx-Wiki/api/Phnx.IO.html)

## Stream Extensions

#### Read to end

```cs
using Phnx.IO;

Stream myData = new MemoryStream(new byte[] { 12, 25 });

byte[] myDataContents = myData.ReadToEnd();

/* Output:
12
25
*/
Console.WriteLine(myDataContents[0]);
Console.WriteLine(myDataContents[1]);
```

#### Write

```cs
using Phnx.IO;

Encoding textEncoding = Encoding.UTF8;
MemoryStream myData = new MemoryStream();

myData.Write("Sample text", textEncoding);

string restoredData = textEncoding.GetString(myData.ToArray());

// Output: Sample text
Console.WriteLine(restoredData);
```

## Pipe Stream

```cs
Stream pipe = new PipeStream();

pipe.In.WriteLine("Sample Text");
pipe.Write("Text 2");

string pipeContentLine = pipe.Out.ReadLine();

// Output: Sample text
Console.WriteLine(pipeContentLine);

pipeContentLine = pipe.Out.ReadLine();

// Output: Text 2
Console.WriteLine(pipeContentLine);
```