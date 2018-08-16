using NUnit.Framework;
using System;

namespace MarkSFrancis.Benchmark.Tests
{
    public class ErrorFactoryExtensionsTests
    {
        [Test]
        public void ErrorFactory_GetBenchmarkNotFinished_ReturnsInvalidOperationException()
        {
            Assert.IsInstanceOf<InvalidOperationException>(ErrorFactory.Default.BenchmarkNotFinished());
        }
    }
}
