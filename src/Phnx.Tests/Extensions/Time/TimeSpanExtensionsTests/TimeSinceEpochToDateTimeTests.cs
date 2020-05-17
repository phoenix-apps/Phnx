using NUnit.Framework;
using System;

namespace Phnx.Tests.Extensions.Time.TimeSpanExtensionsTests
{
    public class TimeSinceEpochToDateTimeTests
    {
        [Test]
        public void TimeSinceEpochToDateTime_With0Time_ReturnsEpoch()
        {
            TimeSpan test = new TimeSpan(0);
            DateTime expected = new DateTime(1970, 1, 1, 0, 0, 0);

            var result = test.TimeSinceEpochToDateTime();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TimeSinceEpochToDateTime_1DayAfter_ReturnsDayAfterEpoch()
        {
            TimeSpan test = TimeSpan.FromDays(1);
            DateTime expected = new DateTime(1970, 1, 2, 0, 0, 0);

            var result = test.TimeSinceEpochToDateTime();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TimeSinceEpochToDateTime_1DayBefore_ReturnsDayAfterEpoch()
        {
            TimeSpan test = TimeSpan.FromDays(-1);
            DateTime expected = new DateTime(1969, 12, 31, 0, 0, 0);

            var result = test.TimeSinceEpochToDateTime();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TimeSinceEpochToDateTime_DateLongAfterEpoch_ReturnsAfterEpoch()
        {
            TimeSpan test = TimeSpan.FromDays(10957);
            DateTime expected = new DateTime(2000, 1, 1, 0, 0, 0);

            var result = test.TimeSinceEpochToDateTime();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TimeSinceEpochToDateTime_DateLongBeforeEpoch_ReturnsBeforeEpoch()
        {
            TimeSpan test = TimeSpan.FromDays(-25567);
            DateTime expected = new DateTime(1900, 1, 1, 0, 0, 0);

            var result = test.TimeSinceEpochToDateTime();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TimeSinceEpochToDateTime_DateTimeLongAfterEpoch_ReturnsAfterEpoch()
        {
            TimeSpan test = new TimeSpan(10957, 8, 37, 10, 792);
            DateTime expected = new DateTime(2000, 1, 1, 8, 37, 10, 792);

            var result = test.TimeSinceEpochToDateTime();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TimeSinceEpochToDateTime_DateTimeLongBeforeEpoch_ReturnsBeforeEpoch()
        {
            TimeSpan test = new TimeSpan(-25567, -8, -37, -10, -792);
            DateTime expected = new DateTime(1899, 12, 31, 15, 22, 49, 208);

            var result = test.TimeSinceEpochToDateTime();

            Assert.AreEqual(expected, result);
        }
    }
}
