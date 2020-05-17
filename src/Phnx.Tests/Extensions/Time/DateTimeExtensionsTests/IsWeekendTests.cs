using NUnit.Framework;
using System;

namespace Phnx.Tests.Extensions.Time.DateTimeExtensionsTests
{
    public class IsWeekendTests
    {
        [Test]
        public void IsWeekend_OnSaturday_ReturnsTrue()
        {
            DateTime test = new DateTime(2000, 1, 1);

            Assert.IsTrue(test.IsWeekend());
        }

        [Test]
        public void IsWeekend_OnSunday_ReturnsTrue()
        {
            DateTime test = new DateTime(2000, 1, 2);

            Assert.IsTrue(test.IsWeekend());
        }

        [Test]
        public void IsWeekend_OnMonday_ReturnsFalse()
        {
            DateTime test = new DateTime(1999, 12, 27);

            Assert.IsFalse(test.IsWeekend());
        }

        [Test]
        public void IsWeekend_OnTuesday_ReturnsFalse()
        {
            DateTime test = new DateTime(1999, 12, 28);

            Assert.IsFalse(test.IsWeekend());
        }

        [Test]
        public void IsWeekend_OnWednesday_ReturnsFalse()
        {
            DateTime test = new DateTime(1999, 12, 29);

            Assert.IsFalse(test.IsWeekend());
        }

        [Test]
        public void IsWeekend_OnThursday_ReturnsFalse()
        {
            DateTime test = new DateTime(1999, 12, 30);

            Assert.IsFalse(test.IsWeekend());
        }

        [Test]
        public void IsWeekend_OnFriday_ReturnsFalse()
        {
            DateTime test = new DateTime(1999, 12, 31);

            Assert.IsFalse(test.IsWeekend());
        }
    }
}
