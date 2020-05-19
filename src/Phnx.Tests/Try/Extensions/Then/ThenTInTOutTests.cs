using NUnit.Framework;
using Phnx.Try;
using System;

namespace Phnx.Tests.Try.Extensions.Then
{
    public class ThenTInTOutTests
    {
        [Test]
        public void ThenTInTOut_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            TryResult<int> t = null;
            Assert.Throws<ArgumentNullException>(() => t.Then(r => 1));
        }

        [Test]
        public void ThenTInTOut_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            TryResult<int> t = "";
            Assert.Throws<ArgumentNullException>(() => t.Then<int, int>(null));
        }

        [Test]
        public void ThenTInTOut_WhenTrySucceeded_Executes()
        {
            TryResult<int> t = 1;

            var executed = false;
            t.Then(r =>
            {
                executed = true;
                return 1;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public void ThenTInTOut_WhenTryFailed_DoesNotExecute()
        {
            TryResult<int> t = "err";

            var executed = false;
            t.Then(r =>
            {
                executed = true;
                return 1;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public void ThenTInTOut_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult<int> t = "err";

            var t2 = t.Then(r => 1);

            Assert.AreEqual(t.ErrorMessage, t2.ErrorMessage);
        }
    }
}
