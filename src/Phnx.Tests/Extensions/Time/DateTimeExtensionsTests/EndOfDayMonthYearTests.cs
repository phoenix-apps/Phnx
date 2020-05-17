using NUnit.Framework;
using System;

namespace Phnx.Tests.Extensions.Time.DateTimeExtensionsTests
{
    public class EndOfDayMonthYearTests
    {
        #region End of Day
        [Test]
        public void GetEndOfDay_WhenTimeIsInMiddleOfDay_GetsEndOfDay()
        {
            DateTime sampleNow = new DateTime(2000, 1, 1, 12, 0, 0);
            DateTime endOfDayShouldBe = new DateTime(2000, 1, 2).AddTicks(-1);

            var endOfDay = sampleNow.EndOfDay();

            Assert.AreEqual(endOfDayShouldBe, endOfDay);
        }

        [Test]
        public void GetEndOfDay_WhenTimeIsInMorning_GetsEndOfDay()
        {
            DateTime sampleNow = new DateTime(2000, 1, 1, 0, 0, 0);
            DateTime endOfDayShouldBe = new DateTime(2000, 1, 2).AddTicks(-1);

            var endOfDay = sampleNow.EndOfDay();

            Assert.AreEqual(endOfDayShouldBe, endOfDay);
        }

        [Test]
        public void GetEndOfDay_WhenTimeIsInEvening_GetsEndOfDay()
        {
            DateTime sampleNow = new DateTime(2000, 1, 1, 23, 0, 0);
            DateTime endOfDayShouldBe = new DateTime(2000, 1, 2).AddTicks(-1);

            var endOfDay = sampleNow.EndOfDay();

            Assert.AreEqual(endOfDayShouldBe, endOfDay);
        }

        [Test]
        public void GetEndOfDay_WhenDayCrossesMonth_GetsEndOfDay()
        {
            DateTime sampleNow = new DateTime(2000, 6, 30, 23, 0, 0);
            DateTime endOfDayShouldBe = new DateTime(2000, 7, 1).AddTicks(-1);

            var endOfDay = sampleNow.EndOfDay();

            Assert.AreEqual(endOfDayShouldBe, endOfDay);
        }

        [Test]
        public void GetEndOfDay_WhenDayCrossesYear_GetsEndOfDay()
        {
            DateTime sampleNow = new DateTime(2000, 12, 31, 23, 0, 0);
            DateTime endOfDayShouldBe = new DateTime(2001, 1, 1).AddTicks(-1);

            var endOfDay = sampleNow.EndOfDay();

            Assert.AreEqual(endOfDayShouldBe, endOfDay);
        }
        #endregion

        #region EndOfMonth
        [Test]
        public void GetEndOfMonth_WhenDateInMiddleOfMonth_GetsEndOfMonth()
        {
            DateTime sampleNow = new DateTime(2000, 1, 15, 12, 0, 0);
            DateTime endOfDayShouldBe = new DateTime(2000, 2, 1).AddTicks(-1);

            var endOfDay = sampleNow.EndOfMonth();

            Assert.AreEqual(endOfDayShouldBe, endOfDay);
        }

        [Test]
        public void GetEndOfMonth_WhenDateInStartOfMonth_GetsEndOfMonth()
        {
            DateTime sampleNow = new DateTime(2000, 1, 1, 0, 0, 0);
            DateTime endOfDayShouldBe = new DateTime(2000, 2, 1).AddTicks(-1);

            var endOfDay = sampleNow.EndOfMonth();

            Assert.AreEqual(endOfDayShouldBe, endOfDay);
        }

        [Test]
        public void GetEndOfMonth_WhenDateInEndOfMonth_GetsEndOfMonth()
        {
            DateTime sampleNow = new DateTime(2000, 1, 31, 23, 0, 0);
            DateTime endOfDayShouldBe = new DateTime(2000, 2, 1).AddTicks(-1);

            var endOfDay = sampleNow.EndOfMonth();

            Assert.AreEqual(endOfDayShouldBe, endOfDay);
        }

        [Test]
        public void GetEndOfMonth_WhenMonthCrossesYear_GetsEndOfMonth()
        {
            DateTime sampleNow = new DateTime(2000, 12, 31, 23, 0, 0);
            DateTime endOfDayShouldBe = new DateTime(2001, 1, 1).AddTicks(-1);

            var endOfDay = sampleNow.EndOfMonth();

            Assert.AreEqual(endOfDayShouldBe, endOfDay);
        }
        #endregion

        #region EndOfYear
        [Test]
        public void GetEndOfYear_WhenDateInMiddleOfYear_GetsEndOfYear()
        {
            DateTime sampleNow = new DateTime(2000, 6, 15, 12, 0, 0);
            DateTime endOfDayShouldBe = new DateTime(2001, 1, 1).AddTicks(-1);

            var endOfDay = sampleNow.EndOfYear();

            Assert.AreEqual(endOfDayShouldBe, endOfDay);
        }

        [Test]
        public void GetEndOfYear_WhenDateInStartOfYear_GetsEndOfYear()
        {
            DateTime sampleNow = new DateTime(2000, 1, 1, 0, 0, 0);
            DateTime endOfDayShouldBe = new DateTime(2001, 1, 1).AddTicks(-1);

            var endOfDay = sampleNow.EndOfYear();

            Assert.AreEqual(endOfDayShouldBe, endOfDay);
        }

        [Test]
        public void GetEndOfYear_WhenDateInEndOfYear_GetsEndOfYear()
        {
            DateTime sampleNow = new DateTime(2000, 12, 31, 23, 0, 0);
            DateTime endOfDayShouldBe = new DateTime(2001, 1, 1).AddTicks(-1);

            var endOfDay = sampleNow.EndOfYear();

            Assert.AreEqual(endOfDayShouldBe, endOfDay);
        }
        #endregion
    }
}
