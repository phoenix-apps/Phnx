using System;

namespace Phnx
{
    /// <summary>
    /// Extensions for <see cref="ErrorMessage"/>
    /// </summary>
    internal static class BenchmarkErrorMessageExtensions
    {
        /// <summary>
        /// An error that explains that the benchmark's time cannot be retrieved until the section finishes executing
        /// </summary>
        /// <param name="errors">The <see cref="ErrorMessage"/> to extend</param>
        /// <returns>An <see cref="InvalidOperationException"/>, describing that the benchmark time cannot be retrieved until the benchmark is finished</returns>
        public static string BenchmarkNotFinished(this ErrorMessage errors)
        {
            return "The time of the benchmark cannot be retrieved until the benchmark is completed";
        }
    }
}
