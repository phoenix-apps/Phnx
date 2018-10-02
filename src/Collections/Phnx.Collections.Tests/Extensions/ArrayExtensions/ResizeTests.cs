using Phnx.Collections.Extensions;
using NUnit.Framework;
using System;

namespace Phnx.Collections.Tests.Extensions.ArrayExtensions
{
    public class ResizeTests
    {
        [Test]
        public void Resize_WhenArrayIsNull_ThrowsArgumentNullException()
        {
            int[] items = null;

            Assert.Throws<ArgumentNullException>(() => items.Resize(5));
        }

        [Test]
        public void ResizeTo0_WhenArrayIsEmpty_ReturnsEmptyCollection()
        {
            int[] items = new int[0];

            items = items.Resize(0);

            CollectionAssert.IsEmpty(items);
        }

        [Test]
        public void ResizeToMinus1_ThrowsArgumentOutOfRangeException()
        {
            int[] items = new int[0];

            Assert.Throws<ArgumentOutOfRangeException>(() => items.Resize(-1));
        }

        [Test]
        public void ResizeTo10_WhenArrayHas5_PreservesOriginal5Items()
        {
            int first = 5,
                second = 3,
                third = 5,
                fourth = 1,
                fifth = 2;

            var items = new int[] { first, second, third, fourth, fifth };

            items = items.Resize(10);

            Assert.AreEqual(10, items.Length);
            Assert.AreEqual(first, items[0]);
            Assert.AreEqual(second, items[1]);
            Assert.AreEqual(third, items[2]);
            Assert.AreEqual(fourth, items[3]);
            Assert.AreEqual(fifth, items[4]);
        }

        [Test]
        public void ResizeTo0_WhenArrayHas5_ClearsArray()
        {
            var items = new int[] { 16, 25, 12, 1, 0 };

            items = items.Resize(0);

            CollectionAssert.IsEmpty(items);
        }

        [Test]
        public void ResizeTo1_WhenArrayHas3_PreservesFirstItem()
        {
            int firstItem = 16;
            var items = new int[] { firstItem, 25, 12 };

            items = items.Resize(1);

            Assert.AreEqual(1, items.Length);
            Assert.AreEqual(firstItem, items[0]);
        }
    }
}
