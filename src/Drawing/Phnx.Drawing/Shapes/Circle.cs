using System;
using System.Drawing;

namespace Phnx.Drawing.Shapes
{
    /// <summary>
    /// A circle shape
    /// </summary>
    public class Circle : IShape
    {
        /// <summary>
        /// Create a new circle
        /// </summary>
        /// <param name="centerPoint">The center coordinates of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        public Circle(Point centerPoint, double radius)
        {
            CenterCoordinates = centerPoint;

            if (radius < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(radius));
            }

            Radius = radius;
        }

        /// <summary>
        /// Create a new circle
        /// </summary>
        /// <param name="x">The x coordinate of the center of the circle</param>
        /// <param name="y">The y coordinate of the center of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        public Circle(int x, int y, double radius) : this(new Point(x, y), radius)
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
        /// The radius of this <see cref="Circle"/>
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// The area of this <see cref="Circle"/>
        /// </summary>
        public double Area => Radius * Radius * Math.PI;
    }
}
