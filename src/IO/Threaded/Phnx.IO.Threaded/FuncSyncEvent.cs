using System;
using System.Diagnostics;
using System.Threading;

namespace Phnx.IO.Threaded
{
    /// <summary>
    /// Provides a way to synchronize threads based on a function. Uses <see cref="SpinWait"/> for the first 2ms of a wait, then falls back onto <see cref="Monitor"/> for syncronization
    /// </summary>
    public class FuncSyncEvent
    {
        private readonly Stopwatch _stopwatch;

        /// <summary>
        /// Create a new <see cref="FuncSyncEvent"/>
        /// </summary>
        public FuncSyncEvent()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        /// <summary>
        /// Wait until <paramref name="untilThisIsTrue"/> returns <see langword="true"/>
        /// </summary>
        /// <param name="untilThisIsTrue">The function to evaluate to see whether to wait</param>
        /// <param name="reevaluateAfterMilliseconds">How long to wait after an evaluation results <see langword="false"/> before reevaluating again</param>
        /// <exception cref="ArgumentNullException"><paramref name="untilThisIsTrue"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="reevaluateAfterMilliseconds"/> is less than zero</exception>
        public void WaitUntil(Func<bool> untilThisIsTrue, int reevaluateAfterMilliseconds)
        {
            if (untilThisIsTrue is null)
            {
                throw new ArgumentNullException(nameof(untilThisIsTrue));
            }

            if (reevaluateAfterMilliseconds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(reevaluateAfterMilliseconds));
            }

            if (untilThisIsTrue())
            {
                return;
            }

            // Busy wait. Blocks a thread, but much more responsive than Thread.Sleep
            if (SpinWait.SpinUntil(untilThisIsTrue, 10))
            {
                return;
            }

            // Fall back to Monitor
            while (!untilThisIsTrue())
            {
                Thread.Sleep(reevaluateAfterMilliseconds);
            }
        }

        /// <summary>
        /// Wait until <paramref name="untilThisIsTrue"/> returns <see langword="true"/>, or <paramref name="millisecondsTimeout"/> has elapsed
        /// </summary>
        /// <param name="untilThisIsTrue">The function to evaluate to see whether to wait</param>
        /// <param name="reevaluateAfterMilliseconds">How long to wait after an evaluation results <see langword="false"/> before reevaluating again</param>
        /// <param name="millisecondsTimeout">The time after which to exit, regardless of the value of <paramref name="untilThisIsTrue"/>. Set to <see cref="Timeout.Infinite"/> to never timeout</param>
        /// <returns><see langword="true"/> if it exited because <paramref name="untilThisIsTrue"/> returned <see langword="true"/>, <see langword="false"/> if a timeout occured</returns>
        /// <exception cref="ArgumentNullException"><paramref name="untilThisIsTrue"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="reevaluateAfterMilliseconds"/> is less than zero or <paramref name="millisecondsTimeout"/> is less than -1</exception>
        public bool WaitUntil(Func<bool> untilThisIsTrue, int reevaluateAfterMilliseconds, int millisecondsTimeout)
        {
            long startTimeMilliseconds = _stopwatch.ElapsedMilliseconds;

            if (untilThisIsTrue is null)
            {
                throw new ArgumentNullException(nameof(untilThisIsTrue));
            }

            if (reevaluateAfterMilliseconds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(reevaluateAfterMilliseconds));
            }

            // -1 = Timeout.Infinite
            if (millisecondsTimeout < -1)
            {
                throw new ArgumentOutOfRangeException(nameof(millisecondsTimeout), "Cannot be less than -1");
            }

            if (millisecondsTimeout == Timeout.Infinite)
            {
                // Don't use timeout
                WaitUntil(untilThisIsTrue, reevaluateAfterMilliseconds);

                // Could not have timed out
                return true;
            }

            if (untilThisIsTrue())
            {
                return true;
            }

            // Busy wait based on SpinWait. Blocks a thread, but much more responsive than Thread.Sleep
            while (_stopwatch.ElapsedMilliseconds - startTimeMilliseconds < 2)
            {
                if (untilThisIsTrue())
                {
                    return true;
                }
            }

            var value = false;

            // Fall back to Thread.Sleep
            while (!value && _stopwatch.ElapsedMilliseconds - startTimeMilliseconds < millisecondsTimeout)
            {
                Thread.Sleep(reevaluateAfterMilliseconds);

                value = untilThisIsTrue();
            }

            return value;
        }
    }
}
