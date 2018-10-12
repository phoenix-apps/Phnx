using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Phnx.Collections.Tests.Extensions.IListExtensions
{
    public class ToListTests
    {
        [Test]
        public void ToList_WhenSourceIsNull_ThrowsArgumentNullException()
        {
            IList<int> items = null;

            Assert.Throws<ArgumentNullException>(() => items.ToList());
        }

        [Test]
        public void ToList_WhenSourceIsEmpty_ReturnsListWithCapacityZero()
        {
            IList<int> items = new List<int> { };

            var result = items.ToList();

            Assert.AreEqual(0, result.Capacity);
        }

        [Test]
        public void ToList_WhenSourceContainsItems_ReturnsListWithOriginalCollectionAndCapacityOfOriginalCollectionCount()
        {
            var expected = new List<int> { 1256, 2136, 213, 12, 1236 };
            IList<int> items = expected;

            var result = items.ToList();

            CollectionAssert.AreEqual(expected, result);
            Assert.AreEqual(expected.Count, result.Capacity);
        }
    }
}
