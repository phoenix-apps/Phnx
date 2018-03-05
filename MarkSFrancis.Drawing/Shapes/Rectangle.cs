namespace MarkSFrancis.Drawing.Shapes
{
    /// <summary>
    /// A rectangular <see cref="Quadrilateral"/>
    /// </summary>
    public class Rectangle : Quadrilateral
    {
        /// <summary>
        /// Create a new rectangle from a top left coordinate, a width and a height
        /// </summary>
        /// <param name="topLeft">The top left coordinate of the rectangle</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        public Rectangle(Point topLeft, int width, int height) : this(topLeft.X, topLeft.Y, width, height)
        {
        }
        
        /// <summary>
        /// Create a new rectangle from a top left coordinate and a size
        /// </summary>
        /// <param name="topLeft">The top left coordinate of the rectangle</param>
        /// <param name="size">The size of the rectangle</param>
        public Rectangle(Point topLeft, Size size) : this(topLeft.X, topLeft.Y, size.Width, size.Height)
        {
        }
        
        /// <summary>
        /// Create a new rectangle from a top left coordinate and a size
        /// </summary>
        /// <param name="topLeftX">The top left X coordinate of the rectangle</param>
        /// <param name="topLeftY">The top left Y coordinate of the rectangle</param>
        /// <param name="size">The size of the rectangle</param>
        public Rectangle(int topLeftX, int topLeftY, Size size) : this(topLeftX, topLeftY, size.Width, size.Height)
        {
        }
        
        /// <summary>
        /// Create a new rectangle from a top left coordinate, a width and a height
        /// </summary>
        /// <param name="topLeftX">The top left X coordinate of the rectangle</param>
        /// <param name="topLeftY">The top left Y coordinate of the rectangle</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        public Rectangle(int topLeftX, int topLeftY, int width, int height) : base(
            new Point(topLeftX, topLeftY),
            new Point(topLeftX, height),
            new Point(width, height),
            new Point(width, topLeftY)
        )
        {
        }

        /// <summary>
        /// Create a new rectangle from the top left and bottom right coordinates
        /// </summary>
        /// <param name="topLeft">The top left coordinate</param>
        /// <param name="bottomRight">The bottom right coordinate</param>
        public Rectangle(Point topLeft, Point bottomRight) : base(topLeft, new Point(bottomRight.X, topLeft.Y), bottomRight, new Point(topLeft.X, bottomRight.Y))
        {
        }
    }
}
