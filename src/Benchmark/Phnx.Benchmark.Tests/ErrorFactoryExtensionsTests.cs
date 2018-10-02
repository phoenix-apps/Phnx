using NUnit.Framework;

namespace Phnx.Benchmark.Tests
{
    public class ErrorMessageExtensionsTests
    {
        [Test]
        public void ErrorMessages_GetBenchmarkNotFinished_GetsMessage()
        {
            string error = ErrorMessage.Factory.BenchmarkNotFinished();
            Assert.IsFalse(string.IsNullOrEmpty(error));
        }
    }
}
