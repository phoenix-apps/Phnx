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
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);

            var shortTime = sampleNow.AsTimeString();
            var shortTimeShouldBe = sampleNow.ToShortTimeString();

            Assert.AreEqual(shortTimeShouldBe, shortTime);
        }

        [Test]
        public void AsTimeString_WithoutFormatProviderAndLongFormat_FormatsAsLongLocalTime()
        {
            var formatProvider = CultureInfo.CurrentCulture.DateTimeFormat;
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);

            var shortTime = sampleNow.AsTimeString(false);
            var shortTimeShouldBe = sampleNow.ToLongTimeString();

            Assert.AreEqual(shortTimeShouldBe, shortTime);
        }

        [Test]
        public void AsTimeString_WithFormatProviderUSAndShortFormat_FormatsAsShortUSTime()
        {
            var formatProviderUS = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);

            var shortTime = sampleNow.AsTimeString(formatProviderUS);
            var shortTimeShouldBe = "12:15 PM";

            Assert.AreEqual(shortTimeShouldBe, shortTime);
        }

        [Test]
        public void AsTimeString_WithFormatProviderGBAndShortFormat_FormatsAsShortUKTime()
        {
            var formatProviderUK = CultureInfo.GetCultureInfo("en-GB").DateTimeFormat;
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);

            var shortTime = sampleNow.AsTimeString(formatProviderUK);
            var shortTimeShouldBe = "12:15";

            Assert.AreEqual(shortTimeShouldBe, shortTime);
        }

        [Test]
        public void AsTimeString_WithFormatProviderUSAndLongFormat_FormatsAsLongUSTime()
        {
            var formatProviderUS = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);

            var shortTime = sampleNow.AsTimeString(formatProviderUS, false);
            var shortTimeShouldBe = "12:15:36 PM";

            Assert.AreEqual(shortTimeShouldBe, shortTime);
        }

        [Test]
        public void AsTimeString_WithFormatProviderGBAndLongFormat_FormatsAsLongUKTime()
        {
            var formatProviderUK = CultureInfo.GetCultureInfo("en-GB").DateTimeFormat;
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);

            var shortTime = sampleNow.AsTimeString(formatProviderUK, false);
            var shortTimeShouldBe = "12:15:36";

            Assert.AreEqual(shortTimeShouldBe, shortTime);
        }

        [Test]
        public void AsTimeString_WithNullFormatProvider_ThrowsArgumentNullException()
        {
            IFormatProvider formatProvider = null;
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);

            Assert.Throws<ArgumentNullException>(() => sampleNow.AsTimeString(formatProvider));
        }
    }
}
