using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace Phnx.Tests.Try.Extensions.Async.Then.ThenAsync
{
    using static Factory;

    public class ThenAsyncTInTOutTests
    {
        [Test]
        public void AsyncThenInTOut_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            Task<TryResult<int>> t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(async r => 1));
        }

        [Test]
        public void AsyncThenInTOut_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            var t = FailedTask<int>("");
            Func<int, Task<TryResult<int>>> t2 = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(t2));
        }

        [Test]
        public async Task AsyncThenInTOut_WhenTrySucceeded_Executes()
        {
            var t = SucceedTask(1);

            var executed = false;
            await t.ThenAsync(async r =>
            {
                executed = true;
                return 1;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task AsyncThenInTOut_WhenTryFailed_DoesNotExecute()
        {
            var t = FailedTask<int>("err");

            var executed = false;
            await t.ThenAsync(async r =>
            {
                executed = true;
                return 1;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task AsyncThenInTOut_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult<int> tr = "err";
            var t = Task.FromResult(tr);

            var t2 = await t.ThenAsync(async r => 1);

            Assert.AreEqual(tr.ErrorMessage, t2.ErrorMessage);
        }
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
