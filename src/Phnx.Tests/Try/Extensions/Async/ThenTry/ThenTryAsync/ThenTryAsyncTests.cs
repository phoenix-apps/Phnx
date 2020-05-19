using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace Phnx.Tests.Try.Extensions.Async.ThenTry.ThenTryAsync
{
    public class ThenTryAsyncTests
    {
        [Test]
        public void ThenTryAsync_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            TryResult t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync(async () => TryResult.Succeed()));
        }

        [Test]
        public void ThenTryAsync_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            TryResult t = "";
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenTryAsync(null));
        }

        [Test]
        public async Task ThenTryAsync_WhenTrySucceeded_Executes()
        {
            TryResult t = TryResult.Succeed();

            var executed = false;
            await t.ThenTryAsync(async () =>
            {
                executed = true;
                return TryResult.Succeed();
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task ThenTryAsync_WhenTryFailed_DoesNotExecute()
        {
            TryResult t = "err";

            var executed = false;
            await t.ThenTryAsync(async () =>
            {
                executed = true;
                return TryResult.Succeed();
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task ThenTryAsync_WhenTryFailed_ReturnsOriginalError()
        {
            var t = TryResult.Fail("err");

            var t2 = await t.ThenTryAsync(async () => TryResult.Succeed());

            Assert.AreEqual(t, t2);
        }
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
