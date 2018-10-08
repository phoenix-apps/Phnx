using NUnit.Framework;
using Phnx.Drawing.Shapes;
using System;
using System.Drawing;
using System.Linq;

namespace Phnx.Drawing.Tests.Shapes
{
    public class EllipsesTests
    {
        [Test]
        public void Ellipse_IsATypeOfShape()
        {
            var interfaces = typeof(Ellipse).GetInterfaces();
            var isIShape = interfaces.Contains(typeof(IShape));
            Assert.IsTrue(isIShape);
        }

        [Test]
        public void NewEllipse_WithNegativeMinorRadius_ThrowsArgumentLessThanZeroException()
        {
            Assert.Throws<ArgumentLessThanZeroException>(() => new Ellipse(new Point(0, 0), -1, 0, 0));
        }

        [Test]
        public void NewEllipse_WithNegativeMajorRadius_ThrowsArgumentLessThanZeroException()
        {
            Assert.Throws<ArgumentLessThanZeroException>(() => new Ellipse(0, 0, 0, -1, 0));
        }

        [Test]
        public void NewEllipse_WithMajorRadiusLessThanMinor_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Ellipse(0, 0, 1, 0, 0));
        }

        [Test]
        public void NewEllipse_WithNegativeAngle_LoopsForwardAndDoesNotThrow()
        {
            var ellipse = new Ellipse(0, 0, 0, 0, -50);

            Assert.AreEqual(310, ellipse.MinorRadiusAngle);
        }

        [Test]
        public void NewEllipse_WithMinorRadiusAngleOver360_LoopsBackAndDoesNotThrow()
        {
            var ellipse = new Ellipse(0, 0, 0, 0, 710);

            Assert.AreEqual(350, ellipse.MinorRadiusAngle);
        }

        [Test]
        public void NewEllipse_WithPositiveRadiuses_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new Ellipse(0, 0, 0, 0, 0));
        }

        [Test]
        public void NewEllipse_SetsCenterPointAndRadius()
        {
            int x = 51, y = -12, minorRadius = 40, majorRadius = 60, minorAngle = 83;

            var Ellipse = new Ellipse(x, y, minorRadius, majorRadius, minorAngle);

            Assert.AreEqual(x, Ellipse.X);
            Assert.AreEqual(x, Ellipse.CenterCoordinates.X);
            Assert.AreEqual(y, Ellipse.Y);
            Assert.AreEqual(y, Ellipse.CenterCoordinates.Y);
            Assert.AreEqual(minorRadius, Ellipse.MinorRadiusSize);
            Assert.AreEqual(majorRadius, Ellipse.MajorRadiusSize);
            Assert.AreEqual(minorAngle, Ellipse.MinorRadiusAngle);
        }

        [Test]
        public void Area_WithZeroRadius_ReturnsZero()
        {
            var Ellipse = new Ellipse(new Point(5, 5), 0, 0, 90);

            var area = Ellipse.Area;

            Assert.AreEqual(0, area);
        }

        [Test]
        public void Area_With5By10Radius_GetsArea()
        {
            var Ellipse = new Ellipse(-20, 17, 5, 10, 270);

            var area = Ellipse.Area;

            Assert.AreEqual(157.07963267948966d, area);
        }
    }
}
