using MarkSFrancis.Extensions.Time;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.Extensions.Time.TimeSpanExtensionsTests.ToStringTests
{
    public class BadCombinationTests
    {
        [Test]
        public void ToString_WithDaysHoursSecondsShortFormat_ThrowsArgumentException()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);

            Assert.Throws<ArgumentException>(() => test.ToString(TimeComponents.Days | TimeComponents.Hours | TimeComponents.Seconds));
        }

        [Test]
        public void ToString_WithDaysHoursSecondsLongFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "12 days, 5 hours, 7 seconds";

            var result = test.ToString(TimeComponents.Days | TimeComponents.Hours | TimeComponents.Seconds, true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithHoursSecondsShortFormat_ThrowsArgumentException()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);

            Assert.Throws<ArgumentException>(() => test.ToString(TimeComponents.Hours | TimeComponents.Seconds));
        }

        [Test]
        public void ToString_WithHoursSecondsLongFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "5 hours, 7 seconds";

            var result = test.ToString(TimeComponents.Hours | TimeComponents.Seconds, true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithDaysHoursMillisecondsShortFormat_ThrowsArgumentException()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);

            Assert.Throws<ArgumentException>(() => test.ToString(TimeComponents.Days | TimeComponents.Hours | TimeComponents.Milliseconds));
        }

        [Test]
        public void ToString_WithDaysHoursMillisecondsLongFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "12 days, 5 hours, 24 milliseconds";

            var result = test.ToString(TimeComponents.Days | TimeComponents.Hours | TimeComponents.Milliseconds, true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithHoursMillisecondsShortFormat_ThrowsArgumentException()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);

            Assert.Throws<ArgumentException>(() => test.ToString(TimeComponents.Hours | TimeComponents.Milliseconds));
        }

        [Test]
        public void ToString_WithHoursMillisecondsLongFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "5 hours, 24 milliseconds";

            var result = test.ToString(TimeComponents.Hours | TimeComponents.Milliseconds, true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithDaysMinutesMillisecondsShortFormat_ThrowsArgumentException()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);

            Assert.Throws<ArgumentException>(() => test.ToString(TimeComponents.Days | TimeComponents.Minutes | TimeComponents.Milliseconds));
        }

        [Test]
        public void ToString_WithDaysMinutesMillisecondsLongFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "12 days, 3 minutes, 24 milliseconds";

            var result = test.ToString(TimeComponents.Days | TimeComponents.Minutes | TimeComponents.Milliseconds, true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithMinutesMillisecondsShortFormat_ThrowsArgumentException()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);

            Assert.Throws<ArgumentException>(() => test.ToString(TimeComponents.Minutes | TimeComponents.Milliseconds));
        }

        [Test]
        public void ToString_WithMinutesMillisecondsLongFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "3 minutes, 24 milliseconds";

            var result = test.ToString(TimeComponents.Minutes | TimeComponents.Milliseconds, true);

            Assert.AreEqual(expected, result);
        }
    }
}
