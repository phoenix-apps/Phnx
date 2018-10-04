using System;
using System.Drawing;

namespace Phnx.Drawing.Shapes
{
    /// <summary>
    /// A shape with an edge defined by a series of straight edges
    /// </summary>
    public class Polygon : IShape
    {
        /// <summary>
        /// Create a new <see cref="Polygon"/> with a range of corners as <see cref="Point"/>
        /// </summary>
        /// <param name="points">The corners</param>
        public Polygon(Point[] points)
        {
            Points = points;
        }

        /// <summary>
        /// The area of this <see cref="Polygon"/>
        /// </summary>
        public double Area
        {
            get
            {
                int clockwiseSum = 0, antiClockwiseSum = 0;

                for (int index = 0; index < Points.Length - 1; ++index)
                {
                    clockwiseSum += Points[index].X * Points[index + 1].Y;
                    antiClockwiseSum += Points[index + 1].X * Points[index].Y;
                }

                clockwiseSum += Points[Points.Length - 1].X * Points[0].Y;
                antiClockwiseSum += Points[0].X * Points[Points.Length - 1].Y;

                var totalArea = Math.Abs(clockwiseSum - antiClockwiseSum) / 2d;

                return totalArea;
            }
        }

        /// <summary>
        /// The number of sides in this <see cref="Polygon"/>
        /// </summary>
        public int SidesCount => Points.Length;

        /// <summary>
        /// The corners in this <see cref="Polygon"/>
        /// </summary>
        public Point[] Points { get; }
    }
}
