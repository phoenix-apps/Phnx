using NUnit.Framework;
using System;

namespace Phnx.Collections.Tests.Extensions.ByteArrayExtensions
{
    public class ToLongTests
    {
        [Test]
        public void ToLong_WithNullArray_ThrowsArgumentNullException()
        {
            byte[] bytes = null;

            Assert.Throws<ArgumentNullException>(() => bytes.ToLong());
        }

        [Test]
        public void ToLong_WithByteArrayTooShort_ThrowsArgumentException()
        {
            var bytes = new byte[] { 1, 6, 4, 2 };

            Assert.Throws<ArgumentException>(() => bytes.ToLong());
        }

        [Test]
        public void ToLong_WithStartIndexTooCloseToEnd_ThrowsArgumentException()
        {
            var bytes = new byte[] { 1, 6, 4, 2, 51, 62, 12, 25 };

            Assert.Throws<ArgumentOutOfRangeException>(() => bytes.ToLong(4));
        }

        [Test]
        public void ToLong_WithStartIndexLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var bytes = new byte[] { 1, 6, 4, 2, 51, 62, 12, 25 };

            Assert.Throws<ArgumentOutOfRangeException>(() => bytes.ToLong(-1));
        }

        [Test]
        public void ToLong_WithStartIndexAfterEnd_ThrowsArgumentOutOfRangeException()
        {
            var bytes = new byte[] { 1, 6, 4, 2, 51, 62, 12, 25 };

            Assert.Throws<ArgumentOutOfRangeException>(() => bytes.ToLong(12));
        }

        [Test]
        public void ToLong_WithValueByteArray_Converts()
        {
            var bytes = new byte[] { 1, 6, 4, 2, 51, 62, 12, 25 };

            var value = bytes.ToLong();

            Assert.AreEqual(1804885939466798593, value);
        }
    }
}
