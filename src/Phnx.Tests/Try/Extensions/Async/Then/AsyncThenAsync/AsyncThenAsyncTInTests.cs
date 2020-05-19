using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace Phnx.Tests.Try.Extensions.Async.Then.AsyncThenAsync
{
    using static Factory;

    public class AsyncThenAsyncTInTests
    {
        [Test]
        public void AsyncThenAsyncIn_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            Task<TryResult<int>> t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(async r => string.Empty));
        }

        [Test]
        public void AsyncThenAsyncIn_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            var t = FailedTask<int>("");
            Func<int, Task> t2 = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(t2));
        }

        [Test]
        public void AsyncThenAsyncIn_WhenTryResultResultIsNull_ThrowsInvalidOperationException()
        {
            var t = NullTask<int>();
            Assert.ThrowsAsync<InvalidOperationException>(() => t.ThenAsync(async r => { }));
        }

        [Test]
        public async Task AsyncThenAsyncIn_WhenTrySucceeded_Executes()
        {
            var t = SucceedTask(1);

            var executed = false;
            await t.ThenAsync(async r =>
            {
                executed = true;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task AsyncThenAsyncIn_WhenTryFailed_DoesNotExecute()
        {
            var t = FailedTask<int>("err");

            var executed = false;
            await t.ThenAsync(async r =>
            {
                executed = true;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task AsyncThenAsyncIn_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult<int> tr = "err";
            var t = Task.FromResult(tr);

            var t2 = await t.ThenAsync(async r => { });

            Assert.AreEqual(tr, t2);
        }
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
