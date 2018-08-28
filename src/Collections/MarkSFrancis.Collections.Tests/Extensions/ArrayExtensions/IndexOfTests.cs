using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Collections.Tests.Extensions.ArrayExtensions
{
    public class IndexOfTests
    {
        [Test]
        public void IndexOf_WithEmptyArray_ReturnsMinus1()
        {
            var items = new int[0];

            Assert.AreEqual(-1, items.IndexOf(0));
        }

        [Test]
        public void IndexOf_WhenItemIsAtStart_Returns0()
        {
            var items = new int[] { 1, 2, 3 };

            var index = items.IndexOf(1);

            Assert.AreEqual(0, index);
        }

        [Test]
        public void IndexOf_WhenItemIsAtEnd_ReturnsLengthMinus1()
        {
            var items = new int[] { 1, 3, 2 };

            var index = items.IndexOf(2);

            Assert.AreEqual(2, index);
        }

        [Test]
        public void IndexOf_WhenItemIsTwiceInList_GetsFirstOccuranceIndex()
        {
            var items = new int[] { 1, 2, 1, 2, 3, 4, 1 };

            var index = items.IndexOf(1);

            Assert.AreEqual(0, index);
        }

        [Test]
        public void IndexOf_WhenArrayIsNull_ThrowsArgumentNullException()
        {
            int[] items = null;

            Assert.Throws<ArgumentNullException>(() => items.IndexOf(0));
        }
    }
}
