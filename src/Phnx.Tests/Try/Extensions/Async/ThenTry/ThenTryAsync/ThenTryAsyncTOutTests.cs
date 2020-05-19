using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace Phnx.Tests.Try.Extensions.Async.ThenTry.ThenTryAsync
{
    public class ThenTryAsyncTOutTests
    {
        [Test]
        public void ThenTryAsyncTOut_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            TryResult t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync<int>(async () => 1));
        }

        [Test]
        public void ThenTryAsyncTOut_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            TryResult t = "";
            Func<Task<TryResult<int>>> t2 = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync(t2));
        }

        [Test]
        public async Task ThenTryAsyncTOut_WhenTrySucceeded_Executes()
        {
            var t = TryResult.Succeed();

            var executed = false;
            await t.ThenTryAsync<int>(async () =>
            {
                executed = true;
                return 1;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task ThenTryAsyncTOut_WhenTrySucceeded_ReturnsResultOfExecutor()
        {
            var t = TryResult.Succeed();

            var result = 1;
            var tr = await t.ThenTryAsync<int>(async () => result);

            Assert.AreEqual(result, tr.Result);
        }

        [Test]
        public async Task ThenTryAsyncTOut_WhenTryFailed_DoesNotExecute()
        {
            TryResult t = "err";

            var executed = false;
            await t.ThenTryAsync<int>(async () =>
            {
                executed = true;
                return 1;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task ThenTryAsyncTOut_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult t = "err";

            var t2 = await t.ThenTryAsync<int>(async () => 1);

            Assert.AreEqual(t.ErrorMessage, t2.ErrorMessage);
        }
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
