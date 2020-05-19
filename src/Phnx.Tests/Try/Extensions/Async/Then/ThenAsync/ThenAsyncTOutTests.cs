using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace Phnx.Tests.Try.Extensions.Async.Then.ThenAsync
{
    public class ThenAsyncTOutTests
    {
        [Test]
        public void ThenAsyncTOut_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            TryResult t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(async () => 1));
        }

        [Test]
        public void ThenAsyncTOut_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            TryResult t = "";
            Func<Task<TryResult<int>>> t2 = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(t2));
        }

        [Test]
        public async Task ThenAsyncTOut_WhenTrySucceeded_Executes()
        {
            var t = TryResult.Succeed();

            var executed = false;
            await t.ThenAsync(async () =>
            {
                executed = true;
                return 1;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task ThenAsyncTOut_WhenTrySucceeded_ReturnsResultOfExecutor()
        {
            var t = TryResult.Succeed();

            var result = 1;
            var tr = await t.ThenAsync(async () => result);

            Assert.AreEqual(result, tr.Result);
        }

        [Test]
        public async Task ThenAsyncTOut_WhenTryFailed_DoesNotExecute()
        {
            TryResult t = "err";

            var executed = false;
            await t.ThenAsync(async () =>
            {
                executed = true;
                return 1;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task ThenAsyncTOut_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult t = "err";

            var t2 = await t.ThenAsync(async () => 1);

            Assert.AreEqual(t.ErrorMessage, t2.ErrorMessage);
        }
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
