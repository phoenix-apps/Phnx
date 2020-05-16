using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Phnx.IO.Threaded.Tests
{
    public class FuncSyncEventTests
    {
        [Test]
        public void New_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new FuncSyncEvent());
        }

        private bool TimesOut(Action action, int timeout)
        {
            var timedOut = true;

            var actionTask = Task.Run(() =>
            {
                action();
                timedOut = false;
            });

            var delayTask = Task.Delay(timeout);

            var tasks = Task.WhenAny(actionTask, delayTask);

            tasks.Wait();

            if (actionTask.IsFaulted)
            {
                // Pass the error up
                throw actionTask.Exception;
            }

            return timedOut;
        }

        [Test]
        public void WaitUntil_WithNullFunc_ThrowsArgumentNullException()
        {
            var sync = new FuncSyncEvent();

            Assert.Throws<ArgumentNullException>(() => sync.WaitUntil(null, 0));
        }

        [Test]
        public void WaitUntilWithTimeout_WithNullFunc_ThrowsArgumentNullException()
        {
            var sync = new FuncSyncEvent();

            Assert.Throws<ArgumentNullException>(() => sync.WaitUntil(null, 0, 0));
        }

        [Test]
        public void WaitUntil_WithNegativeReevaluateTime_ThrowsArgumentOutOfRangeException()
        {
            var sync = new FuncSyncEvent();

            Assert.Throws<ArgumentOutOfRangeException>(() => sync.WaitUntil(() => true, -1));
        }

        [Test]
        public void WaitUntilWithTimeout_WithNegativeReevaluateTime_ThrowsArgumentOutOfRangeException()
        {
            var sync = new FuncSyncEvent();

            Assert.Throws<ArgumentOutOfRangeException>(() => sync.WaitUntil(() => true, -1, 0));
        }

        [Test]
        public void WaitUntilWithTimeout_WithTimeoutLessThanMinus1_ThrowsArgumentOutOfRangeException()
        {
            var sync = new FuncSyncEvent();

            Assert.Throws<ArgumentOutOfRangeException>(() => sync.WaitUntil(() => true, 0, -2));
        }

        [Test]
        public void WaitUntilWithTimeout_WithInfiniteTimeout_ThatReturnsFalse_DoesNotExit()
        {
            var sync = new FuncSyncEvent();

            bool functionNeverFinished = TimesOut(() => sync.WaitUntil(() => false, 0, Timeout.Infinite), 10);

            Assert.IsTrue(functionNeverFinished);
        }

        [Test]
        public void WaitUntilWithTimeout_WithInfiniteTimeout_ThatReturnsTrue_Exits()
        {
            var sync = new FuncSyncEvent();
            var internalTimeout = true;

            bool timedOut = TimesOut(() => internalTimeout = !sync.WaitUntil(() => true, 0, Timeout.Infinite), 10);

            Assert.IsFalse(timedOut);
            Assert.IsFalse(internalTimeout);
        }

        [Test]
        public void WaitUntil_ThatReturnsTrueWithoutTimeout_Exits()
        {
            var sync = new FuncSyncEvent();

            bool timedOut = TimesOut(() => sync.WaitUntil(() => true, 0), 10);

            Assert.IsFalse(timedOut);
        }

        [Test]
        public void WaitUntil_ThatReturnsFalseWithoutTimeout_DoesNotExit()
        {
            var sync = new FuncSyncEvent();

            bool timedOut = TimesOut(() => sync.WaitUntil(() => false, 0), 10);

            Assert.IsTrue(timedOut);
        }

        [Test]
        public void WaitUntil_ThatReturnsTrueWithTimeout_Exits()
        {
            var sync = new FuncSyncEvent();
            var internalTimedOut = false;

            bool timedOut = TimesOut(() => internalTimedOut = !sync.WaitUntil(() => true, 0, 100), 100);

            Assert.IsFalse(timedOut);
            Assert.IsFalse(internalTimedOut);
        }

        [Test]
        public void WaitUntil_ThatReturnsFalseWithTimeout_TimesOut()
        {
            var sync = new FuncSyncEvent();
            var internalTimedOut = true;

            bool timedOut = TimesOut(() => internalTimedOut = !sync.WaitUntil(() => false, 1, 10), 100);

            Assert.IsFalse(timedOut);
            Assert.IsTrue(internalTimedOut);
        }

        [Test]
        public void WaitUntil_WithoutTimeoutAndWithTwoEvaluationsNeeded_UsesSpinsAndSkipsReevaluateTimers()
        {
            var sync = new FuncSyncEvent();
            var evaluatedOnce = false;

            bool timedOut = TimesOut(() => sync.WaitUntil(() =>
            {
                evaluatedOnce = !evaluatedOnce;
                return !evaluatedOnce;
            }, 0), 100);

            Assert.IsFalse(timedOut);
        }

        [Test]
        public void WaitUntil_WithTimeoutAndTwoEvaluationsNeeded_UsesSpinsAndSkipsReevaluateTimers()
        {
            var sync = new FuncSyncEvent();
            var evaluatedOnce = false;

            bool timedOut = TimesOut(() => sync.WaitUntil(() =>
            {
                evaluatedOnce = !evaluatedOnce;
                return !evaluatedOnce;
            }, 0, 1000), 50);

            Assert.IsFalse(timedOut);
        }
    }
}
