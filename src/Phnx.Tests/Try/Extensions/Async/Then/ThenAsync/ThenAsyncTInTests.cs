using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace Phnx.Tests.Try.Extensions.Async.Then.ThenAsync
{
    using static Factory;

    public class ThenAsyncTInTests
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
            Assert.ThrowsAsync<InvalidOperationException>(() => t.ThenAsync(r => string.Empty));
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

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
