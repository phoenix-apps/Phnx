using NUnit.Framework;
using System;
using System.Drawing;

namespace Phnx.Collections.Tests.Extensions.ArrayExtensions
{
    public class BinarySearchTests
    {
        [Test]
        public void BinarySearch_WhenArrayIsNull_ThrowsArgumentNullException()
        {
            int[] array = null;

            Assert.Throws<ArgumentNullException>(() => array.BinarySearch(0));
        }

        [Test]
        public void BinarySearch_WhenArrayIsEmpty_ReturnsIndexMinus1()
        {
            var array = new int[0];

            var index = array.BinarySearch(0);

            Assert.AreEqual(-1, index);
        }

        [Test]
        public void BinarySearch_WhenArrayDoesNotContainIten_ReturnsIndexWhereItemShouldGo()
        {
            var array = new int[] { 0, 2, 4, 6, 8 };

            var index = array.BinarySearch(-2);
            Assert.AreEqual(~0, index);

            index = array.BinarySearch(1);
            Assert.AreEqual(~1, index);

            index = array.BinarySearch(3);
            Assert.AreEqual(~2, index);

            index = array.BinarySearch(5);
            Assert.AreEqual(~3, index);

            index = array.BinarySearch(7);
            Assert.AreEqual(~4, index);

            index = array.BinarySearch(9);
            Assert.AreEqual(~5, index);

            index = array.BinarySearch(1451614);
            Assert.AreEqual(~5, index);
        }

        [Test]
        public void BinarySearch_WhenArrayDoesContainIten_ReturnsIndexWhereItemIs()
        {
            var array = new int[] { 0, 2, 4, 6, 8 };

            var index = array.BinarySearch(0);
            Assert.AreEqual(0, index);

            index = array.BinarySearch(2);
            Assert.AreEqual(1, index);

            index = array.BinarySearch(4);
            Assert.AreEqual(2, index);

            index = array.BinarySearch(6);
            Assert.AreEqual(3, index);

            index = array.BinarySearch(8);
            Assert.AreEqual(4, index);
        }

        [Test]
        public void BinarySearch_WhenIComparableNotImplemented_ThrowsInvalidOperationException()
        {
            var array = new Point[] { new Point(5, 5) };

            Assert.Throws<InvalidOperationException>(() => array.BinarySearch(new Point(5, 5)));
        }
    }
}
