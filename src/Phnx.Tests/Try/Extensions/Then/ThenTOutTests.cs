using NUnit.Framework;
using Phnx.Try;
using System;

namespace Phnx.Tests.Try.Extensions.Then
{
    public class ThenTOutTests
    {
        [Test]
        public void ThenTOut_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            TryResult t = null;
            Assert.Throws<ArgumentNullException>(() => t.Then(() => 1));
        }

        [Test]
        public void ThenTOut_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            TryResult t = "";
            Assert.Throws<ArgumentNullException>(() => t.Then<int>(null));
        }

        [Test]
        public void ThenTOut_WhenTrySucceeded_Executes()
        {
            var t = TryResult.Succeed();

            var executed = false;
            t.Then(() =>
            {
                executed = true;
                return 1;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public void ThenTOut_WhenTrySucceeded_ReturnsResultOfExecutor()
        {
            var t = TryResult.Succeed();

            var result = "yay";
            var tr = t.Then(() => result);

            Assert.AreEqual(result, tr.Result);
        }

        [Test]
        public void ThenTOut_WhenTryFailed_DoesNotExecute()
        {
            TryResult t = "err";

            var executed = false;
            t.Then(() =>
            {
                executed = true;
                return 1;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public void ThenTOut_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult t = "err";

            var t2 = t.Then(() => 1);

            Assert.AreEqual(t.ErrorMessage, t2.ErrorMessage);
        }
    }
}
