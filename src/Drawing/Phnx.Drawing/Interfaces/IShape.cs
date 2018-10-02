namespace Phnx.Drawing.Interfaces
{
    /// <summary>
    /// Provides common properties and methods for two-dimensional shapes
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// The area of this shape
        /// </summary>
        double Area { get; }
    }
}
