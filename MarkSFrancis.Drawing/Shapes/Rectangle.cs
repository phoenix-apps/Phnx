namespace MarkSFrancis.Geometry.Shapes
{
    public class Rectangle : Polygon
    {
        public Rectangle(Point topLeft, int width, int height) : this(topLeft.X, topLeft.Y, width, height)
        {
        }

        public Rectangle(int topLeftX, int topLeftY, int width, int height) : base(new[]
        {
            new Point(topLeftX, topLeftY),
            new Point(topLeftX, height),
            new Point(width, height),
            new Point(width, topLeftY)
        })
        {
        }
    }
}
