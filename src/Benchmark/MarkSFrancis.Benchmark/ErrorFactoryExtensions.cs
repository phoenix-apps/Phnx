using MarkSFrancis.ThrowHelpers;
using System;

namespace MarkSFrancis.Benchmark
{
    /// <summary>
    /// Extensions for <see cref="ErrorFactory"/>
    /// </summary>
    public static partial class ErrorFactoryExtensions
    {
        /// <summary>
        /// An error that explains that the benchmark's time cannot be retrieved until the section finishes executing
        /// </summary>
        /// <param name="factory">The <see cref="ErrorFactory"/> to extend</param>
        /// <returns>An <see cref="InvalidOperationException"/>, describing that the benchmark time cannot be retrieved until the benchmark is finished</returns>
        public static IThrowHelper BenchmarkNotFinished(this ErrorFactory factory)
        {
            return new ThrowHelper<InvalidOperationException>("The time of the benchmark cannot be retrieved until the benchmark is completed");
        }
    }
}
