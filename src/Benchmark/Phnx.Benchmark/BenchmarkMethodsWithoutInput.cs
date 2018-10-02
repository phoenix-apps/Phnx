using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Phnx.Benchmark
{
    /// <summary>
    /// Benchmark a function or series of functions, timing how long each function takes to run a certain number of times
    /// </summary>
    public class BenchmarkMethods
    {
        /// <summary>
        /// The methods that are being benchmarked
        /// </summary>
        private readonly Action[] _computeMethods;

        /// <summary>
        /// The methods that are being benchmarked
        /// </summary>
        public IEnumerable<Action> ComputeMethods => _computeMethods;

        /// <summary>
        /// The method that logs events
        /// </summary>
        private readonly Action<string> _log;

        /// <summary>
        /// Whether to log events
        /// </summary>
        private bool WriteToLog => _log != null;

        /// <summary>
        /// Create a new <see cref="BenchmarkMethods"/>
        /// </summary>
        /// <param name="computeMethods">The methods to benchmark</param>
        /// <param name="log">The function called when events such as "values generated" occur, with the event description passed in</param>
        public BenchmarkMethods(Action[] computeMethods, Action<string> log = null)
        {
            _computeMethods = computeMethods;
            _log = log;
        }

        /// <summary>
        /// Benchmarks all methods, and returns how long each of the <see cref="ComputeMethods"/> took to compute all methods in a <see cref="TimeSpan"/> array
        /// </summary>
        /// <returns>How long each <see cref="_computeMethods"/> took to process all values</returns>
        public TimeSpan[] Run(long timesToExecuteEachMethod = 1)
        {
            var timings = new TimeSpan[_computeMethods.Length];

            for (var taskIndex = 0; taskIndex < _computeMethods.Length; taskIndex++)
            {
                LogIfVerbose("Starting benchmark number " + taskIndex);

                Stopwatch stp = new Stopwatch();
                stp.Start();

                for(int timesExecuted = 0; timesExecuted < timesToExecuteEachMethod; ++timesExecuted)
                {
                    _computeMethods[taskIndex]();
                }

                stp.Stop();

                timings[taskIndex] = new TimeSpan(stp.ElapsedTicks);

                LogIfVerbose($"Benchmark {taskIndex} completed");

                LogIfVerbose("Total time taken: " +
                             timings[taskIndex].ToString("c"));
            }

            return timings;
        }

        /// <summary>
        /// Logs to the logging method if <see cref="WriteToLog"/> is <see langword="true"/>
        /// </summary>
        /// <param name="message">The message to log</param>
        protected void LogIfVerbose(string message)
        {
            if (WriteToLog)
            {
                _log(message);
            }
        }
    }
}
