using NUnit.Framework;
using System;

namespace Phnx.Tests.Extensions.Time.TimeSpanExtensionsTests.ToStringTests
{
    public class PluralisationTests
    {
        [Test]
        public void ToString_WithSingleDay_DoesNotPluralise()
        {
            TimeSpan test = new TimeSpan(1, 5, 3, 7, 24);
            string expected = "1 day";

            var resultShortFormat = test.ToString(TimeComponents.Days);
            var resultLongFormat = test.ToString(TimeComponents.Days, true);

            Assert.AreEqual(expected, resultShortFormat);
            Assert.AreEqual(expected, resultLongFormat);
        }

        [Test]
        public void ToString_WithSingleHourLongFormat_DoesNotPluralise()
        {
            TimeSpan test = new TimeSpan(12, 1, 3, 7, 24);
            string expected = "1 hour";

            var result = test.ToString(TimeComponents.Hours, true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithSingleMinuteLongFormat_DoesNotPluralise()
        {
            TimeSpan test = new TimeSpan(12, 5, 1, 7, 24);
            string expected = "1 minute";

            var result = test.ToString(TimeComponents.Minutes, true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithSingleSecondLongFormat_DoesNotPluralise()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 1, 24);
            string expected = "1 second";

            var result = test.ToString(TimeComponents.Seconds, true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithSingleMillisecondLongFormat_DoesNotPluralise()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 1);
            string expected = "1 millisecond";

            var result = test.ToString(TimeComponents.Milliseconds, true);

            Assert.AreEqual(expected, result);
        }
    }
}
