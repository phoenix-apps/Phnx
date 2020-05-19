using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

namespace Phnx.Tests.Try.Extensions.Async.Then.AsyncThen
{
    using static Factory;

    public class AsyncThenTOutTests
    {
        [Test]
        public void AsyncThenTOut_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            Task<TryResult> t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(() => 1));
        }

        [Test]
        public void AsyncThenTOut_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            var t = FailedTask("");
            Func<TryResult<int>> t2 = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(t2));
        }

        [Test]
        public async Task AsyncThenTOut_WhenTrySucceeded_Executes()
        {
            var t = SucceedTask();

            var executed = false;
            await t.ThenAsync(() =>
            {
                executed = true;
                return 1;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task AsyncThenTOut_WhenTrySucceeded_ReturnsResultOfExecutor()
        {
            var t = SucceedTask();

            var result = 1;
            var tr = await t.ThenAsync(() => result);

            Assert.AreEqual(result, tr.Result);
        }

        [Test]
        public async Task AsyncThenTOut_WhenTryFailed_DoesNotExecute()
        {
            var t = FailedTask("err");

            var executed = false;
            await t.ThenAsync(() =>
            {
                executed = true;
                return 1;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task AsyncThenTOut_WhenTryFailed_ReturnsOriginalError()
        {
            var err = "err";
            var t = FailedTask(err);

            var t2 = await t.ThenAsync(() => 1);

            Assert.AreEqual(err, t2.ErrorMessage);
        }
    }
}
