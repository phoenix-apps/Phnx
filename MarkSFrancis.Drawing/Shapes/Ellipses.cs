using System;
using MarkSFrancis.Geometry.Interfaces;

namespace MarkSFrancis.Geometry.Shapes
{
    public class Ellipse : IShape
    {
        public Ellipse(Point centerPoint, double minorRadiusSize, double majorRadiusSize, double minorRadiusAngle)
        {
            CenterPoint = centerPoint;
            MinorRadiusSize = minorRadiusSize;
            MajorRadiusSize = majorRadiusSize;
            MinorRadiusAngle = minorRadiusAngle;
        }

        public Point CenterPoint { get; set; }

        public double MinorRadiusSize { get; set; }

        public double MajorRadiusSize { get; set; }

        public double MinorRadiusAngle { get; set; }

        public double Area => MinorRadiusSize * MajorRadiusSize * Math.PI;
    }
}
