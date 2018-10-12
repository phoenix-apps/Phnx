using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Collections.Tests.Extensions.IEnumerableExtensions
{
    public class CopyRangeTests
    {
        [Test]
        public void CopyRange_WithNullValues_ThrowsArgumentNullException()
        {
            List<string> items = null;

            Assert.Throws<ArgumentNullException>(() => items.CopyRange(0, 0).ToList());
        }

        [Test]
        public void CopyRange_WithEmptyCollection_MakesEmptyCollection()
        {
            var items = new List<string>();

            var results = items.CopyRange(0, 0);

            CollectionAssert.IsEmpty(results);
        }

        [Test]
        public void CopyRange_StartingAt0ToEndOfCollection_CopiesCollection()
        {
            var expected = new List<string>
            {
                "item1",
                "item2",
                "item3"
            };

            var items = new List<string>
            {
                "item1",
                "item2",
                "item3"
            };

            var results = items.CopyRange(0, items.Count);

            CollectionAssert.AreEqual(expected, results);
        }

        [Test]
        public void CopyRange_CopyingFromStartToMiddle_CopiesSegment()
        {
            var expected = new List<string>
            {
                "item1"
            };

            var items = new List<string>
            {
                "item1",
                "item2",
                "item3"
            };

            var results = items.CopyRange(0, 1);

            CollectionAssert.AreEqual(expected, results);
        }

        [Test]
        public void CopyRange_CopyingFromPartWayToPartWayLater_CopiesSegment()
        {
            var expected = new List<string>
            {
                "item2",
                "item3"
            };

            var items = new List<string>
            {
                "item1",
                "item2",
                "item3",
                "item4"
            };

            var results = items.CopyRange(1, 2);

            CollectionAssert.AreEqual(expected, results);
        }

        [Test]
        public void CopyRange_StartingAtEnd_CreatesEmptyCollection()
        {
            var expected = new List<string>
            {
            };

            var items = new List<string>
            {
                "item1",
                "item2",
                "item3"
            };

            var results = items.CopyRange(3, 0);

            CollectionAssert.AreEqual(expected, results);
        }

        [Test]
        public void CopyRange_StartingLessThanZero_ThrowsArgumentLessThanZeroException()
        {
            var items = new List<string>
            {
                "item1",
                "item2",
                "item3"
            };

            Assert.Throws<ArgumentLessThanZeroException>(() => items.CopyRange(-1, 0).ToList());
        }

        [Test]
        public void CopyRange_StartingAfterEnd_ThrowsArgumentOutOfRangeException()
        {
            var items = new List<string>
            {
                "item1",
                "item2",
                "item3"
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => items.CopyRange(4, 0).ToList());
        }

        [Test]
        public void CopyRange_CopyingFromMiddleToAfterEnd_ThrowsArgumentOutOfRangeException()
        {
            var items = new List<string>
            {
                "item1",
                "item2",
                "item3"
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => items.CopyRange(2, 2).ToList());
        }

        [Test]
        public void CopyRange_WithCountLessThanZero_ThrowsArgumentLessThanZeroException()
        {
            var items = new List<string>
            {
                "item1",
                "item2",
                "item3"
            };

            Assert.Throws<ArgumentLessThanZeroException>(() => items.CopyRange(2, -1).ToList());
        }
    }
}
