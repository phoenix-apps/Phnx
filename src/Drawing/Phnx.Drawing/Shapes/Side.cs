using System;

namespace Phnx.Drawing.Shapes
{
    /// <summary>
    /// Describes a 2D shape's side
    /// </summary>
    public struct Side
    {
        /// <summary>
        /// Create a new side with a specified start coordinate and end coordinate
        /// </summary>
        /// <param name="pointA">The first point of the side</param>
        /// <param name="pointB">The second point of the side</param>
        public Side(PointD pointA, PointD pointB)
        {
            A = pointA;
            B = pointB;
        }

        /// <summary>
        /// Create a new side with a specified start coordinate and end coordinate
        /// </summary>
        /// <param name="aX">The X coordinate of the first point of the side</param>
        /// <param name="aY">The Y coordinate of the first point of the side</param>
        /// <param name="bX">The X coordinate of the second point of the side</param>
        /// <param name="bY">The Y coordinate of the second point of the side</param>
        public Side(double aX, double aY, double bX, double bY) : this(new PointD(aX, aY), new PointD(bX, bY))
        {
        }

        /// <summary>
        /// The length of the side
        /// </summary>
        public double Length
        {
            get
            {
                // Use pythagoras to calculate side length
                var width = Math.Abs(A.X - B.X);
                var height = Math.Abs(A.Y - B.Y);

                return Math.Sqrt(width * width + height * height);
            }
            set
            {
                var angle = AngleRadians;

                var cos = Math.Cos(angle);
                var newYLength = cos * value;

                var sin = Math.Sin(angle);
                var newXLength = sin * value;

                B = new PointD(A.X + newXLength, A.Y + newYLength);
            }
        }

        /// <summary>
        /// The angle of the line (in degrees)
        /// </summary>
        public double AngleDegrees
        {
            get
            {
                return AngleRadians.ToDegrees();
            }
            set
            {
                AngleRadians = value.ToRadians();
            }
        }

        /// <summary>
        /// The angle of the line (in radians)
        /// </summary>
        public double AngleRadians
        {
            get
            {
                var tan = Math.Atan2(B.X - A.X, B.Y - A.Y);

                if (tan < 0)
                {
                    tan += 2 * Math.PI;
                }

                return tan;
            }
            set
            {
                var length = Length;

                var cos = Math.Cos(value);
                var newXLength = Math.Round(cos * length, 14);

                var sin = Math.Sin(value);
                var newYLength = Math.Round(sin * length, 14);

                B = new PointD(A.X + newXLength, A.Y + newYLength);
            }
        }

        /// <summary>
        /// The first point of the side
        /// </summary>
        public PointD A { get; set; }

        /// <summary>
        /// The second point of the side
        /// </summary>
        public PointD B { get; set; }
    }
}
