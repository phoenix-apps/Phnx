using NUnit.Framework;

namespace MarkSFrancis.Tests.ComparerHelperTests
{
    public class IntComparerTests
    {
        [Test]
        public void GettingComparer_ForInts_ReturnsComparer()
        {
            var intComparer = ComparerHelpers.DefaultComparer<int>();

            Assert.IsNotNull(intComparer);
        }

        [Test]
        public void GettingComparer_ForInts_ReturnsValidComparer()
        {
            var intComparer = ComparerHelpers.DefaultComparer<int>();

            Assert.AreEqual(-1, intComparer.Compare(0, 1));
            Assert.AreEqual(0, intComparer.Compare(0, 0));
            Assert.AreEqual(1, intComparer.Compare(0, -1));
        }

        [Test]
        public void GettingEqualityComparer_ForInts_ReturnsComparer()
        {
            var intComparer = ComparerHelpers.DefaultEqualityComparer<int>();

            Assert.IsNotNull(intComparer);
        }

        [Test]
        public void GettingEqualityComparer_ForInts_ReturnsValidComparer()
        {
            var intComparer = ComparerHelpers.DefaultEqualityComparer<int>();

            Assert.AreEqual(false, intComparer.Equals(0, 1));
            Assert.AreEqual(true, intComparer.Equals(0, 0));
            Assert.AreEqual(false, intComparer.Equals(0, -1));
        }
    }
}
