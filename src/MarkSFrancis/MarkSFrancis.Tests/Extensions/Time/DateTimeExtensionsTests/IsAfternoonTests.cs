using NUnit.Framework;
using System;

namespace MarkSFrancis.Tests.Extensions.Time.DateTimeExtensionsTests
{
    public class IsAfternoonTests
    {
        [Test]
        public void IsAfternoon_ForNoon_ReturnsTrue()
        {
            DateTime test = new DateTime(2000, 1, 1, 12, 0, 0);

            Assert.IsTrue(test.IsAfternoon());
        }

        [Test]
        public void IsAfternoon_ForMidnight_ReturnsFalse()
        {
            DateTime test = new DateTime(2000, 1, 1, 0, 0, 0);

            Assert.IsFalse(test.IsAfternoon());
        }

        [Test]
        public void IsAfternoon_For11_59Am_ReturnsFalse()
        {
            DateTime test = new DateTime(2000, 1, 1, 11, 59, 59);

            Assert.IsFalse(test.IsAfternoon());
        }

        [Test]
        public void IsAfternoon_For11_59Pm_ReturnsTrue()
        {
            DateTime test = new DateTime(2000, 1, 1, 23, 59, 59);

            Assert.IsTrue(test.IsAfternoon());
        }
    }
}
