namespace MarkSFrancis.Drawing
{
    /// <summary>
    /// A 2D coordinate
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// Create a new <see cref="Point"/> from an X and Y coordinate
        /// </summary>
        /// <param name="x">The X coordinate</param>
        /// <param name="y">The Y coordinate</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// The X coordinate of this point
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y coordinate of this point
        /// </summary>
        public int Y { get; set; }
    }
}
