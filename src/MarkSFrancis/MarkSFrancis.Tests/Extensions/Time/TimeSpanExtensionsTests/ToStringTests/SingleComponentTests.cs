using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.Extensions.Time.TimeSpanExtensionsTests.ToStringTests
{
    public class SingleComponentTests
    {
        [Test]
        public void ToString_WithJustDaysShortFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "12 days";

            var result = test.ToString(TimeComponents.Days);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithJustHoursShortFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "05";

            var result = test.ToString(TimeComponents.Hours);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithJustMinutesShortFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "03";

            var result = test.ToString(TimeComponents.Minutes);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithJustSecondsShortFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "07";

            var result = test.ToString(TimeComponents.Seconds);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithJustMillisecondsShortFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "024";

            var result = test.ToString(TimeComponents.Milliseconds);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithJustDaysLongFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "12 days";

            var result = test.ToString(TimeComponents.Days, true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithJustHoursLongFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "5 hours";

            var result = test.ToString(TimeComponents.Hours, true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithJustMinutesLongFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "3 minutes";

            var result = test.ToString(TimeComponents.Minutes, true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithJustSecondsLongFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "7 seconds";

            var result = test.ToString(TimeComponents.Seconds, true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithJustMillisecondsLongFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expected = "24 milliseconds";

            var result = test.ToString(TimeComponents.Milliseconds, true);

            Assert.AreEqual(expected, result);
        }
    }
}
