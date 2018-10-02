using Phnx.Collections.Extensions;
using Phnx.Collections.Tests.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Phnx.Collections.Tests.Extensions.ArrayExtensions
{
    public class BinarySearchComparableTests
    {
        private Comparer<int> IntComparer => new ReverseIntComparer();

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

            var index = array.BinarySearch(0, IntComparer);

            Assert.AreEqual(-1, index);
        }

        [Test]
        public void BinarySearch_WhenArrayDoesNotContainIten_ReturnsIndexWhereItemShouldGo()
        {
            var array = new int[] { 8, 6, 4, 2, 0 };

            var index = array.BinarySearch(-2, IntComparer);
            Assert.AreEqual(~5, index);

            index = array.BinarySearch(1, IntComparer);
            Assert.AreEqual(~4, index);

            index = array.BinarySearch(3, IntComparer);
            Assert.AreEqual(~3, index);

            index = array.BinarySearch(5, IntComparer);
            Assert.AreEqual(~2, index);

            index = array.BinarySearch(7, IntComparer);
            Assert.AreEqual(~1, index);

            index = array.BinarySearch(9, IntComparer);
            Assert.AreEqual(~0, index);

            index = array.BinarySearch(1451614, IntComparer);
            Assert.AreEqual(~0, index);
        }

        [Test]
        public void BinarySearch_WhenArrayDoesContainIten_ReturnsIndexWhereItemIs()
        {
            var array = new int[] { 8, 6, 4, 2, 0 };

            var index = array.BinarySearch(0, IntComparer);
            Assert.AreEqual(4, index);

            index = array.BinarySearch(2, IntComparer);
            Assert.AreEqual(3, index);

            index = array.BinarySearch(4, IntComparer);
            Assert.AreEqual(2, index);

            index = array.BinarySearch(6, IntComparer);
            Assert.AreEqual(1, index);

            index = array.BinarySearch(8, IntComparer);
            Assert.AreEqual(0, index);
        }

        [Test]
        public void BinarySearch_WhenIComparableNullAndImplemented_UsesDefaultIComparable()
        {
            var array = new int[] { 0, 2, 4, 6, 8 };

            var index = array.BinarySearch(-2, null);
            Assert.AreEqual(~0, index);

            index = array.BinarySearch(9, null);
            Assert.AreEqual(~5, index);

            index = array.BinarySearch(4, null);
            Assert.AreEqual(2, index);
        }

        [Test]
        public void BinarySearch_WhenIComparableNullAndNotImplemented_ThrowsInvalidOperationException()
        {
            var array = new Point[] { new Point(5, 5) };

            Assert.Throws<InvalidOperationException>(() => array.BinarySearch(new Point(5, 5), null));
        }

        [Test]
        public void BinarySearch_CustomIComparable_WhenThereIsNoDefault_UsesCustomIComparable()
        {
            var array = new Point[] { new Point(5, 5) };
            var comparer = new AreaComparer();

            var index = array.BinarySearch(new Point(5, 5), comparer);
            Assert.AreEqual(0, index);

            index = array.BinarySearch(new Point(6, 5), comparer);
            Assert.AreEqual(~1, index);

            index = array.BinarySearch(new Point(6, 2), comparer);
            Assert.AreEqual(~0, index);
        }
    }
}
