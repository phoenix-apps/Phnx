using NUnit.Framework;

namespace Phnx.Tests.Extensions.Numeric
{
    public class LongExtensionsTests
    {
        [Test]
        public void RoundToNearest7_WhenValueIs9223372036854775807_ReturnsValue()
        {
            long value = 9223372036854775807;

            var result = value.RoundToNearest(7);

            Assert.AreEqual(value, result);
        }

        [Test]
        public void RoundToNearest7_WhenValueIs9223372036854775802_Returns9223372036854775800()
        {
            long value = 9223372036854775802;

            var result = value.RoundToNearest(7);

            Assert.AreEqual(9223372036854775800, result);
        }

        [Test]
        public void RoundToNearest6_WhenValueIs9_Returns12()
        {
            long value = 9;

            var result = value.RoundToNearest(6);

            Assert.AreEqual(12, result);
        }

        [Test]
        public void RoundToNearest6_WhenValueIs0_Returns0()
        {
            long value = 0;

            var result = value.RoundToNearest(6);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Converting0ToString_With0MinimumDecimals_Returns0()
        {
            var result = 0L.ToString(0);

            Assert.AreEqual("0", result);
        }

        [Test]
        public void Converting0ToString_With5MinimumDecimals_Returns00000()
        {
            var result = 0L.ToString(5);

            Assert.AreEqual("00000", result);
        }

        [Test]
        public void Converting100ToString_With0MinimumDecimals_Returns100()
        {
            var result = 100L.ToString(0);

            Assert.AreEqual("100", result);
        }

        [Test]
        public void Converting100ToString_With5MinimumDecimals_Returns00100()
        {
            var result = (100L).ToString(5);

            Assert.AreEqual("00100", result);
        }

        [Test]
        public void ConvertingMinus100ToString_With0MinimumDecimals_ReturnsMinus100()
        {
            var result = (-100L).ToString(0);

            Assert.AreEqual("-100", result);
        }

        [Test]
        public void ConvertingMinus100ToString_With5MinimumDecimals_ReturnsMinus00100()
        {
            var result = (-100L).ToString(5);

            Assert.AreEqual("-00100", result);
        }
    }
}
