namespace MarkSFrancis.Drawing.Shapes
{
    public class Square : Rectangle
    {
        public Square(Point topLeft, int width) : this(topLeft.X, topLeft.Y, width)
        {
        }

        public Square(int topLeftX, int topLeftY, int width) : base(topLeftX, topLeftY, width, width)
        {
        }
    }
}
