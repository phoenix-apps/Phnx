namespace MarkSFrancis.Data
{
    /// <summary>
    /// Extends <see cref="ErrorMessage"/> with new messages related to <see cref="Data"/>
    /// </summary>
    public static class ErrorMessagesExtensions
    {
        /// <summary>
        /// Gets the default message for being unable to set a value
        /// </summary>
        /// <param name="factory">The factory to extend</param>
        /// <returns>The default message for an empty collection</returns>
        public static string CannotSetValue(this ErrorMessage factory)
        {
            return "Cannot set value. Operation not supported";
        }
    }
}
