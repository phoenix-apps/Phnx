namespace MarkSFrancis.Geometry.Shapes
{
    public class Circle : Ellipse
    {
        public Circle(Point centerPoint, double radius) : base(centerPoint, radius, radius, 0)
        {
        }
    }
}
