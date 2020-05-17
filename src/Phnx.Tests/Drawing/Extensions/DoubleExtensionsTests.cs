using NUnit.Framework;
using System;

namespace Phnx.Drawing.Tests.Extensions
{
    public class DoubleExtensionsTests
    {
        [Test]
        public void ToRadians_WithOneHundredEighty_ReturnsTwoPi()
        {
            var degrees = 180d;

            var radians = degrees.ToRadians();

            Assert.AreEqual(Math.PI, radians);
        }

        [Test]
        public void ToDegrees_WithOnePointFivePi_ReturnsTwoHundredAndSeventy()
        {
            var radians = 1.5 * Math.PI;

            var degrees = radians.ToDegrees();

            Assert.AreEqual(270, degrees);
        }
    }
}
