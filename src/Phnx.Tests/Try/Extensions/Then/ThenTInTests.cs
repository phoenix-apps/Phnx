using NUnit.Framework;
using Phnx.Try;
using System;

namespace Phnx.Tests.Try.Extensions.Then
{
    public class ThenTInTests
    {
        [Test]
        public void ThenTIn_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            TryResult<int> t = null;
            Assert.Throws<ArgumentNullException>(() => t.Then(() => string.Empty));
        }

        [Test]
        public void ThenTIn_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            TryResult<int> t = "";
            Assert.Throws<ArgumentNullException>(() => t.Then(null));
        }

        [Test]
        public void ThenTIn_WhenTrySucceeded_Executes()
        {
            TryResult<int> t = 1;

            var executed = false;
            t.Then(() =>
            {
                executed = true;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public void ThenTIn_WhenTryFailed_DoesNotExecute()
        {
            TryResult<int> t = "err";

            var executed = false;
            t.Then(() =>
            {
                executed = true;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public void ThenTIn_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult<int> t = "err";

            var t2 = t.Then(() => { });

            Assert.AreEqual(t, t2);
        }
    }
}
