using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

namespace Phnx.Tests.Try.Extensions.Async.ThenTry.AsyncThenTry
{
    using static Factory;

    public class AsyncThenTryTOutTests
    {
        [Test]
        public void AsyncThenTryTOut_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            Task<TryResult> t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync<int>(() => 1));
        }

        [Test]
        public void AsyncThenTryTOut_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            var t = FailedTask("");
            Func<TryResult<int>> t2 = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync(t2));
        }

        [Test]
        public async Task AsyncThenTryTOut_WhenTrySucceeded_Executes()
        {
            var t = SucceedTask();

            var executed = false;
            await t.ThenTryAsync<int>(() =>
            {
                executed = true;
                return 1;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task AsyncThenTryTOut_WhenTrySucceeded_ReturnsResultOfExecutor()
        {
            var t = SucceedTask();

            var result = 1;
            var tr = await t.ThenTryAsync<int>(() => result);

            Assert.AreEqual(result, tr.Result);
        }

        [Test]
        public async Task AsyncThenTryTOut_WhenTryFailed_DoesNotExecute()
        {
            var t = FailedTask("err");

            var executed = false;
            await t.ThenTryAsync<int>(() =>
            {
                executed = true;
                return 1;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task AsyncThenTryTOut_WhenTryFailed_ReturnsOriginalError()
        {
            var err = "err";
            var t = FailedTask(err);

            var t2 = await t.ThenTryAsync<int>(() => 1);

            Assert.AreEqual(err, t2.ErrorMessage);
        }
    }
}
