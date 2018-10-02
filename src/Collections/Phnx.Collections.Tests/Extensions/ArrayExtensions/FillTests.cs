using Phnx.Collections.Extensions;
using NUnit.Framework;
using System;

namespace Phnx.Collections.Tests.Extensions.ArrayExtensions
{
    public class FillTests
    {
        [Test]
        public void Fill_NullArray_ThrowsArgumentNullException()
        {
            Array items = null;

            Assert.Throws<ArgumentNullException>(() => items.Fill(1));
        }

        [Test]
        public void FillArray_WithNull_FillsArray()
        {
            string[] expected = new string[] { null, null, null, null, null };
            Array items = new string[5];

            items.Fill(null);

            CollectionAssert.AreEqual(expected, items);
        }

        [Test]
        public void FillArray_WithObjectOfWrongType_ThrowsInvalidCastException()
        {
            Array items = new string[5];

            Assert.Throws<InvalidCastException>(() => items.Fill(21));
        }

        [Test]
        public void Fill2DArray_WithObjectOfWrongType_ThrowsArgumentException()
        {
            Array items = new string[3, 3];

            Assert.Throws<ArgumentException>(() => items.Fill(null));
        }

        [Test]
        public void FillIntArray_WithLength1_WithValue6_FillsArray()
        {
            Array expected = new int[] { 6 };
            Array items = new int[1];
            items.Fill(6);

            CollectionAssert.AreEqual(expected, items);
        }
    }
}
