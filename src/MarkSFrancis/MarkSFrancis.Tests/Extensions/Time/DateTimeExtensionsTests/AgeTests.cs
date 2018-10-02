using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.Extensions.Time.DateTimeExtensionsTests
{
    public class AgeTests
    {
        [Test]
        public void GetAge_WhenDobIsSameDayOfYear_ReturnsAge()
        {
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);
            DateTime dob = new DateTime(2000, sampleNow.Month, sampleNow.Day);
            var age = dob.Age(sampleNow);

            Assert.AreEqual(12, age);
        }

        [Test]
        public void GetAge_WhenDobIsLaterInYear_ReturnsAge()
        {
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);
            DateTime dob = new DateTime(2000, sampleNow.Month, sampleNow.Day).AddMonths(1);
            var age = dob.Age(sampleNow);

            Assert.AreEqual(11, age);
        }

        [Test]
        public void GetAge_WhenDobIsEarlierInYear_ReturnsAge()
        {
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);
            DateTime dob = new DateTime(2000, sampleNow.Month, sampleNow.Day).AddMonths(-1);
            var age = dob.Age(sampleNow);

            Assert.AreEqual(12, age);
        }

        [Test]
        public void GetAge_WhenDobIsInFuture_Returns0()
        {
            DateTime sampleNow = new DateTime(2012, 9, 4, 12, 15, 36);
            DateTime dob = sampleNow.AddYears(3).AddMonths(3).AddDays(6);

            var age = dob.Age(sampleNow);

            Assert.AreEqual(0, age);
        }
    }
}
