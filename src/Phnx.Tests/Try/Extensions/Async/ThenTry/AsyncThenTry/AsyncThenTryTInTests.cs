using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

namespace Phnx.Tests.Try.Extensions.Async.ThenTry.AsyncThenTry
{
    using static Factory;

    public class AsyncThenTryTInTests
    {
        [Test]
        public void AsyncThenTryIn_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            Task<TryResult<int>> t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync(r => string.Empty));
        }

        [Test]
        public void AsyncThenTryIn_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            var t = FailedTask<int>("");
            Func<int, TryResult> t2 = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync(t2));
        }

        [Test]
        public void AsyncThenTryIn_WhenTryResultResultIsNull_ThrowsInvalidOperationException()
        {
            var t = NullTask<int>();
            Assert.ThrowsAsync<InvalidOperationException>(() => t.ThenTryAsync(r => TryResult.Succeed()));
        }

        [Test]
        public async Task AsyncThenTryIn_WhenTrySucceeded_Executes()
        {
            var t = SucceedTask(1);

            var executed = false;
            await t.ThenTryAsync(r =>
            {
                executed = true;
                return TryResult.Succeed();
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task AsyncThenTryIn_WhenTryFailed_DoesNotExecute()
        {
            var t = FailedTask<int>("err");

            var executed = false;
            await t.ThenTryAsync(r =>
            {
                executed = true;
                return TryResult.Succeed();
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task AsyncThenTryIn_WhenTryFailed_ReturnsOriginalError()
        {
            TryResult<int> tr = "err";
            var t = Task.FromResult(tr);

            var t2 = await t.ThenTryAsync(r => TryResult.Succeed());

            Assert.AreEqual(tr, t2);
        }
    }
}
