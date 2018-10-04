using NUnit.Framework;
using Phnx.Data.Seeds;
using System;

namespace Phnx.Data.Tests.Seeds
{
    public class SeedAsyncTests
    {
        [Test]
        public void New_WithNullAction_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SeedGroupAsync(null));
        }

        [Test]
        public void CastFromAction_WithNull_IsNull()
        {
            Action a = null;
            SeedAsync result = a;

            Assert.IsNull(result);
        }

        [Test]
        public void CastFromAction_WithAction_IsNotNull()
        {
            Action @delegate = () => { };

            SeedAsync result = @delegate;
            Assert.IsNotNull(result);
        }

        [Test]
        public void CastToAction_WithNull_IsNull()
        {
            SeedAsync seed = null;

            Action result = seed;
            Assert.IsNull(result);
        }

        [Test]
        public void CastToAction_WithSeed_IsNotNull()
        {
            void @delegate() { }

            var seed = new SeedAsync(@delegate);

            Action result = seed;
            Assert.IsNotNull(result);
        }

        [Test]
        public void RunAsync_ExecutesSeed()
        {
            int runCount = 0;
            void action() => ++runCount;

            var seed = new SeedAsync(action);

            Assert.AreEqual(0, runCount);

            var t = seed.RunAsync();
            t.Wait();

            Assert.AreEqual(1, runCount);
        }
    }
}
