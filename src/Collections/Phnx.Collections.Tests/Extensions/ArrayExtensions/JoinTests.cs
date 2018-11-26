using NUnit.Framework;
using System;

namespace Phnx.Collections.Tests.Extensions.ArrayExtensions
{
    public class JoinTests
    {
        [Test]
        public void Join_NullArray_ThrowsArgumentNullException()
        {
            byte[] arr = null;

            Assert.Throws<ArgumentNullException>(() => arr.Join(new byte[0]));
        }

        [Test]
        public void Join_NullOntoArray_DoesNotAddAnything()
        {
            var arr = new byte[5];
            byte[] none = null;
            var result = arr.Join(none);

            CollectionAssert.AreEqual(arr, result);
        }

        [Test]
        public void Join_MultipleArrays_AddsArrays()
        {
            var expected = new byte[] { 76, 61, 195, 74, 1, 2, 2, 21, 83, 65, 52, 12 };

            var arr = new byte[] { 76, 61, 195, 74 };
            byte[][] addOns = new byte[][] { new byte[] { 1, 2, 2, 21, 83 }, new byte[] { 65, 52, 12 } };

            var result = arr.Join(addOns);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void Join_MultipleArraysWithNullInside_SkipsNullButAddsOthers()
        {
            var expected = new byte[] { 76, 61, 195, 74, 1, 2, 2, 21, 83, 65, 52, 12 };

            var arr = new byte[] { 76, 61, 195, 74 };
            byte[][] addOns = new byte[][] { new byte[] { 1, 2, 2, 21, 83 }, null, new byte[] { 65, 52, 12 } };

            var result = arr.Join(addOns);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void Join_NullMultiple_ThrowsArgumentNullException()
        {
            var arr = new byte[5];
            byte[][] addOns = null;

            Assert.Throws<ArgumentNullException>(() => arr.Join(addOns));
        }
    }
}
