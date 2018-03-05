namespace MarkSFrancis.Drawing.Shapes
{
    /// <summary>
    /// A triangular <see cref="Polygon"/>
    /// </summary>
    public class Triangle : Polygon
    {
        /// <summary>
        /// Create a new triangle from 3 corners
        /// </summary>
        /// <param name="corner1">The first corner</param>
        /// <param name="corner2">The second corner</param>
        /// <param name="corner3">The third corner</param>
        public Triangle(Point corner1, Point corner2, Point corner3) : base(new []{corner1, corner2, corner3})
        {
        }
    }
}
