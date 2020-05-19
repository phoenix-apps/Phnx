using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

namespace Phnx.Tests.Try.Extensions.Async.Then.AsyncThen
{
    using static Factory;

    public class AsyncThenTInTOutTests
    {
        [Test]
        public void AsyncThenInTOut_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            Task<TryResult<int>> t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(r => 1));
        }

        [Test]
        public void AsyncThenInTOut_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            var t = FailedTask<int>("");
            Func<int, TryResult<int>> t2 = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(t2));
        }

        [Test]
        public async Task AsyncThenInTOut_WhenTrySucceeded_Executes()
        {
            var t = SucceedTask(1);

            var executed = false;
            await t.ThenAsync(r =>
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
            await t.ThenAsync(r =>
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

            var t2 = await t.ThenAsync(r => 1);

            Assert.AreEqual(tr.ErrorMessage, t2.ErrorMessage);
        }
    }
}
