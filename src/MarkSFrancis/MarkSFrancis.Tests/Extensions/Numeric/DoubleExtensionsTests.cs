using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.Extensions.Numeric
{
    public class DoubleExtensionsTests
    {
        [Test]
        public void ToMoney_WhenCurrencySymbolIsNull_ThrowsArgumentNullException()
        {
            double value = 14;

            Assert.Throws<ArgumentNullException>(() => value.ToMoney(null));
        }

        [Test]
        public void ToMoney_WithTwoDigitDoubleWithGbp_GetsPrice()
        {
            double value = 12.99;

            var worth = value.ToMoney("£");

            Assert.AreEqual("£12.99", worth);
        }

        [Test]
        public void ToMoney_WithTwoDigitDoubleWithFrenchEuro_GetsPrice()
        {
            double value = 12.99;

            var worth = value.ToMoney(" €", false);

            Assert.AreEqual("12.99 €", worth);
        }

        [Test]
        public void ToMoney_WithThreeDigitDoubleWithGbp_RoundsAndGetsPrice()
        {
            double value = 12.994;

            var worth = value.ToMoney("£");

            Assert.AreEqual("£12.99", worth);

            value = 12.995;

            worth = value.ToMoney("£");

            Assert.AreEqual("£13.00", worth);

            value = 12.90;

            worth = value.ToMoney("£");

            Assert.AreEqual("£12.90", worth);
        }

        [Test]
        public void RoundToNearest7_WhenValueIs14_ReturnsValue()
        {
            double value = 14;

            var result = value.RoundToNearest(7);

            Assert.AreEqual(14, result);
        }

        [Test]
        public void RoundToNearest7_WhenValueIs24AndAQuarter_Returns21()
        {
            double value = 24.25;

            var result = value.RoundToNearest(7);

            Assert.AreEqual(21, result);
        }

        [Test]
        public void RoundToNearest7_WhenValueIs24AndAHalf_Returns28()
        {
            double value = 24.5;

            var result = value.RoundToNearest(7);

            Assert.AreEqual(28, result);
        }

        [Test]
        public void RoundToNearest6_WhenValueIs9_Returns12()
        {
            double value = 9;

            var result = value.RoundToNearest(6);

            Assert.AreEqual(12, result);
        }

        [Test]
        public void RoundToNearest6_WhenValueIs0_Returns0()
        {
            double value = 0;

            var result = value.RoundToNearest(6);

            Assert.AreEqual(0, result);
        }
    }
}
