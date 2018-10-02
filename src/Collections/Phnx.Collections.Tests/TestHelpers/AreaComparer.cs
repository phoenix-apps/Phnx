using System.Collections.Generic;
using System.Drawing;

namespace Phnx.Collections.Tests.TestHelpers
{
    public class AreaComparer : Comparer<Point>
    {
        public override int Compare(Point x, Point y)
        {
            var xArea = x.X * x.Y;
            var yArea = y.X * y.Y;

            if (xArea > yArea)
                return 1;
            else if (xArea < yArea)
                return -1;
            else
                return 0;
        }
    }
}
