using System;

namespace Phnx.Benchmark.Section
{
    /// <summary>
    /// A benchmark of a section of code, marked out by a dispose pattern
    /// </summary>
    public class BenchmarkSection : IDisposable
    {
        private readonly BenchmarkSectionTime _benchmarkSectionTime;

        /// <summary>
        /// Create a new benchmarked section, with a variable to store the execution time
        /// </summary>
        /// <param name="benchmarkSectionTime">Where the execution time will be stored when the section is disposed</param>
        public BenchmarkSection(out BenchmarkSectionTime benchmarkSectionTime)
        {
            benchmarkSectionTime = new BenchmarkSectionTime();
            _benchmarkSectionTime = benchmarkSectionTime;
        }

        /// <summary>
        /// Dipose of this <see cref="BenchmarkSection"/>, and finish the executing <see cref="BenchmarkSectionTime"/>
        /// </summary>
        public void Dispose()
        {
            _benchmarkSectionTime.Finish();
        }
    }
}