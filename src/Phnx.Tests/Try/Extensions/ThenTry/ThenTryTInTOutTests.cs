using NUnit.Framework;
using Phnx.Try;
using System;

namespace Phnx.Tests.Try.Extensions.ThenTry
{
    public class ThenTryTInTOutTests
    {
        [Test]
        public void ThenTryTInTOut_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            TryResult<int> t = null;
            Assert.Throws<ArgumentNullException>(() => t.ThenTry<int, int>(r => 1));
        }

        [Test]
        public void ThenTryTInTOut_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            TryResult<int> t = "";
            Assert.Throws<ArgumentNullException>(() => t.ThenTry<int, int>(null));
        }

        [Test]
        public void ThenTryTInTOut_WhenTrySucceeded_Executes()
        {
            TryResult<int> t = 1;

            var executed = false;
            t.ThenTry<int, int>(r =>
            {
                executed = true;
                return 1;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public void ThenTryTInTOut_WhenTrySucceededAndExecutorFails_ReturnsExecutorError()
        {
            TryResult<int> t = 1;

            var err = "err";
            var tr = t.ThenTry<int, int>(r => err);

            Assert.AreEqual(err, tr.ErrorMessage);
        }

        [Test]
        public void ThenTryTInTOut_WhenTrySucceededAndExecutorSucceeds_ReturnsResultOfExecutor()
        {
            TryResult<int> t = 1;

            var result = 5;
            var tr = t.ThenTry<int, int>(r => result);

            Assert.AreEqual(result, tr.Result);
        }

        [Test]
        public void ThenTryTInTOut_WhenTryFailed_DoesNotExecute()
        {
            TryResult<int> t = "err";

            var executed = false;
            t.ThenTry<int, int>(r =>
            {
                executed = true;
                return 1;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public void ThenTryTInTOut_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult<int> t = "err";

            var t2 = t.ThenTry<int, int>(r => 1);

            Assert.AreEqual(t.Result, t2.Result);
        }
    }
}
