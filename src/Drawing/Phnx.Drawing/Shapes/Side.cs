using System;
using System.Drawing;

namespace Phnx.Drawing.Shapes
{
    /// <summary>
    /// Describes a 2D shape's side
    /// </summary>
    public class Side
    {
        /// <summary>
        /// Create a new side with a specified start coordinate, length and end coordinate
        /// </summary>
        /// <param name="pointA">The first point of the side</param>
        /// <param name="pointB">The second point of the side</param>
        public Side(Point pointA, Point pointB)
        {
            PointA = pointA;
            PointB = pointB;
        }

        /// <summary>
        /// The length of the side
        /// </summary>
        public double Length
        {
            get
            {
                // Use pythagoras to calculate side length
                var width = Math.Abs(PointA.X - PointB.X);
                var height = Math.Abs(PointB.Y - PointB.Y);

                return Math.Sqrt(width * 2 + height * 2);
            }
        }

        /// <summary>
        /// The first point of the side
        /// </summary>
        public Point PointA { get; set; }

        /// <summary>
        /// The second point of the side
        /// </summary>
        public Point PointB { get; set; }
    }
}
