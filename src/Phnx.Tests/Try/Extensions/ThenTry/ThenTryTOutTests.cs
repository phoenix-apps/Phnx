using NUnit.Framework;
using Phnx.Try;
using System;

namespace Phnx.Tests.Try.Extensions.ThenTry
{
    public class ThenTryTOutTests
    {
        [Test]
        public void ThenTryTOut_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            TryResult t = null;
            Assert.Throws<ArgumentNullException>(() => t.ThenTry<int>(() => 1));
        }

        [Test]
        public void ThenTryTOut_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            TryResult t = "";
            Assert.Throws<ArgumentNullException>(() => t.ThenTry<int>(null));
        }

        [Test]
        public void ThenTryTOut_WhenTrySucceeded_Executes()
        {
            var t = TryResult.Succeed();

            var executed = false;
            t.ThenTry<int>(() =>
            {
                executed = true;
                return 1;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public void ThenTryTOut_WhenTrySucceededAndExecutorFails_ReturnsExecutorError()
        {
            var t = TryResult.Succeed();

            var err = "err";
            var tr = t.ThenTry<int>(() => err);

            Assert.AreEqual(err, tr.ErrorMessage);
        }

        [Test]
        public void ThenTryTOut_WhenTrySucceededAndExecutorSucceeds_ReturnsResultOfExecutor()
        {
            var t = TryResult.Succeed();

            var result = 1;
            var tr = t.ThenTry<int>(() => result);

            Assert.AreEqual(result, tr.Result);
        }

        [Test]
        public void ThenTryTOut_WhenTryFailed_DoesNotExecute()
        {
            TryResult t = "err";

            var executed = false;
            t.ThenTry<int>(() =>
            {
                executed = true;
                return 1;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public void ThenTryTOut_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult t = "err";

            var t2 = t.ThenTry<int>(() => 1);

            Assert.AreEqual(t.ErrorMessage, t2.ErrorMessage);
        }
    }
}
