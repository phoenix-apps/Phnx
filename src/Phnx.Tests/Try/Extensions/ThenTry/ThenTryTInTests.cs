using NUnit.Framework;
using Phnx.Try;
using System;

namespace Phnx.Tests.Try.Extensions.ThenTry
{
    public class ThenTryTInTests
    {
        [Test]
        public void ThenTryTIn_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            TryResult<int> t = null;
            Assert.Throws<ArgumentNullException>(() => t.ThenTry(r => string.Empty));
        }

        [Test]
        public void ThenTryTIn_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            TryResult<int> t = "";
            Func<int, TryResult> t2 = null;
            Assert.Throws<ArgumentNullException>(() => t.ThenTry(t2));
        }

        [Test]
        public void ThenTryTIn_WhenExecuteResultIsNull_ThrowsInvalidOperationException()
        {
            TryResult<int> t = 1;
            Assert.Throws<InvalidOperationException>(() => t.ThenTry(r => null));
        }

        [Test]
        public void ThenTryTIn_WhenTrySucceeded_Executes()
        {
            TryResult<int> t = 1;

            var executed = false;
            t.ThenTry(() =>
            {
                executed = true;
                return TryResult.Succeed();
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public void ThenTryTIn_WhenTrySucceededAndExecutorFails_ReturnsExecutorError()
        {
            TryResult<int> t = 1;

            var err = "err";
            var tr = t.ThenTry(r => TryResult.Fail(err));

            Assert.AreEqual(err, tr.ErrorMessage);
        }

        [Test]
        public void ThenTryTIn_WhenTrySucceededAndExecutorSucceeds_ReturnsResultOfOriginalTry()
        {
            TryResult<int> t = 1;

            var tr = t.ThenTry(r => TryResult.Succeed());

            Assert.AreEqual(t, tr);
        }

        [Test]
        public void ThenTryTIn_WhenTryFailed_DoesNotExecute()
        {
            TryResult<int> t = "err";

            var executed = false;
            t.ThenTry(() =>
            {
                executed = true;
                return TryResult.Succeed();
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public void ThenTryTIn_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult<int> t = "err";

            var t2 = t.ThenTry(() => TryResult.Succeed());

            Assert.AreEqual(t, t2);
        }
    }
}
