using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Collections.Tests.Extensions.ArrayExtensions
{
    public class FillTTests
    {
        [Test]
        public void Fill_NullArray_ThrowsArgumentNullException()
        {
            int[] items = null;

            Assert.Throws<ArgumentNullException>(() => items.Fill(1));
        }

        [Test]
        public void FillArray_WithNull_FillsArray()
        {
            string[] expected = new string[] { null, null, null, null, null };
            var items = new string[5];

            items.Fill(null);

            CollectionAssert.AreEqual(expected, items);
        }

        [Test]
        public void FillIntArray_WithLength1_WithValue6_FillsArray()
        {
            var expected = new int[] { 6 };
            var items = new int[1];
            items.Fill(6);

            CollectionAssert.AreEqual(expected, items);
        }
    }
}
