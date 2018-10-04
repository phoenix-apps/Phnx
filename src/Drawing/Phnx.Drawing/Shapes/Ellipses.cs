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
            CenterPoint = centerPoint;
            MinorRadiusSize = minorRadiusSize;
            MajorRadiusSize = majorRadiusSize;
            MinorRadiusAngle = minorRadiusAngle;
        }

        /// <summary>
        /// The center point of this <see cref="Ellipse"/>
        /// </summary>
        public Point CenterPoint { get; set; }

        /// <summary>
        /// The smallest radius within this <see cref="Ellipse"/>
        /// </summary>
        public double MinorRadiusSize { get; set; }

        /// <summary>
        /// The largest radius within this <see cref="Ellipse"/>
        /// </summary>
        public double MajorRadiusSize { get; set; }

        /// <summary>
        /// The angle at which the smallest radius occurs in degrees
        /// </summary>
        public double MinorRadiusAngle { get; set; }

        /// <summary>
        /// The area of this <see cref="Ellipse"/>
        /// </summary>
        public double Area => MinorRadiusSize * MajorRadiusSize * Math.PI;
    }
}
