using System;

namespace MarkSFrancis.Benchmark
{
    /// <summary>
    /// Extensions for <see cref="ErrorFactory"/>
    /// </summary>
    public static class ErrorFactoryExtensions
    {
        /// <summary>
        /// An error that explains that the benchmark's time cannot be retrieved until the section finishes executing
        /// </summary>
        /// <param name="factory">The factory to extend</param>
        /// <returns></returns>
        public static InvalidOperationException BenchmarkNotFinished(this ErrorFactory factory)
        {
            return new InvalidOperationException("The time of the benchmark cannot be retrieved until the benchmark is completed");
        }
    }
}
