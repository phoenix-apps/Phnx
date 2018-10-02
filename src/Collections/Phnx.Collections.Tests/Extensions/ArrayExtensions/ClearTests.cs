using Phnx.Collections.Extensions;
using NUnit.Framework;
using System;

namespace Phnx.Collections.Tests.Extensions.ArrayExtensions
{
    public class ClearTests
    {
        [Test]
        public void Clear_EmptyArray_LeavesArrayEmpty()
        {
            int[] items = new int[0];

            items.Clear();

            Assert.AreEqual(0, items.Length);
        }

        [Test]
        public void Clear_PopulatedArray_ClearsArray()
        {
            int[] expected = new int[] { 0, 0, 0, 0, 0 };
            int[] items = new int[] { 1, 2, 3, 4, 5 };

            items.Clear();

            CollectionAssert.AreEqual(expected, items);
        }

        [Test]
        public void Clear_2D_PopulatedArray_ClearsArray()
        {
            int[,] expected = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            int[,] items = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            items.Clear();

            CollectionAssert.AreEqual(expected, items);
        }

        [Test]
        public void Clear_NullArray_ThrowsArgumentNullException()
        {
            int[] items = null;

            Assert.Throws<ArgumentNullException>(() => items.Clear());
        }
    }
}
