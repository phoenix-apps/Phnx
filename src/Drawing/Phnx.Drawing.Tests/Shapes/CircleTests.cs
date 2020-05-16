using NUnit.Framework;
using Phnx.Drawing.Shapes;
using System;
using System.Drawing;
using System.Linq;

namespace Phnx.Drawing.Tests.Shapes
{
    public class CircleTests
    {
        [Test]
        public void Circle_IsATypeOfShape()
        {
            var interfaces = typeof(Circle).GetInterfaces();
            var isIShape = interfaces.Contains(typeof(IShape));
            Assert.IsTrue(isIShape);
        }

        [Test]
        public void NewCircle_WithNegativeRadius_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Circle(new Point(0, 0), -1));
        }

        [Test]
        public void NewCircle_WithPositiveRadius_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new Circle(new Point(0, 0), 0));
        }

        [Test]
        public void NewCircle_SetsCenterPointAndRadius()
        {
            int x = 51, y = -12, radius = 60;

            var circle = new Circle(x, y, radius);

            Assert.AreEqual(x, circle.X);
            Assert.AreEqual(x, circle.CenterCoordinates.X);
            Assert.AreEqual(y, circle.Y);
            Assert.AreEqual(y, circle.CenterCoordinates.Y);
            Assert.AreEqual(radius, circle.Radius);
        }

        [Test]
        public void Area_WithZeroRadius_ReturnsZero()
        {
            var circle = new Circle(new Point(5, 5), 0);

            var area = circle.Area;

            Assert.AreEqual(0, area);
        }

        [Test]
        public void Area_With5Radius_GetsArea()
        {
            var circle = new Circle(-20, 17, 5);

            var area = circle.Area;

            Assert.AreEqual(78.539816339744831d, area);
        }
    }
}
