using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System;
using System.Linq;

namespace MarkSFrancis.Collections.Tests.Extensions.ArrayExtensions
{
    public class ReverseTests
    {
        [Test]
        public void Reverse_EmptyArray_LeavesArrayEmpty()
        {
            int[] items = new int[0];

            items.Reverse();

            Assert.AreEqual(0, items.Length);
        }

        [Test]
        public void Reverse_PopulatedArray_ReversesArray()
        {
            int[] expected = new int[] { 4, 5, 3, 2, 1 };
            int[] items = new int[] { 1, 2, 3, 5, 4 };

            items.Reverse();

            CollectionAssert.AreEqual(expected, items);
        }

        [Test]
        public void Reverse_NullArray_ThrowsArgumentNullException()
        {
            int[] items = null;

            Assert.Throws<ArgumentNullException>(() => items.Reverse());
        }

        [Test]
        public void ReverseRange_EmptyArray_LeavesArrayEmpty()
        {
            int[] items = new int[0];

            items.Reverse();

            Assert.AreEqual(0, items.Length);
        }

        [Test]
        public void ReverseRange_PopulatedArray_ReversesArray()
        {
            int[] expected = new int[] { 1, 2, 3, 4, 5 };
            int[] items = new int[] { 1, 2, 3, 5, 4 };

            items.Reverse(3, 2);

            CollectionAssert.AreEqual(expected, items);
        }

        [Test]
        public void ReverseRange_LengthBeyondEndOfArray_ThrowsArgumentException()
        {
            int[] expected = new int[] { 1, 2, 3, 4, 5 };
            int[] items = new int[] { 1, 2, 3, 5, 4 };

            Assert.Throws<ArgumentException>(() => items.Reverse(3, 5));
        }

        [Test]
        public void ReverseRange_StartLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            int[] expected = new int[] { 1, 2, 3, 4, 5 };
            int[] items = new int[] { 1, 2, 3, 5, 4 };

            Assert.Throws<ArgumentOutOfRangeException>(() => items.Reverse(-1, 5));
        }

        [Test]
        public void ReverseRange_NullArray_ThrowsArgumentNullException()
        {
            int[] items = null;

            Assert.Throws<ArgumentNullException>(() => items.Reverse(0, 0));
        }
    }
}
