using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace Phnx.Tests.Try.Extensions.Async.ThenTry.AsyncThenTryAsync
{
    using static Factory;

    public class AsyncThenTryAsyncTInTests
    {
        [Test]
        public void AsyncThenTryAsyncIn_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            Task<TryResult<int>> t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync(async r => string.Empty));
        }

        [Test]
        public void AsyncThenTryAsyncIn_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            var t = FailedTask<int>("");
            Func<int, Task<TryResult>> t2 = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync(t2));
        }

        [Test]
        public void AsyncThenTryAsyncIn_WhenTryResultResultIsNull_ThrowsInvalidOperationException()
        {
            var t = NullTask<int>();
            Assert.ThrowsAsync<InvalidOperationException>(() => t.ThenTryAsync(async r => TryResult.Succeed()));
        }

        [Test]
        public async Task AsyncThenTryAsyncIn_WhenTrySucceeded_Executes()
        {
            var t = SucceedTask(1);

            var executed = false;
            await t.ThenTryAsync(async r =>
            {
                executed = true;
                return TryResult.Succeed();
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task AsyncThenTryAsyncIn_WhenTryFailed_DoesNotExecute()
        {
            var t = FailedTask<int>("err");

            var executed = false;
            await t.ThenTryAsync(async r =>
            {
                executed = true;
                return TryResult.Succeed();
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task AsyncThenTryAsyncIn_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult<int> tr = "err";
            var t = Task.FromResult(tr);

            var t2 = await t.ThenTryAsync(async r => TryResult.Succeed());

            Assert.AreEqual(tr, t2);
        }
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
