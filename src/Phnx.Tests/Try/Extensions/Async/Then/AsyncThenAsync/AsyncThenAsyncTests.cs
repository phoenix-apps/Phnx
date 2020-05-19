using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace Phnx.Tests.Try.Extensions.Async.Then.AsyncThenAsync
{
    using static Factory;

    public class AsyncThenAsyncTests
    {
        [Test]
        public void AsyncThenAsync_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            Task<TryResult> t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(async () => string.Empty));
        }

        [Test]
        public void AsyncThenAsync_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            var t = FailedTask("");
            Func<Task> t2 = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(t2));
        }

        [Test]
        public void AsyncThenAsync_WhenTryResultResultIsNull_ThrowsInvalidOperationException()
        {
            var t = NullTask();
            Assert.ThrowsAsync<InvalidOperationException>(() => t.ThenAsync(async () => { }));
        }

        [Test]
        public async Task AsyncThenAsync_WhenTrySucceeded_Executes()
        {
            var t = SucceedTask();

            var executed = false;
            await t.ThenAsync(async () =>
            {
                executed = true;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task AsyncThenAsync_WhenTryFailed_DoesNotExecute()
        {
            var t = FailedTask("err");

            var executed = false;
            await t.ThenAsync(async () =>
            {
                executed = true;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task AsyncThenAsync_WhenTryFailed_ReturnsOriginalError()
        {
            var r = TryResult.Fail("err");
            var t = Task.FromResult(r);

            var t2 = await t.ThenAsync(async () => { });

            Assert.AreEqual(r, t2);
        }
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
