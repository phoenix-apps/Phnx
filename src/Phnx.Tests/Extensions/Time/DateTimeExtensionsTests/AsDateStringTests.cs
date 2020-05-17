using NUnit.Framework;
using System;
using System.Globalization;

namespace Phnx.Tests.Extensions.Time.DateTimeExtensionsTests
{
    public class AsDateStringTests
    {
        [Test]
        public void AsDateString_WithoutFormatProviderAndShortFormat_FormatsAsShortLocalDate()
        {
            var formatProvider = CultureInfo.CurrentCulture.DateTimeFormat;
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);

            var shortDate = sampleNow.AsDateString();
            var shortDateShouldBe = sampleNow.ToShortDateString();

            Assert.AreEqual(shortDateShouldBe, shortDate);
        }

        [Test]
        public void AsDateString_WithoutFormatProviderAndLongFormat_FormatsAsLongLocalDate()
        {
            var formatProvider = CultureInfo.CurrentCulture.DateTimeFormat;
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);

            var shortDate = sampleNow.AsDateString(false);
            var shortDateShouldBe = sampleNow.ToLongDateString();

            Assert.AreEqual(shortDateShouldBe, shortDate);
        }

        [Test]
        public void AsDateString_WithFormatProviderUSAndShortFormat_FormatsAsShortUSDate()
        {
            var formatProviderUS = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);

            var shortDate = sampleNow.AsDateString(formatProviderUS);
            var shortDateShouldBe = "9/4/2012";

            Assert.AreEqual(shortDateShouldBe, shortDate);
        }

        [Test]
        public void AsDateString_WithFormatProviderGBAndShortFormat_FormatsAsShortGBDate()
        {
            var formatProviderUK = CultureInfo.GetCultureInfo("en-GB").DateTimeFormat;
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);

            var shortDate = sampleNow.AsDateString(formatProviderUK);
            var shortDateShouldBe = "04/09/2012";

            Assert.AreEqual(shortDateShouldBe, shortDate);
        }

        [Test]
        public void AsDateString_WithFormatProviderUSAndLongFormat_FormatsAsLongUSDate()
        {
            var formatProviderUS = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);

            var shortDate = sampleNow.AsDateString(formatProviderUS, false);
            var shortDateShouldBe = "Tuesday, September 4, 2012";

            Assert.AreEqual(shortDateShouldBe, shortDate);
        }

        [Test]
        public void AsDateString_WithFormatProviderGBAndLongFormat_FormatsAsLongUKDate()
        {
            var formatProviderUK = CultureInfo.GetCultureInfo("en-GB").DateTimeFormat;
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);

            var shortDate = sampleNow.AsDateString(formatProviderUK, false);
            var shortDateShouldBe = "04 September 2012";

            Assert.AreEqual(shortDateShouldBe, shortDate);
        }

        [Test]
        public void AsDateString_WithNullFormatProvider_ThrowsArgumentNullException()
        {
            IFormatProvider formatProvider = null;
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);

            Assert.Throws<ArgumentNullException>(() => sampleNow.AsDateString(formatProvider));
        }
    }
}
