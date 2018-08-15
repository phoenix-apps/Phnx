using MarkSFrancis.Extensions.Time;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.Extensions.Time.TimeSpanExtensionsTests
{
    public class ToStringTests
    {
        [Test]
        public void ToString_WithJustDaysShortFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expectedResult = "12 days";

            var result = test.ToString(TimeComponents.Days);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ToString_WithJustHoursShortFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expectedResult = "05";

            var result = test.ToString(TimeComponents.Hours);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ToString_WithJustMinutesShortFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expectedResult = "03";

            var result = test.ToString(TimeComponents.Minutes);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ToString_WithJustSecondsShortFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expectedResult = "07";

            var result = test.ToString(TimeComponents.Seconds);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ToString_WithJustMillisecondsShortFormat_Converts()
        {
            TimeSpan test = new TimeSpan(12, 5, 3, 7, 24);
            string expectedResult = "024";

            var result = test.ToString(TimeComponents.Milliseconds);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
