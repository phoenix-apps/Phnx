namespace MarkSFrancis.Drawing.Shapes
{
    /// <summary>
    /// A four-sided polygon
    /// </summary>
    public class Quadrilateral : Polygon
    {
        /// <summary>
        /// Create a new quarilateral from 4 corners
        /// </summary>
        /// <param name="corner1">The first corner</param>
        /// <param name="corner2">The second corner</param>
        /// <param name="corner3">The third corner</param>
        /// <param name="corner4">The fourth corner</param>
        public Quadrilateral(Point corner1, Point corner2, Point corner3, Point corner4) : base(new[] { corner1, corner2, corner3, corner4 })
        {
        }
    }
}
