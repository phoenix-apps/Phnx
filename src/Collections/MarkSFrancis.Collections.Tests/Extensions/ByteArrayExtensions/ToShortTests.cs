using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Collections.Tests.Extensions.ByteArrayExtensions
{
    public class ToShortTests
    {
        [Test]
        public void ToShort_WithNullArray_ThrowsArgumentNullException()
        {
            byte[] bytes = null;

            Assert.Throws<ArgumentNullException>(() => bytes.ToShort());
        }

        [Test]
        public void ToShort_WithByteArrayTooShort_ThrowsArgumentException()
        {
            var bytes = new byte[] { 62 };

            Assert.Throws<ArgumentException>(() => bytes.ToShort());
        }

        [Test]
        public void ToShort_WithStartIndexTooCloseToEnd_ThrowsArgumentException()
        {
            var bytes = new byte[] { 1, 62 };

            Assert.Throws<ArgumentOutOfRangeException>(() => bytes.ToShort(1));
        }

        [Test]
        public void ToShort_WithStartIndexLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var bytes = new byte[] { 1, 62 };

            Assert.Throws<ArgumentOutOfRangeException>(() => bytes.ToShort(-1));
        }

        [Test]
        public void ToShort_WithStartIndexAfterEnd_ThrowsArgumentOutOfRangeException()
        {
            var bytes = new byte[] { 1, 62 };

            Assert.Throws<ArgumentOutOfRangeException>(() => bytes.ToShort(6));
        }

        [Test]
        public void ToShort_WithValueByteArray_Converts()
        {
            var bytes = new byte[] { 1, 62 };

            var value = bytes.ToShort();

            Assert.AreEqual(15873, value);
        }
    }
}
