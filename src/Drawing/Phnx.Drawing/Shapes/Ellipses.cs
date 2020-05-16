using System;

namespace Phnx.Drawing.Shapes
{
    /// <summary>
    /// An ellipse shape
    /// </summary>
    public struct Ellipse : IShape
    {
        /// <summary>
        /// Create a new ellipse
        /// </summary>
        /// <param name="centerPoint">The center of the ellipse</param>
        /// <param name="minorRadiusSize">The smallest radius within the ellipse</param>
        /// <param name="majorRadiusSize">The largest radius within the ellipse</param>
        /// <param name="minorRadiusAngle">The angle at which the smallest radius occurs in degrees</param>
        public Ellipse(PointD centerPoint, double minorRadiusSize, double majorRadiusSize, double minorRadiusAngle)
            : this(centerPoint.X, centerPoint.Y, minorRadiusSize, majorRadiusSize, minorRadiusAngle)
        {
        }

        /// <summary>
        /// Create a new ellipse
        /// </summary>
        /// <param name="x">The X coordinate of the center of the ellipse</param>
        /// <param name="y">The Y coordinate of the center of the ellipse</param>
        /// <param name="minorRadiusSize">The smallest radius within the ellipse</param>
        /// <param name="majorRadiusSize">The largest radius within the ellipse</param>
        /// <param name="minorRadiusAngle">The angle at which the smallest radius occurs in degrees</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minorRadiusAngle"/> or <paramref name="majorRadiusSize"/> is less than zero</exception>
        /// <exception cref="ArgumentException"><paramref name="majorRadiusSize"/> is less than <paramref name="minorRadiusSize"/></exception>
        public Ellipse(double x, double y, double minorRadiusSize, double majorRadiusSize, double minorRadiusAngle)
        {
            X = x;
            Y = y;

            if (minorRadiusSize < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minorRadiusSize));
            }

            MinorRadiusSize = minorRadiusSize;

            if (majorRadiusSize < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(majorRadiusSize));
            }

            if (majorRadiusSize < minorRadiusSize)
            {
                throw new ArgumentException($"{nameof(majorRadiusSize)} cannot be less than {nameof(minorRadiusSize)}");
            }

            MajorRadiusSize = majorRadiusSize;

            if (minorRadiusAngle < 0)
            {
                minorRadiusAngle = 360 - Math.Abs(minorRadiusAngle);
            }

            MinorRadiusAngle = minorRadiusAngle % 360;
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
        /// The smallest radius
        /// </summary>
        public double MinorRadiusSize { get; set; }

        /// <summary>
        /// The largest radius
        /// </summary>
        public double MajorRadiusSize { get; set; }

        /// <summary>
        /// The angle at which the smallest radius occurs
        /// </summary>
        public double MinorRadiusAngle { get; set; }

        /// <summary>
        /// The total area
        /// </summary>
        public double Area => MinorRadiusSize * MajorRadiusSize * Math.PI;
    }
}
