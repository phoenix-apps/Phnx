using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Collections.Tests.Extensions.IEnumerableLinqExtensions
{
    public class DistinctByTests
    {
        [Test]
        public void DistinctBy_WhenCollectionIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> values = null;

            Assert.Throws<ArgumentNullException>(() => values.DistinctBy(i => i).ToList());
        }

        [Test]
        public void DistinctBy_WhenKeySelectorIsNull_ThrowsArgumentNullException()
        {
            var values = new List<int>();
            Func<int, int> selector = null;

            Assert.Throws<ArgumentNullException>(() => values.DistinctBy(selector).ToList());
        }

        [Test]
        public void DistinctBy_WhenCollectionContainsNoDuplicates_ReturnsCollection()
        {
            var expected = new List<int>
            {
                125, 3652, 6435, 1245, 1
            };

            var values = new List<int>
            {
                125, 3652, 6435, 1245, 1
            };

            var result = values.DistinctBy(v => v);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void DistinctBy_WhenCollectionContains1Duplicate_RemovesBadItem()
        {
            var expected = new List<int>
            {
                125, 3652, 6435, 1
            };

            var values = new List<int>
            {
                125, 3652, 6435, 125, 1
            };

            var result = values.DistinctBy(v => v);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void DistinctBy_WhenCollectionContainsAllDuplicates_ReturnsSingleItem()
        {
            var expected = new List<int>
            {
                125
            };

            var values = new List<int>
            {
                125, 3652, 6435, 125, 1
            };

            var result = values.DistinctBy(_ => 1);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
