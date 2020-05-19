using NUnit.Framework;
using Phnx.Try;
using System;

namespace Phnx.Tests.Try.Extensions.Then
{
    public class ThenTests
    {
        [Test]
        public void Then_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            TryResult t = null;
            Assert.Throws<ArgumentNullException>(() => t.Then(() => string.Empty));
        }

        [Test]
        public void Then_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            TryResult t = "";
            Assert.Throws<ArgumentNullException>(() => t.Then(null));
        }

        [Test]
        public void Then_WhenTrySucceeded_Executes()
        {
            var t = TryResult.Succeed();

            var executed = false;
            t.Then(() =>
            {
                executed = true;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public void Then_WhenTryFailed_DoesNotExecute()
        {
            TryResult t = "err";

            var executed = false;
            t.Then(() =>
            {
                executed = true;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public void Then_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult t = "err";

            var t2 = t.Then(() => { });

            Assert.AreEqual(t, t2);
        }
    }
}
