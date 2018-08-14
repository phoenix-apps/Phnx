using MarkSFrancis.Extensions.Time;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.Extensions.Time.DateTimeExtensionsTests
{
    public class FirstDayOfWeekTests
    {
        [Test]
        public void GettingFirstDayOfWeek_WhenFirstDayIsSundayAndDateIsSaturday_ReturnsWeekEndingInDate()
        {
            DateTime testDate = new DateTime(2000, 1, 1);
            var firstDayOfWeek = testDate.StartOfWeek(false);

            Assert.AreEqual(testDate.AddDays(-6), firstDayOfWeek);
        }
    }
}
