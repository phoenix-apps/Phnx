# Phnx.Benchmark

This library contains tools to help benchmark your code for performance

There are two options for benchmarking your code.

1. Allows you to wrap several functions that take the same parameters, and will automatically log events (such as a method finishing) for you.

1. Allows you to quickly run a small benchmark for a particular section of code.

## Quick Examples

For a full list of all things possible with Phnx.Benchmark, please check the [API reference guide](https://phoenix-apps.github.io/Phnx-Wiki/api/Phnx.Benchmark.html)

```cs
Action fastMethod = () => {
    Thread.SpinWait(1);
};

Action slowMethod = () => {
    Thread.SpinWait(100);
};

BenchmarkMethods benchmark = new BenchmarkMethods(new Action[] {
    fastMethod,
    slowMethod
});

// Runs each method 10,000 times
TimeSpan[] times = benchmark.Run(10000);

// Output: Total time taken for fastMethod
Console.WriteLine(times[0]);

// Output: Total time taken for slowMethod
Console.WriteLine(times[1]);
```

## Benchmark Section

For a full list of all things possible with BenchmarkSection, please check the [API reference guide](https://phoenix-apps.github.io/Phnx-Wiki/api/Phnx.Benchmark.Section.html)

```cs
BenchmarkSectionTime time;
using (new BenchmarkSection(out time))
{
    Thread.SpinWait(1000);
}

// Output: Total time spent inside benchmark section
Console.WriteLine(time.TimeTaken);
```