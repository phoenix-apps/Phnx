using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace Phnx.Tests.Try.Extensions.Async.ThenTry.AsyncThenTryAsync
{
    using static Factory;

    public class AsyncThenTryAsyncTests
    {
        [Test]
        public void AsyncThenTryAsync_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            Task<TryResult> t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync(async () => string.Empty));
        }

        [Test]
        public void AsyncThenTryAsync_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            var t = FailedTask("");
            Func<Task<TryResult>> t2 = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync(t2));
        }

        [Test]
        public void AsyncThenTryAsync_WhenTryResultResultIsNull_ThrowsInvalidOperationException()
        {
            var t = NullTask();
            Assert.ThrowsAsync<InvalidOperationException>(() => t.ThenTryAsync(async () => TryResult.Succeed()));
        }

        [Test]
        public async Task AsyncThenTryAsync_WhenTrySucceeded_Executes()
        {
            var t = SucceedTask();

            var executed = false;
            await t.ThenTryAsync(async () =>
            {
                executed = true;
                return TryResult.Succeed();
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task AsyncThenTryAsync_WhenTryFailed_DoesNotExecute()
        {
            var t = FailedTask("err");

            var executed = false;
            await t.ThenTryAsync(async () =>
            {
                executed = true;
                return TryResult.Succeed();
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task AsyncThenTryAsync_WhenTryFailed_ReturnsOriginalError()
        {
            var r = TryResult.Fail("err");
            var t = Task.FromResult(r);

            var t2 = await t.ThenTryAsync(async () => TryResult.Succeed());

            Assert.AreEqual(r, t2);
        }
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
