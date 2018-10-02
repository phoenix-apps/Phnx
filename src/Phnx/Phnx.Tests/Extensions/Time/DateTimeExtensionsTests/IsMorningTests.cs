using NUnit.Framework;
using System;

namespace Phnx.Tests.Extensions.Time
{
    public class IsMorningTests
    {
        [Test]
        public void IsMorning_ForNoon_ReturnsFalse()
        {
            DateTime test = new DateTime(2000, 1, 1, 12, 0, 0);

            Assert.IsFalse(test.IsMorning());
        }

        [Test]
        public void IsMorning_ForMidnight_ReturnsTrue()
        {
            DateTime test = new DateTime(2000, 1, 1, 0, 0, 0);

            Assert.IsTrue(test.IsMorning());
        }

        [Test]
        public void IsMorning_For11_59Am_ReturnsTrue()
        {
            DateTime test = new DateTime(2000, 1, 1, 11, 59, 59);

            Assert.IsTrue(test.IsMorning());
        }

        [Test]
        public void IsMorning_For11_59Pm_ReturnsFalse()
        {
            DateTime test = new DateTime(2000, 1, 1, 23, 59, 59);

            Assert.IsFalse(test.IsMorning());
        }
    }
}
