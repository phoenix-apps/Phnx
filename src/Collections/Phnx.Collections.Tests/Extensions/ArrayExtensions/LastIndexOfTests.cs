using NUnit.Framework;
using System;

namespace Phnx.Collections.Tests.Extensions.ArrayExtensions
{
    public class LastIndexOfTests
    {
        [Test]
        public void LastIndexOf_WithEmptyArray_ReturnsMinus1()
        {
            var items = new int[0];

            Assert.AreEqual(-1, items.LastIndexOf(0));
        }

        [Test]
        public void LastIndexOf_WhenItemIsAtStart_Returns0()
        {
            var items = new int[] { 1, 2, 3 };

            var index = items.LastIndexOf(1);

            Assert.AreEqual(0, index);
        }

        [Test]
        public void LastIndexOf_WhenItemIsAtEnd_ReturnsLengthMinus1()
        {
            var items = new int[] { 1, 3, 2 };

            var index = items.LastIndexOf(2);

            Assert.AreEqual(2, index);
        }

        [Test]
        public void LastIndexOf_WhenItemIsTwiceInList_GetsFirstOccuranceIndex()
        {
            var items = new int[] { 1, 2, 1, 2, 3, 4, 1 };

            var index = items.LastIndexOf(1);

            Assert.AreEqual(6, index);
        }

        [Test]
        public void LastIndexOf_WhenArrayIsNull_ThrowsArgumentNullException()
        {
            int[] items = null;

            Assert.Throws<ArgumentNullException>(() => items.LastIndexOf(0));
        }
    }
}
