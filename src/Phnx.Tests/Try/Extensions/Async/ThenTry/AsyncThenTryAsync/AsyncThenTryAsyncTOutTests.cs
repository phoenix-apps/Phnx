using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace Phnx.Tests.Try.Extensions.Async.ThenTry.AsyncThenTryAsync
{
    using static Factory;

    public class AsyncThenTryAsyncTOutTests
    {
        [Test]
        public void AsyncThenTryAsyncTOut_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            Task<TryResult> t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync<int>(async () => 1));
        }

        [Test]
        public void AsyncThenTryAsyncTOut_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            var t = FailedTask("");
            Func<Task<TryResult<int>>> t2 = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync(t2));
        }

        [Test]
        public async Task AsyncThenTryAsyncTOut_WhenTrySucceeded_Executes()
        {
            var t = SucceedTask();

            var executed = false;
            await t.ThenTryAsync<int>(async () =>
            {
                executed = true;
                return 1;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task AsyncThenTryAsyncTOut_WhenTrySucceeded_ReturnsResultOfExecutor()
        {
            var t = SucceedTask();

            var result = 1;
            var tr = await t.ThenTryAsync<int>(async () => result);

            Assert.AreEqual(result, tr.Result);
        }

        [Test]
        public async Task AsyncThenTryAsyncTOut_WhenTryFailed_DoesNotExecute()
        {
            var t = FailedTask("err");

            var executed = false;
            await t.ThenTryAsync<int>(async () =>
            {
                executed = true;
                return 1;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task AsyncThenTryAsyncTOut_WhenTryFailed_ReturnsOriginalError()
        {
            var err = "err";
            var t = FailedTask(err);

            var t2 = await t.ThenTryAsync<int>(async () => 1);

            Assert.AreEqual(err, t2.ErrorMessage);
        }
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
