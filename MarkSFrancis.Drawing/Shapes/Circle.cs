namespace MarkSFrancis.Drawing.Shapes
{
    /// <summary>
    /// A circle shape
    /// </summary>
    public class Circle : Ellipse
    {
        /// <summary>
        /// Create a new circle
        /// </summary>
        /// <param name="centerPoint">The center point of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        public Circle(Point centerPoint, double radius) : base(centerPoint, radius, radius, 0)
        {
        }
    }
}
