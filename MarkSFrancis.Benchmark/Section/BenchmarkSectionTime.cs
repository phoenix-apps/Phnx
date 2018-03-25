using System;
using System.Diagnostics;

namespace MarkSFrancis.Benchmark.Section
{
    /// <summary>
    /// The amount of time a <see cref="BenchmarkSection"/> took to execute
    /// </summary>
    public class BenchmarkSectionTime
    {
        private TimeSpan _timeTaken;
        private bool _isFinished;
        private readonly Stopwatch _timer;

        internal BenchmarkSectionTime()
        {
            _timer = new Stopwatch();

            _timer.Start();
        }

        internal void Finish()
        {
            _timer.Stop();
            _timeTaken = TimeSpan.FromTicks(_timer.ElapsedTicks);
            _isFinished = true;
        }

        /// <summary>
        /// The amount of time the section took to execute
        /// </summary>
        public TimeSpan TimeTaken
        {
            get
            {
                if (!_isFinished)
                {
                    throw ErrorFactory.Default.BenchmarkNotFinished();
                }

                return _timeTaken;
            }
        }
    }
}
