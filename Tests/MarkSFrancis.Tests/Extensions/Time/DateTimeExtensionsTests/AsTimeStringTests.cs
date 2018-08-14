using MarkSFrancis.Extensions.Time;
using NUnit.Framework;
using System;
using System.Globalization;

namespace MarkSFrancis.Tests.Extensions.Time.DateTimeExtensionsTests
{
    public class AsTimeStringTests
    {
        [Test]
        public void AsTimeString_WithoutFormatProviderAndShortFormat_FormatsAsShortLocalTime()
        {
            var formatProvider = CultureInfo.CurrentCulture.DateTimeFormat;

            var shortTime = Sample.DateTime.AsTimeString();
            var shortTimeShouldBe = Sample.DateTime.ToShortTimeString();

            Assert.AreEqual(shortTimeShouldBe, shortTime);
        }

        [Test]
        public void AsTimeString_WithoutFormatProviderAndLongFormat_FormatsAsLongLocalTime()
        {
            var formatProvider = CultureInfo.CurrentCulture.DateTimeFormat;

            var shortTime = Sample.DateTime.AsTimeString(false);
            var shortTimeShouldBe = Sample.DateTime.ToLongTimeString();

            Assert.AreEqual(shortTimeShouldBe, shortTime);
        }

        [Test]
        public void AsTimeString_WithFormatProviderUSAndShortFormat_FormatsAsShortUSTime()
        {
            var formatProviderUS = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;

            var shortTime = Sample.DateTime.AsTimeString(formatProviderUS);
            var shortTimeShouldBe = Sample.DateTime.ToString("t", formatProviderUS);

            Assert.AreEqual(shortTimeShouldBe, shortTime);
        }

        [Test]
        public void AsTimeString_WithFormatProviderGBAndShortFormat_FormatsAsShortUKTime()
        {
            var formatProviderUK = CultureInfo.GetCultureInfo("en-GB").DateTimeFormat;

            var shortTime = Sample.DateTime.AsTimeString(formatProviderUK);
            var shortTimeShouldBe = Sample.DateTime.ToString("t", formatProviderUK);

            Assert.AreEqual(shortTimeShouldBe, shortTime);
        }

        [Test]
        public void AsTimeString_WithFormatProviderUSAndLongFormat_FormatsAsLongUSTime()
        {
            var formatProviderUS = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;

            var shortTime = Sample.DateTime.AsTimeString(formatProviderUS, false);
            var shortTimeShouldBe = Sample.DateTime.ToString("T", formatProviderUS);

            Assert.AreEqual(shortTimeShouldBe, shortTime);
        }

        [Test]
        public void AsTimeString_WithFormatProviderGBAndLongFormat_FormatsAsLongUKTime()
        {
            var formatProviderUK = CultureInfo.GetCultureInfo("en-GB").DateTimeFormat;

            var shortTime = Sample.DateTime.AsTimeString(formatProviderUK, false);
            var shortTimeShouldBe = Sample.DateTime.ToString("T", formatProviderUK);

            Assert.AreEqual(shortTimeShouldBe, shortTime);
        }

        [Test]
        public void AsTimeString_WithNullFormatProvider_ThrowsArgumentNullException()
        {
            IFormatProvider formatProvider = null;

            Assert.Throws<ArgumentNullException>(() => Sample.DateTime.AsTimeString(formatProvider));
        }
    }
}
