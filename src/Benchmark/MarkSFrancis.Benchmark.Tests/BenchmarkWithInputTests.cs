using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MarkSFrancis.Benchmark.Tests
{
    public class BenchmarkWithInputTests
    {
        [Test]
        public void Run_WithoutBenchmarks_ReturnsEmptyArray()
        {
            var actions = new Action<object>[0];

            var benchmark = new BenchmarkMethods<object>(actions);
            var result = benchmark.Run(new object[0]);

            Assert.IsEmpty(result);
        }

        [Test]
        public void Run_WithSingleBenchmark_ZeroTimes_DoesNotRunBenchmark()
        {
            var timesExecuted = 0;
            var actions = new Action<object>[]
            {
                ignore => ++timesExecuted
            };

            var benchmark = new BenchmarkMethods<object>(actions);
            var result = benchmark.Run(new object[0]);

            Assert.AreEqual(0, timesExecuted);
        }

        [Test]
        public void Run_With5Benchmarks_10Times_RunsAllBenchmarks10Times()
        {
            var timesExecuted = new int[5];

            var actions = new Action<object>[]
            {
                ignore => ++timesExecuted[0],
                ignore => ++timesExecuted[1],
                ignore => ++timesExecuted[2],
                ignore => ++timesExecuted[3],
                ignore => ++timesExecuted[4]
            };

            var benchmark = new BenchmarkMethods<object>(actions);

            var result = benchmark.Run(new object[10]);

            Assert.AreEqual(10, timesExecuted[0]);
            Assert.AreEqual(10, timesExecuted[1]);
            Assert.AreEqual(10, timesExecuted[2]);
            Assert.AreEqual(10, timesExecuted[3]);
            Assert.AreEqual(10, timesExecuted[4]);
        }

        [Test]
        public void Run_With5Benchmarks_Gives5ResultTimes()
        {
            var actions = new Action<object>[]
            {
                ignore => { },
                ignore => { },
                ignore => { },
                ignore => { },
                ignore => { }
            };

            var benchmark = new BenchmarkMethods<object>(actions);

            var result = benchmark.Run(new object[0]);

            Assert.AreEqual(5, result.Length);
        }

        [Test]
        public void Run_With5Input_PassesInputsToItemsInOrder()
        {
            List<int> items = new List<int>();
            var expectedItems = new List<int>(5) { 12, 74, 263, 17546, 1 };

            var actions = new Action<int>[]
            {
                item => items.Add(item)
            };

            var benchmark = new BenchmarkMethods<int>(actions);

            var result = benchmark.Run(expectedItems.ToArray() /*Pass copy*/);

            Assert.AreEqual(expectedItems, items);
        }
    }
}
