using NUnit.Framework;
using Phnx.Drawing.Shapes;
using System;

namespace Phnx.Drawing.Tests.Shapes
{
    public class SideTests
    {
        [Test]
        public void NewSide_WithPoints_CreatesSide()
        {
            var a = new PointD(5, 12);
            var b = new PointD(-2, 1);

            var side = new Side(a, b);

            Assert.AreEqual(a, side.A);
            Assert.AreEqual(b, side.B);
        }

        [Test]
        public void NewSide_WithCoordinates_CreatesSide()
        {
            int aX = 2, aY = 67,
                bX = 3, bY = -2534;

            var side = new Side(aX, aY, bX, bY);

            Assert.AreEqual(side.A.X, aX);
            Assert.AreEqual(side.A.Y, aY);
            Assert.AreEqual(side.B.X, bX);
            Assert.AreEqual(side.B.Y, bY);
        }

        [Test]
        public void GetLength_WhenLengthIsZero_GetsLength()
        {
            var side = new Side();

            var length = side.Length;

            Assert.AreEqual(0, length);
        }

        [Test]
        public void GetLength_WhenPointsAreInOrder_GetsLength()
        {
            var side = new Side(0, 0, 3, 4);

            Assert.AreEqual(5, side.Length);
        }

        [Test]
        public void GetLength_WhenPointsAreReverses_GetsLength()
        {
            var side = new Side(4, 3, 0, 0);

            Assert.AreEqual(5, side.Length);
        }

        [Test]
        public void GetAngleDegrees_WhenAngleIsZero_GetsAngle()
        {
            var side = new Side(0, 0, 0, 5);

            Assert.AreEqual(0, side.AngleDegrees);
        }

        [Test]
        public void GetAngleDegrees_WhenAngleIs45_GetsAngle()
        {
            var side = new Side(0, 0, 5, 5);

            Assert.AreEqual(45, side.AngleDegrees);
        }

        [Test]
        public void GetAngleDegrees_WhenAngleIs180_GetsAngle()
        {
            var side = new Side(0, 0, 0, -5);

            Assert.AreEqual(180, side.AngleDegrees);
        }

        [Test]
        public void GetAngleDegrees_WhenAngleIs315_GetsAngle()
        {
            var side = new Side(0, 0, -5, 5);

            Assert.AreEqual(315, side.AngleDegrees);
        }

        [Test]
        public void GetAngleRadians_WhenAngleIsZero_GetsAngle()
        {
            var side = new Side(0, 0, 0, 5);

            Assert.AreEqual(0, side.AngleRadians);
        }

        [Test]
        public void GetAngleRadians_WhenAngleIsQuarterPi_GetsAngle()
        {
            var side = new Side(0, 0, 5, 5);

            Assert.AreEqual(0.25 * Math.PI, side.AngleRadians);
        }

        [Test]
        public void GetAngleRadians_WhenAngleIsPi_GetsAngle()
        {
            var side = new Side(0, 0, 0, -5);

            Assert.AreEqual(Math.PI, side.AngleRadians);
        }

        [Test]
        public void GetAngleRadians_WhenAngleIsOneAndThreeQuartersPi_GetsAngle()
        {
            var side = new Side(0, 0, -5, 5);

            Assert.AreEqual(1.75 * Math.PI, side.AngleRadians);
        }

        [Test]
        public void SetLength_CalculatesNewCoordinates()
        {
            var side = new Side(0, 0, 3, 4)
            {
                Length = 10
            };

            Assert.AreEqual(0, side.A.X);
            Assert.AreEqual(0, side.A.Y);
            Assert.AreEqual(6, side.B.X);
            Assert.AreEqual(8, side.B.Y);
        }

        [Test]
        public void SetLength_WithNegativeLength_FlipsDirection()
        {
            var side = new Side(0, 0, 3, 4)
            {
                Length = -10
            };

            Assert.AreEqual(0, side.A.X);
            Assert.AreEqual(0, side.A.Y);
            Assert.AreEqual(-6, side.B.X);
            Assert.AreEqual(-8, side.B.Y);
        }

        [Test]
        public void SetAngle_CalculatesNewCoordinates()
        {
            var side = new Side(0, 0, 5, 0)
            {
                AngleDegrees = 180
            };

            Assert.AreEqual(0, side.A.X);
            Assert.AreEqual(0, side.A.Y);
            Assert.AreEqual(-5, side.B.X);
            Assert.AreEqual(0, side.B.Y);
        }

        [Test]
        public void SetAngle_WithAngleOver360_CirclesAngleAround()
        {
            var side = new Side(0, 0, 5, 0)
            {
                AngleDegrees = 450
            };

            Assert.AreEqual(0, side.A.X);
            Assert.AreEqual(0, side.A.Y);
            Assert.AreEqual(0, side.B.X);
            Assert.AreEqual(5, side.B.Y);
        }

        [Test]
        public void SetAngle_WithNegativeAngle_CirclesAngleAround()
        {
            var side = new Side(0, 0, 5, 0)
            {
                AngleDegrees = -90
            };

            Assert.AreEqual(0, side.A.X);
            Assert.AreEqual(0, side.A.Y);
            Assert.AreEqual(0, side.B.X);
            Assert.AreEqual(-5, side.B.Y);
        }
    }
}
