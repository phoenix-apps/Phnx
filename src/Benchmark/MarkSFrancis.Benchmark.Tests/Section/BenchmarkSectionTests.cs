using MarkSFrancis.Benchmark.Section;
using NUnit.Framework;
using System.Threading;

namespace MarkSFrancis.Benchmark.Tests.Section
{
    public class BenchmarkSectionTests
    {
        [Test]
        public void BenchmarkSection_Disposed_StartsAndStopsTimer()
        {
            BenchmarkSectionTime time;
            using (new BenchmarkSection(out time))
            {
                Thread.Sleep(2);
            }

            // Ensure time is not zero
            var timeTaken = time.TimeTaken.Ticks;
            Assert.AreNotEqual(0, timeTaken);

            // Ensure timer is not still running
            Thread.Sleep(2);
            Assert.AreEqual(timeTaken, time.TimeTaken.Ticks);
        }
    }
}
