using System;

namespace Phnx.Drawing.Shapes
{
    /// <summary>
    /// A circle shape
    /// </summary>
    public struct Circle : IShape
    {
        /// <summary>
        /// Create a new circle
        /// </summary>
        /// <param name="centerPoint">The center coordinates of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        public Circle(PointD centerPoint, double radius)
            : this(centerPoint.X, centerPoint.Y, radius)
        {
        }

        /// <summary>
        /// Create a new circle
        /// </summary>
        /// <param name="x">The x coordinate of the center of the circle</param>
        /// <param name="y">The y coordinate of the center of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="radius"/> is less than zero</exception>
        public Circle(double x, double y, double radius)
        {
            X = x;
            Y = y;

            if (radius < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(radius));
            }

            Radius = radius;
        }

        /// <summary>
        /// The coordinates of the center
        /// </summary>
        public PointD CenterCoordinates
        {
            get => new PointD(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// The X coordinate of the center
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The Y coordinate of the center
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// The radius of this <see cref="Circle"/>
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// The area of this <see cref="Circle"/>
        /// </summary>
        public double Area => Radius * Radius * Math.PI;
    }
}
