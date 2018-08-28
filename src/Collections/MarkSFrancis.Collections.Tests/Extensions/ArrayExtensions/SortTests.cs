using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Collections.Tests.Extensions.ArrayExtensions
{
    public class SortTests
    {
        [Test]
        public void Sort_WithEmptyArray_DoesNothing()
        {
            var items = new int[0];
            items.Sort();

            Assert.AreEqual(0, items.Length);
        }

        [Test]
        public void Sort_WhenArrayIsAlreadyInOrder_DoesNothing()
        {
            var expected = new int[] { 1, 2, 3 };
            var items = new int[] { 1, 2, 3 };

            items.Sort();

            CollectionAssert.AreEqual(expected, items);
        }

        [Test]
        public void Sort_WhenSmallArrayOutOfOrder_Sorts()
        {
            var expected = new int[] { 1, 2, 3 };
            var items = new int[] { 1, 3, 2 };

            items.Sort();

            CollectionAssert.AreEqual(expected, items);
        }

        [Test]
        public void Sort_WhenLargeArrayOutOfOrder_Sorts()
        {
            var expected = new int[] { 1, 1, 1, 2, 2, 3, 4 };
            var items = new int[] { 1, 2, 1, 2, 3, 4, 1 };

            items.Sort();

            CollectionAssert.AreEqual(expected, items);
        }

        [Test]
        public void Sort_WhenArrayIsNull_ThrowsArgumentNullException()
        {
            int[] items = null;

            Assert.Throws<ArgumentNullException>(() => items.Sort());
        }
    }
}
