using MarkSFrancis.Extensions.Time;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.Extensions.Time
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void GettingFirstDayOfWeek_WhenFirstDayIsSundayAndDateIsSunday_ReturnsSameDate()
        {
            DateTime testDate = new DateTime(2000, 1, 2);
            var firstDayOfWeek = testDate.StartOfWeek(false);

            Assert.AreEqual(testDate, firstDayOfWeek);
        }
    }
}
