using System;
using System.Diagnostics;

namespace MarkSFrancis.Benchmark
{
    /// <summary>
    /// Benchmark a function or series of functions by generating values and then sending those values to each function, timing how long each function takes to compute all values
    /// </summary>
    /// <typeparam name="T">The type passed to each function as an input</typeparam>
    public class Benchmark<T>
    {
        /// <summary>
        /// The number of values to generate/ compute
        /// </summary>
        public long NumberOfValuesToCompute => BenchmarkWithValues.Length;

        /// <summary>
        /// The methods that are being benchmarked
        /// </summary>
        public Action<T>[] ComputeMethods { get; private set; }

        /// <summary>
        /// The method that logs events
        /// </summary>
        private Action<string> Log { get; set; }

        /// <summary>
        /// Whether to log events
        /// </summary>
        private bool Verbose => Log != null;

        /// <summary>
        /// The values to pass to the <see cref="ComputeMethods"/> when benchmarking
        /// </summary>
        public T[] BenchmarkWithValues { get; private set; }

        /// <summary>
        /// Create a new <see cref="Benchmark{T}"/> without writing steps to the log
        /// </summary>
        /// <param name="computeMethods">The methods to benchmark</param>
        /// <param name="numberOfValuesToCompute">The number of values to be computed by each of the <see cref="ComputeMethods"/></param>
        /// <param name="generateValue">The function used to generate values. The number generated so far is passed in</param>
        public Benchmark(Action<T>[] computeMethods, Func<long, T> generateValue, long numberOfValuesToCompute)
        {
            ComputeMethods = computeMethods;

            GenerateValues(generateValue, numberOfValuesToCompute);
        }

        /// <summary>
        /// Create a new <see cref="Benchmark{T}"/> without writing steps to the log
        /// </summary>
        /// <param name="computeMethods">The methods to benchmark</param>
        /// <param name="benchmarkWithValues">The values to pass to the <see cref="ComputeMethods"/> when benchmarking</param>
        public Benchmark(Action<T>[] computeMethods, T[] benchmarkWithValues)
        {
            ComputeMethods = computeMethods;

            BenchmarkWithValues = benchmarkWithValues;
        }

        /// <summary>
        /// Create a new <see cref="Benchmark{T}"/> without writing steps to the log
        /// </summary>
        /// <param name="computeMethods">The methods to benchmark</param>
        /// <param name="numberOfValuesToCompute">The number of values to be computed by each of the <see cref="ComputeMethods"/></param>
        /// <param name="generateValue">The function used to generate values. The number generated so far is passed in</param>
        /// <param name="log">The function called when events such as "values generated" occur, with the event description passed in</param>
        public Benchmark(Action<T>[] computeMethods, Func<long, T> generateValue, long numberOfValuesToCompute, Action<string> log)
        {
            ComputeMethods = computeMethods;

            GenerateValues(generateValue, numberOfValuesToCompute);

            Log = log;
        }

        /// <summary>
        /// Generates a number of values using a function
        /// </summary>
        /// <param name="generateValue">The function used to generate values. The number of values generated so far is passed in as a parameter to the function</param>
        /// <param name="numberOfValuesToGenerate">The number of values to generate</param>
        private void GenerateValues(Func<long, T> generateValue, long numberOfValuesToGenerate)
        {
            BenchmarkWithValues = new T[numberOfValuesToGenerate];

            LogIfVerbose("Starting to generate values...");

            for (long index = 0; index < numberOfValuesToGenerate; index++)
            {
                BenchmarkWithValues[index] = generateValue(index);
            }

            LogIfVerbose("Finished generating values...");
        }

        /// <summary>
        /// Benchmarks all methods, and returns how long each of the <see cref="ComputeMethods"/> took to compute all <see cref="BenchmarkWithValues"/> in a <see cref="TimeSpan"/> array
        /// </summary>
        /// <returns>How long each <see cref="ComputeMethods"/> took to process all values</returns>
        public TimeSpan[] Run()
        {
            var timings = new TimeSpan[ComputeMethods.Length];
            
            for (var taskIndex = 0; taskIndex < ComputeMethods.Length; taskIndex++)
            {
                LogIfVerbose("Starting benchmark number " + taskIndex);

                Stopwatch stp = new Stopwatch();
                stp.Start();
                foreach (var generatedValue in BenchmarkWithValues)
                {
                    ComputeMethods[taskIndex](generatedValue);
                }

                stp.Stop();

                timings[taskIndex] = new TimeSpan(stp.ElapsedTicks);

                LogIfVerbose("Task " + taskIndex + " completed");

                LogIfVerbose("Total time taken: " +
                    timings[taskIndex].ToString("c"));
            }

            return timings;
        }

        private void LogIfVerbose(string writeToLog)
        {
            if (Verbose)
            {
                Log(writeToLog);
            }
        }
    }
}
