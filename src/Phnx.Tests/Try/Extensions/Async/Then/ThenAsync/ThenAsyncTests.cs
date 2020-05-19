using NUnit.Framework;
using Phnx.Try;
using System;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace Phnx.Tests.Try.Extensions.Async.Then.ThenAsync
{
    public class ThenAsyncTests
    {
        [Test]
        public void ThenAsync_WhenTryResultIsNull_ThrowsArgumentNullException()
        {
            TryResult t = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(async () => { }));
        }

        [Test]
        public void ThenAsync_WhenExecuteIsNull_ThrowsArgumentNullException()
        {
            TryResult t = "";
            Assert.ThrowsAsync<ArgumentNullException>(() => t.ThenAsync(null));
        }

        [Test]
        public async Task ThenAsync_WhenTrySucceeded_Executes()
        {
            TryResult t = TryResult.Succeed();

            var executed = false;
            await t.ThenAsync(async () =>
            {
                executed = true;
            });

            Assert.IsTrue(executed);
        }

        [Test]
        public async Task ThenAsync_WhenTryFailed_DoesNotExecute()
        {
            TryResult t = "err";

            var executed = false;
            await t.ThenAsync(async () =>
            {
                executed = true;
            });

            Assert.IsFalse(executed);
        }

        [Test]
        public async Task ThenAsync_WhenTryFailed_ReturnsOriginalError()
        {
            var t = TryResult.Fail("err");

            var t2 = await t.ThenAsync(async () => { });

            Assert.AreEqual(t, t2);
        }
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
