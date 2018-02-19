using System;
using System.Diagnostics;

namespace MarkSFrancis.Benchmark
{
    public class Benchmark<T>
    {
        public long NumberOfValuesToCompute { get; private set; }

        public Func<long, T> GenerateValueMethod { get; private set; }

        public Action<T>[] ComputeMethods { get; private set; }

        private Action<string> Log { get; set; }

        private bool Verbose => Log != null;

        private T[] GeneratedValues { get; set; }

        public Benchmark(long numberOfValuesToCompute, Func<long, T> generateValue, Action<T>[] computeMethods)
        {
            Initialise(numberOfValuesToCompute, generateValue, null, computeMethods);
        }

        public Benchmark(long numberOfValuesToCompute, Func<long, T> generateValue, Action<string> log, params Action<T>[] computeMethods)
        {
            Initialise(numberOfValuesToCompute, generateValue, log, computeMethods);
        }

        private void Initialise(long numberOfValuesToCompute, Func<long, T> generateValue, Action<string> log, Action<T>[] computeMethods)
        {
            NumberOfValuesToCompute = numberOfValuesToCompute;

            GenerateValueMethod = generateValue;

            ComputeMethods = computeMethods;

            Log = log;
        }

        public void GenerateValues()
        {
            GeneratedValues = new T[NumberOfValuesToCompute];

            LogIfVerbose("Starting to generate values...");

            for (long index = 0; index < NumberOfValuesToCompute; index++)
            {
                GeneratedValues[index] = GenerateValueMethod(index);
            }

            LogIfVerbose("Finished generating values...");
        }

        public TimeSpan[] Run()
        {
            var timings = new TimeSpan[ComputeMethods.Length];

            if (GeneratedValues == null)
            {
                GenerateValues();
            }

            for (var taskIndex = 0; taskIndex < ComputeMethods.Length; taskIndex++)
            {
                LogIfVerbose("Starting benchmark number " + taskIndex);

                Stopwatch stp = new Stopwatch();
                stp.Start();
                foreach (var generatedValue in GeneratedValues)
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