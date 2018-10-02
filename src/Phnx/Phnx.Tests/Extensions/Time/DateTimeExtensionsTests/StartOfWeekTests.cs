using NUnit.Framework;
using System;

namespace Phnx.Tests.Extensions.Time.DateTimeExtensionsTests
{
    public class StartOfWeekTests
    {
        [Test]
        public void StartOfWeek_WhenFirstDayIsSundayAndDateIsSaturday_ReturnsPreviousSunday()
        {
            DateTime testDate = new DateTime(2000, 1, 1);
            DateTime expected = new DateTime(1999, 12, 26);

            var result = testDate.StartOfWeek(false);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void StartOfWeek_WhenFirstDayIsSundayAndDateIsWednesday_ReturnsPreviousSunday()
        {
            DateTime testDate = new DateTime(2000, 1, 19);
            DateTime expected = new DateTime(2000, 1, 16);

            var result = testDate.StartOfWeek(false);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void StartOfWeek_WhenFirstDayIsSundayAndDateIsSunday_ReturnsSameDate()
        {
            DateTime testDate = new DateTime(2000, 1, 2);

            var result = testDate.StartOfWeek(false);

            Assert.AreEqual(testDate, result);
        }

        [Test]
        public void StartOfWeek_WhenFirstDayIsMondayAndDateIsSunday_ReturnsPreviousMonday()
        {
            DateTime testDate = new DateTime(2000, 1, 2);
            DateTime expected = new DateTime(1999, 12, 27);

            var result = testDate.StartOfWeek();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void StartOfWeek_WhenFirstDayIsMondayAndDateIsThursday_ReturnsPreviousMonday()
        {
            DateTime testDate = new DateTime(2000, 1, 20);
            DateTime expected = new DateTime(2000, 1, 17);

            var result = testDate.StartOfWeek(true);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void StartOfWeek_WhenFirstDayIsMondayAndDateIsMonday_ReturnsSameDate()
        {
            DateTime testDate = new DateTime(2000, 1, 3);

            var result = testDate.StartOfWeek();

            Assert.AreEqual(testDate, result);
        }
    }
}
