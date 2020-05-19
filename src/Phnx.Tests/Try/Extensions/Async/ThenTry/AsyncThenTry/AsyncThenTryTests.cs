using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

namespace Phnx.Tests.Try.Extensions.Async.ThenTry.AsyncThenTry
{
    using static Factory;

    public class AsyncThenTryTests
    {
        [Test]
        public void AsyncThenTry_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            Task<TryResult> t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync(() => string.Empty));
        }

        [Test]
        public void AsyncThenTry_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            var t = FailedTask("");
            Func<TryResult> t2 = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync(t2));
        }

        [Test]
        public void AsyncThenTry_WhenTryResultResultIsNull_ThrowsInvalidOperationException()
        {
            var t = NullTask();
            Assert.ThrowsAsync<InvalidOperationException>(() => t.ThenTryAsync(() => TryResult.Succeed()));
        }

        [Test]
        public async Task AsyncThenTry_WhenTrySucceeded_Executes()
        {
            var t = SucceedTask();

            var executed = false;
            await t.ThenTryAsync(() =>
            {
                executed = true;
                return TryResult.Succeed();
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task AsyncThenTry_WhenTrySucceeded_ReturnsResultOfExecutor()
        {
            var t = SucceedTask();

            var err = "err";
            var tr = await t.ThenTryAsync(() => TryResult.Fail(err));

            Assert.AreEqual(err, tr.ErrorMessage);
        }

        [Test]
        public async Task AsyncThenTry_WhenTryFailed_DoesNotExecute()
        {
            var t = FailedTask("err");

            var executed = false;
            await t.ThenTryAsync(() =>
            {
                executed = true;
                return TryResult.Succeed();
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task AsyncThenTry_WhenTryFailed_ReturnsOriginalError()
        {
            var r = TryResult.Fail("err");
            var t = Task.FromResult(r);

            var t2 = await t.ThenTryAsync(() => TryResult.Succeed());

            Assert.AreEqual(r, t2);
        }
    }
}
