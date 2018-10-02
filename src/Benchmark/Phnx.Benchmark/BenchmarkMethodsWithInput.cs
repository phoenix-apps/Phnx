using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Phnx.Benchmark
{
    /// <summary>
    /// Benchmark a function or series of functions by generating values and then sending those values to each function, timing how long each function takes to compute all values
    /// </summary>
    /// <typeparam name="T">The type passed to each function as an input</typeparam>
    public class BenchmarkMethods<T>
    {
        /// <summary>
        /// The methods that are being benchmarked
        /// </summary>
        private readonly Action<T>[] _computeMethods;

        /// <summary>
        /// The methods that are being benchmarked
        /// </summary>
        public IEnumerable<Action<T>> ComputeMethods => _computeMethods;

        /// <summary>
        /// The method that logs events
        /// </summary>
        private readonly Action<string> _log;

        /// <summary>
        /// Whether to log events
        /// </summary>
        private bool WriteToLog => _log != null;

        /// <summary>
        /// Create a new <see cref="BenchmarkMethods{T}"/>
        /// </summary>
        /// <param name="computeMethods">The methods to benchmark</param>
        /// <param name="log">The function called when events such as "values generated" occur, with the event description passed in</param>
        public BenchmarkMethods(Action<T>[] computeMethods, Action<string> log = null)
        {
            _computeMethods = computeMethods;

            _log = log;
        }

        /// <summary>
        /// Benchmarks all methods, and returns how long each of the <see cref="ComputeMethods"/> took to compute all the <paramref name="benchmarkWithValues"/>
        /// </summary>
        /// <param name="benchmarkWithValues">The values to pass to each of the <see cref="ComputeMethods"/> when benchmarking them</param>
        /// <returns>How long each of the <see cref="ComputeMethods"/> took to process all values</returns>
        public TimeSpan[] Run(IEnumerable<T> benchmarkWithValues)
        {
            var timings = new TimeSpan[_computeMethods.Length];

            for (var taskIndex = 0; taskIndex < _computeMethods.Length; taskIndex++)
            {
                LogIfVerbose("Starting benchmark number " + taskIndex + ". ");

                var actionExecutionTimer = new Stopwatch();
                actionExecutionTimer.Start();

                foreach (var generatedValue in benchmarkWithValues)
                {
                    _computeMethods[taskIndex](generatedValue);
                }

                actionExecutionTimer.Stop();

                timings[taskIndex] = new TimeSpan(actionExecutionTimer.ElapsedTicks);

                LogIfVerbose("Task " + taskIndex + " completed. ");

                LogIfVerbose("Total time taken: " +
                             timings[taskIndex].ToString("c") + ". ");
            }

            return timings;
        }

        /// <summary>
        /// Benchmarks all methods, and returns how long each of the <see cref="ComputeMethods"/> took to compute all the <paramref name="generateValue"/>s
        /// </summary>
        /// <param name="generateValue">The method used to generate values to pass to each of the <see cref="ComputeMethods"/> when benchmarking them. The number of values generated so far is passed as a parameter</param>
        /// <param name="numberOfValuesToGenerate">The number of values to generate with <paramref name="generateValue"/></param>
        /// <returns>How long each of the <see cref="ComputeMethods"/> took to process all values</returns>
        public TimeSpan[] Run(Func<long, T> generateValue, long numberOfValuesToGenerate)
        {
            if (generateValue == null)
            {
                throw new ArgumentNullException(nameof(generateValue));
            }

            var values = new T[numberOfValuesToGenerate];

            GenerateValues(generateValue, values);

            return Run(values);
        }

        /// <summary>
        /// Generates a number of values using a function
        /// </summary>
        /// <param name="generateValue">The function used to generate values. The number of values generated so far is passed in as a parameter to the function</param>
        /// <param name="arr">The array to fill with generated values</param>
        private void GenerateValues(Func<long, T> generateValue, T[] arr)
        {
            LogIfVerbose("Starting to generate values...");

            for (long index = 0; index < arr.Length; index++)
            {
                arr[index] = generateValue(index);
            }

            LogIfVerbose("Finished generating values...");
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
