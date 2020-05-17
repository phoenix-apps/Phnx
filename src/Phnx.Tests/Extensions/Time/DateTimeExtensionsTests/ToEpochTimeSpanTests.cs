using NUnit.Framework;
using System;

namespace Phnx.Tests.Extensions.Time.DateTimeExtensionsTests
{
    public class ToEpochTimeSpanTests
    {
        [Test]
        public void ToEpochTimeSpan_WithDate_ReturnsCorrectTimeSpan()
        {
            DateTime test = new DateTime(2000, 1, 1);
            TimeSpan expected = TimeSpan.FromDays(10957);

            var result = test.GetTimeSinceEpoch();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToEpochTimeSpan_WithDateAndTime_ReturnsCorrectTimeSpan()
        {
            DateTime test = new DateTime(2000, 1, 1, 16, 5, 32, 264);
            TimeSpan expected = new TimeSpan(10957, 16, 5, 32, 264);

            var result = test.GetTimeSinceEpoch();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToEpochTimeSpan_WithTimeBeforeEpoch_ReturnsNegativeTimeSpan()
        {
            DateTime test = new DateTime(1970, 1, 1).AddTicks(-1);
            TimeSpan expected = new TimeSpan(-1);

            var result = test.GetTimeSinceEpoch();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToEpochTimeSpan_WithDateBeforeEpoch_ReturnsNegativeTimeSpan()
        {
            DateTime test = new DateTime(1969, 12, 31);
            TimeSpan expected = TimeSpan.FromDays(-1);

            var result = test.GetTimeSinceEpoch();

            Assert.AreEqual(expected, result);
        }
    }
}
