using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

namespace Phnx.Tests.Try.Extensions.Async.Then.AsyncThen
{
    using static Factory;

    public class AsyncThenTInTests
    {
        [Test]
        public void AsyncThenIn_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            Task<TryResult<int>> t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(r => string.Empty));
        }

        [Test]
        public void AsyncThenIn_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            var t = FailedTask<int>("");
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(null));
        }

        [Test]
        public void AsyncThenIn_WhenTryResultResultIsNull_ThrowsInvalidOperationException()
        {
            var t = NullTask<int>();
            Assert.ThrowsAsync<InvalidOperationException>(() => t.ThenAsync(r => { }));
        }

        [Test]
        public async Task AsyncThenIn_WhenTrySucceeded_Executes()
        {
            var t = SucceedTask(1);

            var executed = false;
            await t.ThenAsync(r =>
            {
                executed = true;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task AsyncThenIn_WhenTryFailed_DoesNotExecute()
        {
            var t = FailedTask<int>("err");

            var executed = false;
            await t.ThenAsync(r =>
            {
                executed = true;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task AsyncThenIn_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult<int> tr = "err";
            var t = Task.FromResult(tr);

            var t2 = await t.ThenAsync(r => { });

            Assert.AreEqual(tr, t2);
        }
    }
}
