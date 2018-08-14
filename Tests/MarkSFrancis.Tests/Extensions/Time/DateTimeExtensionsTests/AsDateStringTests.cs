using MarkSFrancis.Extensions.Time;
using NUnit.Framework;
using System.Globalization;

namespace MarkSFrancis.Tests.Extensions.Time.DateTimeExtensionsTests
{
    public class AsDateStringTests
    {

        [Test]
        public void AsDateString_WithoutFormatProviderAndShortFormat_FormatsAsLocal()
        {
            var formatProvider = CultureInfo.CurrentCulture.DateTimeFormat;

            var shortDate = Sample.DateTime.AsDateString();
            var shortDateShouldBe = Sample.DateTime.ToShortDateString();

            Assert.AreEqual(shortDateShouldBe, shortDate);
        }

        [Test]
        public void AsDateString_WithoutFormatProviderAndLongFormat_FormatsAsLocal()
        {
            var formatProvider = CultureInfo.CurrentCulture.DateTimeFormat;

            var shortDate = Sample.DateTime.AsDateString(false);
            var shortDateShouldBe = Sample.DateTime.ToLongDateString();

            Assert.AreEqual(shortDateShouldBe, shortDate);
        }

        [Test]
        public void AsDateString_WithFormatProviderUSAndShortFormat_FormatsAsLocal()
        {
            var formatProviderUS = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;

            var shortDate = Sample.DateTime.AsDateString(formatProviderUS);
            var shortDateShouldBe = Sample.DateTime.ToString("d", formatProviderUS);

            Assert.AreEqual(shortDateShouldBe, shortDate);
        }

        [Test]
        public void AsDateString_WithFormatProviderGBAndShortFormat_FormatsAsLocal()
        {
            var formatProviderUK = CultureInfo.GetCultureInfo("en-GB").DateTimeFormat;

            var shortDate = Sample.DateTime.AsDateString(formatProviderUK);
            var shortDateShouldBe = Sample.DateTime.ToString("d", formatProviderUK);

            Assert.AreEqual(shortDateShouldBe, shortDate);
        }

        [Test]
        public void AsDateString_WithFormatProviderUSAndLongFormat_FormatsAsLocal()
        {
            var formatProviderUS = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;

            var shortDate = Sample.DateTime.AsDateString(formatProviderUS, false);
            var shortDateShouldBe = Sample.DateTime.ToString("D", formatProviderUS);

            Assert.AreEqual(shortDateShouldBe, shortDate);
        }

        [Test]
        public void AsDateString_WithFormatProviderGBAndLongFormat_FormatsAsLocal()
        {
            var formatProviderUK = CultureInfo.GetCultureInfo("en-GB").DateTimeFormat;

            var shortDate = Sample.DateTime.AsDateString(formatProviderUK, false);
            var shortDateShouldBe = Sample.DateTime.ToString("D", formatProviderUK);

            Assert.AreEqual(shortDateShouldBe, shortDate);
        }

        [Test]
        public void AsDateString_WithFormatProviderAndLongFormat_FormatsAsLocal()
        {
            var formatProvider = CultureInfo.CurrentCulture.DateTimeFormat;

            var shortDate = Sample.DateTime.AsDateString(false);
            var shortDateShouldBe = Sample.DateTime.ToLongDateString();

            Assert.AreEqual(shortDateShouldBe, shortDate);
        }
    }
}
