using NUnit.Framework;
using System;

namespace Phnx.Collections.Tests.Extensions.ByteArrayExtensions
{
    public class ToIntTests
    {
        [Test]
        public void ToInt_WithNullArray_ThrowsArgumentNullException()
        {
            byte[] bytes = null;

            Assert.Throws<ArgumentNullException>(() => bytes.ToInt());
        }

        [Test]
        public void ToInt_WithByteArrayTooShort_ThrowsArgumentException()
        {
            var bytes = new byte[] { 1, 62 };

            Assert.Throws<ArgumentOutOfRangeException>(() => bytes.ToInt());
        }

        [Test]
        public void ToInt_WithStartIndexTooCloseToEnd_ThrowsArgumentOutOfRangeException()
        {
            var bytes = new byte[] { 1, 62, 51, 194 };

            Assert.Throws<ArgumentOutOfRangeException>(() => bytes.ToInt(2));
        }

        [Test]
        public void ToInt_WithStartIndexLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var bytes = new byte[] { 1, 62, 51, 194 };

            Assert.Throws<ArgumentOutOfRangeException>(() => bytes.ToInt(-1));
        }

        [Test]
        public void ToInt_WithStartIndexAfterEnd_ThrowsArgumentOutOfRangeException()
        {
            var bytes = new byte[] { 1, 62, 51, 194 };

            Assert.Throws<ArgumentOutOfRangeException>(() => bytes.ToInt(6));
        }

        [Test]
        public void ToInt_WithValueByteArray_Converts()
        {
            var bytes = new byte[] { 1, 62, 51, 194 };

            var value = bytes.ToInt();

            Assert.AreEqual(-1036829183, value);
        }
    }
}
