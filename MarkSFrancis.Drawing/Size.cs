namespace MarkSFrancis.Drawing
{
    /// <summary>
    /// A 2D size
    /// </summary>
    public struct Size
    {
        /// <summary>
        /// Create a new size from a width and height
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// The width of this size
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of this size
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The total area of this size (<see cref="Width"/> * <see cref="Height"/>)
        /// </summary>
        public int Area => Width * Height;
    }
}
