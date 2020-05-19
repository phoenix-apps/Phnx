using NUnit.Framework;
using Phnx.Try;
using System;

namespace Phnx.Tests.Try.Extensions.ThenTry
{
    public class ThenTryTests
    {
        [Test]
        public void ThenTry_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            TryResult t = null;
            Assert.Throws<ArgumentNullException>(() => t.ThenTry(() => string.Empty));
        }

        [Test]
        public void ThenTry_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            TryResult t = "";
            Assert.Throws<ArgumentNullException>(() => t.ThenTry(null));
        }

        [Test]
        public void ThenTry_WhenTrySucceeded_Executes()
        {
            var t = TryResult.Succeed();

            var executed = false;
            t.ThenTry(() =>
            {
                executed = true;
                return TryResult.Succeed();
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public void ThenTry_WhenTrySucceeded_ReturnsResultOfExecutor()
        {
            var t = TryResult.Succeed();

            var err = "err";
            t = t.ThenTry(() => TryResult.Fail(err));

            Assert.AreEqual(err, t.ErrorMessage);
        }

        [Test]
        public void ThenTry_WhenTryFailed_DoesNotExecute()
        {
            TryResult t = "err";

            var executed = false;
            t.ThenTry(() =>
            {
                executed = true;
                return TryResult.Succeed();
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public void ThenTry_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult t = "err";

            var t2 = t.ThenTry(() => TryResult.Succeed());

            Assert.AreEqual(t, t2);
        }
    }
}
