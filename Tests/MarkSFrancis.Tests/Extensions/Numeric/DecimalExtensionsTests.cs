using MarkSFrancis.Extensions.Numeric;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.Extensions.Numeric
{
    public class DecimalExtensionsTests
    {
        [Test]
        public void ToMoney_WhenCurrencySymbolIsNull_ThrowsArgumentNullException()
        {
            decimal value = 14;

            Assert.Throws<ArgumentNullException>(() => value.ToMoney(null));
        }

        [Test]
        public void ToMoney_WithTwoDigitDecimalWithGbp_GetsPrice()
        {
            decimal value = 12.99m;

            var worth = value.ToMoney("£");

            Assert.AreEqual("£12.99", worth);
        }

        [Test]
        public void ToMoney_WithTwoDigitDecimalWithFrenchEuro_GetsPrice()
        {
            decimal value = 12.99m;

            var worth = value.ToMoney(" €", false);

            Assert.AreEqual("12.99 €", worth);
        }

        [Test]
        public void ToMoney_WithThreeDigitDecimalWithGbp_RoundsAndGetsPrice()
        {
            decimal value = 12.994m;

            var worth = value.ToMoney("£");

            Assert.AreEqual("£12.99", worth);

            value = 12.995m;

            worth = value.ToMoney("£");

            Assert.AreEqual("£13.00", worth);
        }

        [Test]
        public void RoundToNearest7_WhenValueIs14_ReturnsValue()
        {
            decimal value = 14;

            var result = value.RoundToNearest(7);

            Assert.AreEqual(14, result);
        }

        [Test]
        public void RoundToNearest7_WhenValueIs24AndAQuarter_Returns21()
        {
            decimal value = 24.25m;

            var result = value.RoundToNearest(7);

            Assert.AreEqual(21, result);
        }

        [Test]
        public void RoundToNearest7_WhenValueIs24AndAHalf_Returns28()
        {
            decimal value = 24.5m;

            var result = value.RoundToNearest(7);

            Assert.AreEqual(28, result);
        }

        [Test]
        public void RoundToNearest6_WhenValueIs9_Returns12()
        {
            decimal value = 9;

            var result = value.RoundToNearest(6);

            Assert.AreEqual(12, result);
        }

        [Test]
        public void RoundToNearest6_WhenValueIs0_Returns0()
        {
            decimal value = 0;

            var result = value.RoundToNearest(6);

            Assert.AreEqual(0, result);
        }
    }
}
