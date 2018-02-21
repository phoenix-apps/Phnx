namespace MarkSFrancis
{
    /// <summary>
    /// An exception factory which contains a series of common errors and messages. To extend this object, use extension methods instead of inheriting
    /// </summary>
    public sealed class ErrorFactory
    {
        /// <summary>
        /// This <see cref="ErrorFactory"/> available as a static object for use across the rest of the application
        /// </summary>
        public static readonly ErrorFactory Default = new ErrorFactory();
    }
}
