using MarkSFrancis.Extensions.Numeric;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.Extensions.Numeric
{
    public class ByteExtensionsTests
    {
        [Test]
        public void RoundToNearest7_WhenValueIs14_ReturnsValue()
        {
            byte value = 14;

            var result = value.RoundToNearest(7);

            Assert.AreEqual(14, result);
        }

        [Test]
        public void RoundToNearest7_WhenValueIs24_Returns21()
        {
            byte value = 24;

            var result = value.RoundToNearest(7);

            Assert.AreEqual(21, result);
        }

        [Test]
        public void RoundToNearest6_WhenValueIs9_Returns12()
        {
            byte value = 9;

            var result = value.RoundToNearest(6);

            Assert.AreEqual(12, result);
        }

        [Test]
        public void RoundToNearest6_WhenValueIs0_Returns0()
        {
            byte value = 0;

            var result = value.RoundToNearest(6);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void ToHexSingleByte_ReturnsValueAsHexidecimalString()
        {
            byte value = 164;

            var result = value.ToHex();

            Assert.AreEqual("A4", result);
        }

        [Test]
        public void ToHexByteArray_WithNullByteArray_ThrowsArgumentNullException()
        {
            byte[] value = null;

            Assert.Throws<ArgumentNullException>(() => value.ToHex());
        }

        [Test]
        public void ToHexByteArray_WithEmptyByteArray_ReturnsEmptyString()
        {
            byte[] value = new byte[0];

            var result = value.ToHex();

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void ToHexByteArray_WithLength5Array_ReturnsValueAsHexidecimalString()
        {
            byte[] value = new byte[] { 164, 241, 61, 67, 0 };

            var result = value.ToHex();

            Assert.AreEqual("A4F13D4300", result);
        }
    }
}
