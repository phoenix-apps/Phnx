using NUnit.Framework;

namespace Phnx.Tests.Extensions.Numeric
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

        [Test]
        public void ConvertingToOrdinal_WithNegativeNumber_GetsNegativeNumber()
        {
            var result = (-100).ToOrdinal();

            Assert.AreEqual("-100", result);
        }

        [Test]
        public void ConvertingToOrdinal_WithZero_GetsOrdinal()
        {
            var result = 0.ToOrdinal();

            Assert.AreEqual("0th", result);
        }

        [Test]
        public void ConvertingToOrdinal_With353_GetsOrdinal()
        {
            var result = 353.ToOrdinal();

            Assert.AreEqual("353rd", result);
        }

        [Test]
        public void ConvertingToOrdinal_With1Or2Or3Or4_GetsOrdinal()
        {
            var result1 = 1.ToOrdinal();
            var result2 = 2.ToOrdinal();
            var result3 = 3.ToOrdinal();
            var result4 = 4.ToOrdinal();

            Assert.AreEqual("1st", result1);
            Assert.AreEqual("2nd", result2);
            Assert.AreEqual("3rd", result3);
            Assert.AreEqual("4th", result4);
        }

        [Test]
        public void ConvertingToOrdinal_With13_GetsOrdinal()
        {
            var result = 13.ToOrdinal();

            Assert.AreEqual("13th", result);
        }
    }
}
