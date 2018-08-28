using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Collections.Tests.Extensions.ArrayExtensions
{
    public class ShallowCopyTests
    {
        [Test]
        public void ShallowCopy_WhenArrayIsNull_ThrowsArgumentNullException()
        {
            int[] items = null;

            Assert.Throws<ArgumentNullException>(() => items.ShallowCopy());
        }

        [Test]
        public void ShallowCopy_WhenArrayIsEmpty_ReturnsEmptyCollection()
        {
            var items = new int[0];

            var result = items.ShallowCopy();

            Assert.IsEmpty(result);
        }

        [Test]
        public void ShallowCopy_WhenArrayHasItems_CopiesItems()
        {
            var expected = new int[] { 3, 2, 5, 4, 1 };
            var items = new int[] { 3, 2, 5, 4, 1 };

            var copy = items.ShallowCopy();

            CollectionAssert.AreEqual(expected, copy);
        }

        [Test]
        public void ShallowCopy_WhenArrayHasItems_DoesNotMakeDeepCopy()
        {
            var firstItem = new int[] { 1, 5 };

            var items = new int[][] { firstItem };

            var copy = items.ShallowCopy();

            // Assert that item was copied successfully
            Assert.AreEqual(1, copy.Length);
            Assert.AreEqual(firstItem, copy[0]);
            CollectionAssert.AreEqual(firstItem, copy[0]);

            // Change element and assert that change is reflected in both
            firstItem[0] = 7;
            CollectionAssert.AreEqual(firstItem, copy[0]);
        }
    }
}
