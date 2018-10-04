using NUnit.Framework;
using Phnx.Data.Seeds;
using System;

namespace Phnx.Data.Tests.Seeds
{
    public class IseedExtensionsTests
    {
        [Test]
        public void NullSeed_ToAsync_ThrowsArgumentNullException()
        {
            ISeed seed = null;

            Assert.Throws<ArgumentNullException>(() => seed.ToAsync());
        }

        [Test]
        public void Seed_ToAsync_IsNotNull()
        {
            void a() { }
            var seed = new Seed(a);

            ISeedAsync seedAsync = null;
            seedAsync = seed.ToAsync();

            Assert.IsNotNull(seedAsync);
        }
    }
}
