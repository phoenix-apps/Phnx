using Phnx.Tests.Fakes.TypeFakes;
using NUnit.Framework;

namespace Phnx.Tests.Extensions.ObjectExtensionsTests
{
    public class IsEqualToTests
    {
        [Test]
        public void GettingEqualityComparer_ForCustomType_ReturnsValidEqualityComparer()
        {
            var neg1 = new TypeWithComparer(-1);
            var zero = new TypeWithComparer(0);
            var pos1 = new TypeWithComparer(1);

            Assert.AreEqual(false, zero.IsEqualTo(pos1));
            Assert.AreEqual(true, zero.IsEqualTo(zero));
            Assert.AreEqual(false, zero.IsEqualTo(neg1));
        }

        [Test]
        public void GettingEqualityComparer_ForInts_ReturnsValidEqualityComparer()
        {
            Assert.AreEqual(false, 0.IsEqualTo(1));
            Assert.AreEqual(true, 0.IsEqualTo(0));
            Assert.AreEqual(false, 0.IsEqualTo(-1));
        }
    }
}
