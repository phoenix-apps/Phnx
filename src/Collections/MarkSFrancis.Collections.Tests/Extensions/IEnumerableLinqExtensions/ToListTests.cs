using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Tests.Extensions.IEnumerableLinqExtensions
{
    public class ToListTests
    {
        [Test]
        public void ToList_WhenListIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> items = null;

            Assert.Throws<ArgumentNullException>(() => items.ToList(0));
        }

        [Test]
        public void ToList_WhenListContainsNoValuesAndCapacityIs0_CreatesEmptyList()
        {
            const int newCapacity = 0;
            var items = new List<int>();

            var result = items.ToList(newCapacity);

            CollectionAssert.AreEqual(items, result);
            Assert.AreEqual(newCapacity, result.Capacity);
        }

        [Test]
        public void ToList_WhenListContains4ValuesAndCapacityIs10_CreatesList()
        {
            const int newCapacity = 10;
            var items = new List<int> { 64, 1265, 3612, 1 };

            var result = items.ToList(newCapacity);

            CollectionAssert.AreEqual(items, result);
            Assert.AreEqual(newCapacity, result.Capacity);
        }

        [Test]
        public void ToList_WhenCapacityIsNegative_ThrowsArgumentOutOfRangeException()
        {
            const int newCapacity = -1;
            var items = new List<int>();

            Assert.Throws<ArgumentOutOfRangeException>(() => items.ToList(newCapacity));
        }
    }
}
