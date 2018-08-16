using MarkSFrancis.Tests.Fakes.TypeFakes;
using NUnit.Framework;

namespace MarkSFrancis.Tests.ComparerHelperTests
{
    public class CustomTypeComparerTests
    {
        [Test]
        public void GettingComparer_ForCustomType_ReturnsComparer()
        {
            var customTypeComparer = ComparerHelpers.DefaultComparer<TypeWithComparer>();

            Assert.IsNotNull(customTypeComparer);
        }

        [Test]
        public void GettingComparer_ForCustomType_ReturnsValidComparer()
        {
            var customTypeComparer = ComparerHelpers.DefaultComparer<TypeWithComparer>();

            var neg1 = new TypeWithComparer(-1);
            var zero = new TypeWithComparer(0);
            var pos1 = new TypeWithComparer(1);

            Assert.AreEqual(-1, customTypeComparer.Compare(zero, pos1));
            Assert.AreEqual(0, customTypeComparer.Compare(zero, zero));
            Assert.AreEqual(1, customTypeComparer.Compare(zero, neg1));
        }

        [Test]
        public void GettingEqualityComparer_ForCustomType_ReturnsEqualityComparer()
        {
            var customTypeComparer = ComparerHelpers.DefaultEqualityComparer<TypeWithComparer>();

            Assert.IsNotNull(customTypeComparer);
        }

        [Test]
        public void GettingEqualityComparer_ForCustomType_ReturnsValidEqualityComparer()
        {
            var customTypeComparer = ComparerHelpers.DefaultEqualityComparer<TypeWithComparer>();

            var neg1 = new TypeWithComparer(-1);
            var zero = new TypeWithComparer(0);
            var pos1 = new TypeWithComparer(1);

            Assert.AreEqual(false, customTypeComparer.Equals(zero, pos1));
            Assert.AreEqual(true, customTypeComparer.Equals(zero, zero));
            Assert.AreEqual(false, customTypeComparer.Equals(zero, neg1));
        }
    }
}
