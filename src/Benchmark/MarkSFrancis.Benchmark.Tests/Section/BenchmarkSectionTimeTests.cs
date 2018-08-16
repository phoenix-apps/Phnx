using MarkSFrancis.Benchmark.Section;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Benchmark.Tests.Section
{
    public class BenchmarkSectionTimeTests
    {
        [Test]
        public void GetTimeTaken_WhenNotFinished_ThrowsInvalidOperationException()
        {
            BenchmarkSectionTime time;
            using (new BenchmarkSection(out time))
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    _ = time.TimeTaken;
                });
            }
        }

        [Test]
        public void GetTimeTaken_AfterExceptionInBenchmark_GetsTime()
        {
            BenchmarkSectionTime time = null;
            try
            {
                using (new BenchmarkSection(out time))
                {
                    throw new Exception();
                }
            }
            catch
            {
                Assert.DoesNotThrow(() =>
                {
                    _ = time.TimeTaken;
                });
            }
        }
    }
}
