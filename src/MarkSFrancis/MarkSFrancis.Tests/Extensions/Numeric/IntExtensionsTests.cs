using NUnit.Framework;

namespace MarkSFrancis.Tests.Extensions.Numeric
{
    public class IntExtensionsTests
    {
        [Test]
        public void RoundToNearest7_WhenValueIs2147483646_ReturnsValue()
        {
            int value = 2147483646;

            var result = value.RoundToNearest(7);

            Assert.AreEqual(2147483646, result);
        }

        [Test]
        public void RoundToNearest7_WhenValueIs2147483647_Returns2147483646()
        {
            int value = 2147483647;

            var result = value.RoundToNearest(7);

            Assert.AreEqual(2147483646, result);
        }

        [Test]
        public void RoundToNearest6_WhenValueIs9_Returns12()
        {
            int value = 9;

            var result = value.RoundToNearest(6);

            Assert.AreEqual(12, result);
        }

        [Test]
        public void RoundToNearest6_WhenValueIs0_Returns0()
        {
            int value = 0;

            var result = value.RoundToNearest(6);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Converting0ToString_With0MinimumDecimals_Returns0()
        {
            var result = 0.ToString(0);

            Assert.AreEqual("0", result);
        }

        [Test]
        public void Converting0ToString_With5MinimumDecimals_Returns00000()
        {
            var result = 0.ToString(5);

            Assert.AreEqual("00000", result);
        }

        [Test]
        public void Converting100ToString_With0MinimumDecimals_Returns100()
        {
            var result = 100.ToString(0);

            Assert.AreEqual("100", result);
        }

        [Test]
        public void Converting100ToString_With5MinimumDecimals_Returns00100()
        {
            var result = (100).ToString(5);

            Assert.AreEqual("00100", result);
        }

        [Test]
        public void ConvertingMinus100ToString_With0MinimumDecimals_ReturnsMinus100()
        {
            var result = (-100).ToString(0);

            Assert.AreEqual("-100", result);
        }

        [Test]
        public void ConvertingMinus100ToString_With5MinimumDecimals_ReturnsMinus00100()
        {
            var result = (-100).ToString(5);

            Assert.AreEqual("-00100", result);
        }
    }
}
