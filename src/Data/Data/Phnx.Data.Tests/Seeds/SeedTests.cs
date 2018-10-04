using NUnit.Framework;
using Phnx.Data.Seeds;
using System;

namespace Phnx.Data.Tests.Seeds
{
    public class SeedTests
    {
        [Test]
        public void New_WithNullAction_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Seed(null));
        }

        [Test]
        public void CastFromAction_WithNull_IsNull()
        {
            Action a = null;
            Seed result = a;

            Assert.IsNull(result);
        }

        [Test]
        public void CastFromAction_WithAction_IsNotNull()
        {
            Action @delegate = () => { };

            Seed result = @delegate;
            Assert.IsNotNull(result);
        }

        [Test]
        public void CastToAction_WithNull_IsNull()
        {
            Seed seed = null;

            Action result = seed;
            Assert.IsNull(result);
        }

        [Test]
        public void CastToAction_WithSeed_IsNotNull()
        {
            void @delegate() { }

            var seed = new Seed(@delegate);

            Action result = seed;
            Assert.IsNotNull(result);
        }

        [Test]
        public void CastToAction_WithSeed_DoesNotThrow()
        {
            void @delegate() { }
            Assert.DoesNotThrow(() => _ = (Seed)@delegate);
        }

        [Test]
        public void Run_ExecutesSeed()
        {
            int runCount = 0;
            void action() => ++runCount;

            var seed = new Seed(action);

            Assert.AreEqual(0, runCount);

            seed.Run();

            Assert.AreEqual(1, runCount);
        }
    }
}
