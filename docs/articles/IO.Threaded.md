# Phnx.IO.Threaded

This library contains tools to help you stream data in an aggressive multi-threaded environment. These tools are often a major performance boost for work that is mixed between heavy IO and heavy processing.

Traditional workloads that involve reading one record, then analysing it, then reading the next record, will heavily benefit from these tools.

# Quick Examples

For a full list of all things possible with Phnx.IO.Threaded, please check the [API reference guide](https://phoenix-apps.github.io/Phnx-Wiki/api/Phnx.IO.Threaded.html)

## Threaded Reader

```cs
// Starts reading ahead immediately
using (ThreadedReader<Guid> reader = new ThreadedReader<Guid>(() => Guid.NewGuid()))
{
    Thread.Sleep(1);

    int totalCached = reader.CachedCount;

    // Output: 100
    Console.WriteLine(totalCached);

    // Reads the first to be cached, and eager loads the next item in a backing thread
    Guid firstGuid = reader.Read();

    // Output (example): e0364c62-eb69-43b0-b2a4-b114bd2e2c5b
    Console.WriteLine(firstGuid);
}
```

## Threaded Writer

```cs
using (ThreadedWriter<Guid> writer = new ThreadedWriter(Console.WriteLine))
{
    // Queues write into backing thread
    writer.Write(Guid.NewGuid());

    // Will not exit until all items have been written.
    // This behaviour can be avoided if the output action throws an exception, or by calling writer.Dispose(false)
}
```