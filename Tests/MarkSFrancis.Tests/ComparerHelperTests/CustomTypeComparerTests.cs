using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.ComparerHelperTests
{
    public class CustomTypeComparerTests
    {
        [Test]
        public void GettingComparer_ForCustomType_ReturnsComparer()
        {
            var customTypeComparer = ComparerHelpers.DefaultComparer<CustomTypeWithComparer>();

            Assert.IsNotNull(customTypeComparer);
        }

        [Test]
        public void GettingComparer_ForCustomType_ReturnsValidComparer()
        {
            var customTypeComparer = ComparerHelpers.DefaultComparer<CustomTypeWithComparer>();

            var neg1 = new CustomTypeWithComparer(-1);
            var zero = new CustomTypeWithComparer(0);
            var pos1 = new CustomTypeWithComparer(1);

            Assert.AreEqual(-1, customTypeComparer.Compare(zero, pos1));
            Assert.AreEqual(0, customTypeComparer.Compare(zero, zero));
            Assert.AreEqual(1, customTypeComparer.Compare(zero, neg1));
        }

        [Test]
        public void GettingEqualityComparer_ForCustomType_ReturnsComparer()
        {
            var customTypeComparer = ComparerHelpers.DefaultEqualityComparer<CustomTypeWithComparer>();

            Assert.IsNotNull(customTypeComparer);
        }

        [Test]
        public void GettingEqualityComparer_ForCustomType_ReturnsValidComparer()
        {
            var customTypeComparer = ComparerHelpers.DefaultEqualityComparer<CustomTypeWithComparer>();

            var neg1 = new CustomTypeWithComparer(-1);
            var zero = new CustomTypeWithComparer(0);
            var pos1 = new CustomTypeWithComparer(1);

            Assert.AreEqual(false, customTypeComparer.Equals(zero, pos1));
            Assert.AreEqual(true, customTypeComparer.Equals(zero, zero));
            Assert.AreEqual(false, customTypeComparer.Equals(zero, neg1));
        }

        private class CustomTypeWithComparer : IComparable<CustomTypeWithComparer>, IEquatable<CustomTypeWithComparer>
        {
            public CustomTypeWithComparer(int value)
            {
                Value = value;
            }

            public int Value { get; }

            public int CompareTo(CustomTypeWithComparer obj)
            {
                return Value.CompareTo(obj.Value);
            }

            public bool Equals(CustomTypeWithComparer other)
            {
                return Value.Equals(other.Value);
            }
        }
    }
}
