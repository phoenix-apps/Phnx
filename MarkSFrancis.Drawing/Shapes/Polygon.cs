using System;

namespace MarkSFrancis.Drawing.Shapes
{
    public class Polygon
    {
        public Polygon(Point[] points)
        {
            Points = points;
        }

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

        public int SidesCount => Points.Length;

        public Point[] Points { get; private set; }
    }
}
