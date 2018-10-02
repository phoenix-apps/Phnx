using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.Extensions.Time.DateTimeExtensionsTests
{
    public class IsSameDayTests
    {
        [Test]
        public void IsSameDay_WhenDateTimesAreSameAndMidnight_ReturnsTrue()
        {
            DateTime test = new DateTime(2000, 1, 1);

            Assert.IsTrue(test.IsSameDay(test));
        }

        [Test]
        public void IsSameDay_WhenDateTimesAreSame_ReturnsTrue()
        {
            DateTime test = new DateTime(2000, 1, 1, 13, 12, 06);

            Assert.IsTrue(test.IsSameDay(test));
        }

        [Test]
        public void IsSameDay_WhenDateTimesAreDifferentTimeSameDay_ReturnsTrue()
        {
            DateTime time1 = new DateTime(2000, 1, 1, 12, 0, 0);
            DateTime time2 = new DateTime(2000, 1, 1, 17, 25, 6);

            Assert.IsTrue(time1.IsSameDay(time2));
            Assert.IsTrue(time2.IsSameDay(time1));
        }

        [Test]
        public void IsSameDay_WhenDateTimesAreDifferentDatesSameTime_ReturnsFalse()
        {
            DateTime time1 = new DateTime(2000, 1, 2, 12, 0, 0);
            DateTime time2 = new DateTime(2000, 1, 1, 12, 0, 0);

            Assert.IsFalse(time1.IsSameDay(time2));
        }

        [Test]
        public void IsSameDay_WhenDateTimesAreDifferentDatesDifferentTime_ReturnsFalse()
        {
            DateTime time1 = new DateTime(2000, 1, 2, 12, 0, 0);
            DateTime time2 = new DateTime(2000, 1, 1, 7, 1, 0);

            Assert.IsFalse(time1.IsSameDay(time2));
        }

        [Test]
        public void IsSameDay_WhenDateTimesAreDifferentYears_ReturnsFalse()
        {
            DateTime time1 = new DateTime(1999, 1, 1, 0, 0, 0);
            DateTime time2 = new DateTime(2000, 1, 1, 0, 0, 0);

            Assert.IsFalse(time1.IsSameDay(time2));
        }

        [Test]
        public void IsSameDay_WhenDateTimesAreDifferentMonths_ReturnsFalse()
        {
            DateTime time1 = new DateTime(2000, 2, 1, 0, 0, 0);
            DateTime time2 = new DateTime(2000, 1, 1, 0, 0, 0);

            Assert.IsFalse(time1.IsSameDay(time2));
        }
    }
}
