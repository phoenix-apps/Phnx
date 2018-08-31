using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Tests.Extensions.IEnumerableLinqExtensions
{
    public class EqualsRangeTests
    {
        [Test]
        public void EqualsRange_WhenCollectionIsNull_ReturnsFalse()
        {
            IEnumerable<int> items = null;
            IEnumerable<int> items2 = new List<int>();

            Assert.IsFalse(items.EqualsRange(items2));
        }

        [Test]
        public void EqualsRange_WhenCompareIsNull_ReturnsFalse()
        {
            IEnumerable<string> items = new List<string>();
            IEnumerable<string> items2 = null;

            Assert.IsFalse(items.EqualsRange(items2));
        }

        [Test]
        public void EqualsRange_WhenBothCollectionsAreEmpty_ReturnsTrue()
        {
            IEnumerable<string> items = new List<string>();
            IEnumerable<string> items2 = new List<string>();

            Assert.IsTrue(items.EqualsRange(items2));
        }

        [Test]
        public void EqualsRange_WhenBothCollectionsMatch_ReturnsTrue()
        {
            IEnumerable<int> items = new List<int>
            {
                1, 6743, 346, 7423, 37, 16, 2521
            };

            IEnumerable<int> items2 = new List<int>
            {
                1, 6743, 346, 7423, 37, 16, 2521
            };

            Assert.IsTrue(items.EqualsRange(items2));
        }

        [Test]
        public void EqualsRange_WhenSourceIsShorter_ReturnsFalse()
        {
            IEnumerable<int> items = new List<int>
            {
                1, 6743, 346, 7423, 37, 16
            };

            IEnumerable<int> items2 = new List<int>
            {
                1, 6743, 346, 7423, 37, 16, 2521
            };

            Assert.IsFalse(items.EqualsRange(items2));
        }

        [Test]
        public void EqualsRange_WhenCompareIsShorter_ReturnsFalse()
        {
            IEnumerable<int> items = new List<int>
            {
                1, 6743, 346, 7423, 37, 16, 2521
            };

            IEnumerable<int> items2 = new List<int>
            {
                1, 6743, 346, 7423, 37, 16
            };

            Assert.IsFalse(items.EqualsRange(items2));
        }

        [Test]
        public void EqualsRange_WhenCollectionsHaveDifferentValues_ReturnsFalse()
        {
            IEnumerable<int> items = new List<int>
            {
                1, 6743, 346, 7423, 37, 16, 2521
            };

            IEnumerable<int> items2 = new List<int>
            {
                1, 6743, 347, 7423, 37, 16, 2521
            };

            Assert.IsFalse(items.EqualsRange(items2));
        }
    }
}
