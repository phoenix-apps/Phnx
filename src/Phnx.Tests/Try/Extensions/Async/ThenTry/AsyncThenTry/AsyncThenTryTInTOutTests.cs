using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

namespace Phnx.Tests.Try.Extensions.Async.ThenTry.AsyncThenTry
{
    using static Factory;

    public class AsyncThenTryTInTOutTests
    {
        [Test]
        public void AsyncThenTryInTOut_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            Task<TryResult<int>> t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync<int, int>(r => 1));
        }

        [Test]
        public void AsyncThenTryInTOut_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            var t = FailedTask<int>("");
            Func<int, TryResult<int>> t2 = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync(t2));
        }

        [Test]
        public async Task AsyncThenTryInTOut_WhenTrySucceeded_Executes()
        {
            var t = SucceedTask(1);

            var executed = false;
            await t.ThenTryAsync<int, int>(r =>
            {
                executed = true;
                return 1;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task AsyncThenTryInTOut_WhenTryFailed_DoesNotExecute()
        {
            var t = FailedTask<int>("err");

            var executed = false;
            await t.ThenTryAsync<int, int>(r =>
            {
                executed = true;
                return 1;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task AsyncThenTryInTOut_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult<int> tr = "err";
            var t = Task.FromResult(tr);

            var t2 = await t.ThenTryAsync<int, int>(r => 1);

            Assert.AreEqual(tr.ErrorMessage, t2.ErrorMessage);
        }
    }
}
