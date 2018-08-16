using MarkSFrancis.Extensions.Time;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.Extensions.Time.DateTimeExtensionsTests
{
    public class EndOfWeekTests
    {
        [Test]
        public void EndOfWeek_WhenFirstDayIsSundayAndDateIsSunday_ReturnsEndOfNextSaturday()
        {
            DateTime testDate = new DateTime(2000, 1, 2);
            DateTime expected = new DateTime(2000, 1, 9).AddTicks(-1);

            var result = testDate.EndOfWeek(false);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void StartOfWeek_WhenFirstDayIsSundayAndDateIsWednesday_ReturnsEndOfNextSaturday()
        {
            DateTime testDate = new DateTime(2000, 1, 19);
            DateTime expected = new DateTime(2000, 1, 23).AddTicks(-1);

            var result = testDate.EndOfWeek(false);

            Assert.AreEqual(expected, result);
        }
        [Test]
        public void EndOfWeek_WhenFirstDayIsSundayAndDateIsSaturday_ReturnsEndOfSameDay()
        {
            DateTime testDate = new DateTime(2000, 1, 1);
            DateTime expected = new DateTime(2000, 1, 2).AddTicks(-1);

            var result = testDate.EndOfWeek(false);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EndOfWeek_WhenFirstDayIsMondayAndDateIsMonday_ReturnsEndOfNextSunday()
        {
            DateTime testDate = new DateTime(2000, 1, 3);
            DateTime expected = new DateTime(2000, 1, 10).AddTicks(-1);

            var result = testDate.EndOfWeek();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EndOfWeek_WhenFirstDayIsMondayAndDateIsThursday_ReturnsEndOfNextSunday()
        {
            DateTime testDate = new DateTime(2000, 1, 20);
            DateTime expected = new DateTime(2000, 1, 24).AddTicks(-1);

            var result = testDate.EndOfWeek(true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EndOfWeek_WhenFirstDayIsMondayAndDateIsSunday_ReturnsEndOfSameDay()
        {
            DateTime testDate = new DateTime(2000, 1, 2);
            DateTime expected = new DateTime(2000, 1, 3).AddTicks(-1);

            var result = testDate.EndOfWeek();

            Assert.AreEqual(expected, result);
        }
    }
}
