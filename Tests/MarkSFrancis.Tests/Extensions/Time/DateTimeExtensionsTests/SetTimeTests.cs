using MarkSFrancis.Extensions.Time;
using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.Extensions.Time.DateTimeExtensionsTests
{
    public class SetTimeTests
    {
        [Test]
        public void SetTime_WhenNewTimeIsValid_UpdatesHoursMinutesSecondsMilliseconds()
        {
            DateTime testTime = new DateTime(2000, 1, 1, 0, 0, 0);
            DateTime expectedTime = new DateTime(2000, 1, 1, 12, 30, 16, 2);

            testTime = testTime.SetTime(12, 30, 16, 2);

            Assert.AreEqual(expectedTime, testTime);
        }

        [Test]
        public void SetTime_WhenNewTimeIsOnlySeconds_UpdatesOnlySeconds()
        {
            DateTime testTime = new DateTime(2000, 1, 1, 17, 24, 13, 53);
            DateTime expectedTime = new DateTime(2000, 1, 1, 17, 24, 16, 53);

            testTime = testTime.SetTime(second: 16);

            Assert.AreEqual(expectedTime, testTime);
        }

        [Test]
        public void SetTime_WhenHourIsNegative_ThrowsArgumentOutOfRangeException()
        {
            DateTime testTime = new DateTime(2000, 1, 1, 17, 24, 13, 53);

            Assert.Throws<ArgumentOutOfRangeException>(() => testTime.SetTime(hour: -1));
        }

        [Test]
        public void SetTime_WhenHourIsTooLarge_ThrowsArgumentOutOfRangeException()
        {
            DateTime testTime = new DateTime(2000, 1, 1, 17, 24, 13, 53);

            Assert.Throws<ArgumentOutOfRangeException>(() => testTime.SetTime(hour: 25));
        }

        [Test]
        public void SetTime_WhenMinuteIsNegative_ThrowsArgumentOutOfRangeException()
        {
            DateTime testTime = new DateTime(2000, 1, 1, 17, 24, 13, 53);

            Assert.Throws<ArgumentOutOfRangeException>(() => testTime.SetTime(minute: -1));
        }

        [Test]
        public void SetTime_WhenMinuteIsTooLarge_ThrowsArgumentOutOfRangeException()
        {
            DateTime testTime = new DateTime(2000, 1, 1, 17, 24, 13, 53);

            Assert.Throws<ArgumentOutOfRangeException>(() => testTime.SetTime(minute: 60));
        }

        [Test]
        public void SetTime_WhenSecondIsNegative_ThrowsArgumentOutOfRangeException()
        {
            DateTime testTime = new DateTime(2000, 1, 1, 17, 24, 13, 53);

            Assert.Throws<ArgumentOutOfRangeException>(() => testTime.SetTime(second: -1));
        }

        [Test]
        public void SetTime_WhenSecondIsTooLarge_ThrowsArgumentOutOfRangeException()
        {
            DateTime testTime = new DateTime(2000, 1, 1, 17, 24, 13, 53);

            Assert.Throws<ArgumentOutOfRangeException>(() => testTime.SetTime(second: 60));
        }

        [Test]
        public void SetTime_WhenMillisecondIsNegative_ThrowsArgumentOutOfRangeException()
        {
            DateTime testTime = new DateTime(2000, 1, 1, 17, 24, 13, 53);

            Assert.Throws<ArgumentOutOfRangeException>(() => testTime.SetTime(millisecond: -1));
        }

        [Test]
        public void SetTime_WhenMillisecondIsTooLarge_ThrowsArgumentOutOfRangeException()
        {
            DateTime testTime = new DateTime(2000, 1, 1, 17, 24, 13, 53);

            Assert.Throws<ArgumentOutOfRangeException>(() => testTime.SetTime(millisecond: 1000));
        }
    }
}
