namespace MarkSFrancis.Drawing.Shapes
{
    public class Quadrilateral : Polygon
    {
        public Quadrilateral(Point point1, Point point2, Point point3, Point point4) : base(new[] { point1, point2, point3, point4 })
        {
        }
    }
}
