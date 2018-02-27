namespace MarkSFrancis.Drawing.Shapes
{
    /// <summary>
    /// A square <see cref="Rectangle"/>
    /// </summary>
    public class Square : Rectangle
    {
        /// <summary>
        /// Create a new square from a top left coordinate and a width
        /// </summary>
        /// <param name="topLeft">The top left coordinate of the square</param>
        /// <param name="width">The width/ height of the square</param>
        public Square(Point topLeft, int width) : this(topLeft.X, topLeft.Y, width)
        {
        }
        
        /// <summary>
        /// Create a new square from a top left coordinate and a width
        /// </summary>
        /// <param name="topLeftX">The top left X coordinate of the square</param>
        /// <param name="topLeftY">The top left Y coordinate of the square</param>
        /// <param name="width">The width/ height of the square</param>
        public Square(int topLeftX, int topLeftY, int width) : base(topLeftX, topLeftY, width, width)
        {
        }
    }
}
