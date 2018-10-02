using NUnit.Framework;
using System;

namespace Phnx.Benchmark.Tests
{
    public class BenchmarkWithoutInput
    {
        [Test]
        public void Run_WithoutBenchmarks_ReturnsEmptyArray()
        {
            var actions = new Action[0];

            var benchmark = new BenchmarkMethods(actions);
            var result = benchmark.Run();

            Assert.IsEmpty(result);
        }

        [Test]
        public void Run_WithSingleBenchmark_ZeroTimes_DoesNotRunBenchmark()
        {
            var timesExecuted = 0;
            var actions = new Action[]
            {
                () => ++timesExecuted
            };

            var benchmark = new BenchmarkMethods(actions);
            var result = benchmark.Run(0);

            Assert.AreEqual(0, timesExecuted);
        }

        [Test]
        public void Run_With5Benchmarks_10Times_RunsAllBenchmarks10Times()
        {
            var timesExecuted = new int[5];

            var actions = new Action[]
            {
                () => ++timesExecuted[0],
                () => ++timesExecuted[1],
                () => ++timesExecuted[2],
                () => ++timesExecuted[3],
                () => ++timesExecuted[4]
            };

            var benchmark = new BenchmarkMethods(actions);

            var result = benchmark.Run(10);

            Assert.AreEqual(10, timesExecuted[0]);
            Assert.AreEqual(10, timesExecuted[1]);
            Assert.AreEqual(10, timesExecuted[2]);
            Assert.AreEqual(10, timesExecuted[3]);
            Assert.AreEqual(10, timesExecuted[4]);
        }

        [Test]
        public void Run_With5Benchmarks_Gives5ResultTimes()
        {
            var actions = new Action[]
            {
                () => { },
                () => { },
                () => { },
                () => { },
                () => { }
            };

            var benchmark = new BenchmarkMethods(actions);

            var result = benchmark.Run();

            Assert.AreEqual(5, result.Length);
        }
    }
}
