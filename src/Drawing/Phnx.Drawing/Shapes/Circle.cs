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
        /// <param name="centerPoint">The center point of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        public Circle(Point centerPoint, double radius)
        {
            CenterPoint = centerPoint;
            Radius = radius;
        }

        /// <summary>
        /// The center point of this <see cref="Circle"/>
        /// </summary>
        public Point CenterPoint { get; set; }

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
