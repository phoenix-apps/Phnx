using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

namespace Phnx.Tests.Try.Extensions.Async.Then.AsyncThen
{
    using static Factory;

    public class AsyncThenTests
    {
        [Test]
        public void AsyncThen_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            Task<TryResult> t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(() => string.Empty));
        }

        [Test]
        public void AsyncThen_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            var t = FailedTask("");
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(null));
        }

        [Test]
        public void AsyncThen_WhenTryResultResultIsNull_ThrowsInvalidOperationException()
        {
            var t = NullTask();
            Assert.ThrowsAsync<InvalidOperationException>(() => t.ThenAsync(() => { }));
        }

        [Test]
        public async Task AsyncThen_WhenTrySucceeded_Executes()
        {
            var t = SucceedTask();

            var executed = false;
            await t.ThenAsync(() =>
            {
                executed = true;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task AsyncThen_WhenTryFailed_DoesNotExecute()
        {
            var t = FailedTask("err");

            var executed = false;
            await t.ThenAsync(() =>
            {
                executed = true;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task AsyncThen_WhenTryFailed_ReturnsOriginalError()
        {
            var r = TryResult.Fail("err");
            var t = Task.FromResult(r);

            var t2 = await t.ThenAsync(() => { });

            Assert.AreEqual(r, t2);
        }
    }
}
