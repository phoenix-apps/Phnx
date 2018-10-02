using NUnit.Framework;
using System;

namespace Phnx.Tests.Extensions.Time.TimeSpanExtensionsTests.ToStringTests
{
    public class RandomCombinationsTests
    {
        [Test]
        public void ToString_WithNoComponents_ReturnsEmptyString()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = string.Empty;

            var resultLongFormat = test.ToString(0, true);
            var resultShortFormat = test.ToString(0);

            Assert.AreEqual(expected, resultLongFormat);
            Assert.AreEqual(expected, resultShortFormat);
        }

        [Test]
        public void ToString_AllComponentsShortFormat_FormatCorrectly()
        {
            TimeSpan test = new TimeSpan(12, 5, 1, 17, 24);
            string expected = "12 days, 05:01:17.024";

            TimeComponents timeComponents =
                TimeComponents.Days |
                TimeComponents.Hours |
                TimeComponents.Minutes |
                TimeComponents.Seconds |
                TimeComponents.Milliseconds;

            var result = test.ToString(timeComponents);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_AllComponentsLongFormat_FormatCorrectly()
        {
            TimeSpan test = new TimeSpan(12, 5, 1, 17, 24);
            string expected = "12 days, 5 hours, 1 minute, 17 seconds, 24 milliseconds";

            TimeComponents timeComponents =
                TimeComponents.Days |
                TimeComponents.Hours |
                TimeComponents.Minutes |
                TimeComponents.Seconds |
                TimeComponents.Milliseconds;

            var result = test.ToString(timeComponents, true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_DaysHoursMinutesShortFormat_FormatCorrectly()
        {
            TimeSpan test = new TimeSpan(12, 5, 1, 17, 24);
            string expected = "12 days, 05:01";

            TimeComponents timeComponents =
                TimeComponents.Days |
                TimeComponents.Hours |
                TimeComponents.Minutes;

            var result = test.ToString(timeComponents);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_DaysMinutesMillisecondsLongFormat_FormatCorrectly()
        {
            TimeSpan test = new TimeSpan(12, 5, 1, 17, 24);
            string expected = "12 days, 1 minute, 24 milliseconds";

            TimeComponents timeComponents =
                TimeComponents.Days |
                TimeComponents.Minutes |
                TimeComponents.Milliseconds;

            var result = test.ToString(timeComponents, true);

            Assert.AreEqual(expected, result);
        }
    }
}
