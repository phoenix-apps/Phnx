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
            Vertices = points;
        }

        /// <summary>
        /// The area of this <see cref="Polygon"/>
        /// </summary>
        public double Area
        {
            get
            {
                int clockwiseSum = 0, antiClockwiseSum = 0;

                for (int index = 0; index < Vertices.Length - 1; ++index)
                {
                    clockwiseSum += Vertices[index].X * Vertices[index + 1].Y;
                    antiClockwiseSum += Vertices[index + 1].X * Vertices[index].Y;
                }

                clockwiseSum += Vertices[Vertices.Length - 1].X * Vertices[0].Y;
                antiClockwiseSum += Vertices[0].X * Vertices[Vertices.Length - 1].Y;

                var totalArea = Math.Abs(clockwiseSum - antiClockwiseSum) / 2d;

                return totalArea;
            }
        }

        /// <summary>
        /// The total number of sides
        /// </summary>
        public int SidesCount => Vertices.Length < 2 ? 0 : Vertices.Length - 1;

        /// <summary>
        /// All sides of this shape, as described by <see cref="Vertices"/>
        /// </summary>
        public Side[] Sides
        {
            get
            {
                if (Vertices.Length == 0)
                {
                    return new Side[0];
                }

                Side[] sides = new Side[Vertices.Length - 1];

                for (int vertexIndex = 0; vertexIndex < Vertices.Length - 1; vertexIndex++)
                {
                    sides[vertexIndex] = new Side(Vertices[vertexIndex], Vertices[vertexIndex + 1]);
                }

                return sides;
            }
        }

        /// <summary>
        /// All vertices (corners)
        /// </summary>
        public Point[] Vertices { get; set; }
    }
}
