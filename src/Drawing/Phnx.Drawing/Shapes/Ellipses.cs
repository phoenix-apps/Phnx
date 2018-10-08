using System;
using System.Drawing;

namespace Phnx.Drawing.Shapes
{
    /// <summary>
    /// An ellipse shape
    /// </summary>
    public class Ellipse : IShape
    {
        /// <summary>
        /// Create a new ellipse
        /// </summary>
        /// <param name="centerPoint">The center of the ellipse</param>
        /// <param name="minorRadiusSize">The smallest radius within the ellipse</param>
        /// <param name="majorRadiusSize">The largest radius within the ellipse</param>
        /// <param name="minorRadiusAngle">The angle at which the smallest radius occurs in degrees</param>
        public Ellipse(Point centerPoint, double minorRadiusSize, double majorRadiusSize, double minorRadiusAngle)
        {
            CenterCoordinates = centerPoint;

            if (minorRadiusSize < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(minorRadiusSize));
            }

            MinorRadiusSize = minorRadiusSize;

            if (majorRadiusSize < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(majorRadiusSize));
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
        /// Create a new ellipse
        /// </summary>
        /// <param name="x">The X coordinate of the center of the ellipse</param>
        /// <param name="y">The Y coordinate of the center of the ellipse</param>
        /// <param name="minorRadiusSize">The smallest radius within the ellipse</param>
        /// <param name="majorRadiusSize">The largest radius within the ellipse</param>
        /// <param name="minorRadiusAngle">The angle at which the smallest radius occurs in degrees</param>
        public Ellipse(int x, int y, double minorRadiusSize, double majorRadiusSize, double minorRadiusAngle)
            : this(new Point(x, y), minorRadiusSize, majorRadiusSize, minorRadiusAngle)
        {
        }

        /// <summary>
        /// The coordinates of the center
        /// </summary>
        public Point CenterCoordinates
        {
            get => new Point(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// The X coordinate of the center
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y coordinate of the center
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// The smaller radius
        /// </summary>
        public double MinorRadiusSize { get; set; }

        /// <summary>
        /// The larger radius
        /// </summary>
        public double MajorRadiusSize { get; set; }

        /// <summary>
        /// The angle at which the smallest radius occurs in degrees
        /// </summary>
        public double MinorRadiusAngle { get; set; }

        /// <summary>
        /// The total area
        /// </summary>
        public double Area => MinorRadiusSize * MajorRadiusSize * Math.PI;
    }
}
