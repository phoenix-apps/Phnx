namespace Phnx
{
    /// <summary>
    /// Extends <see cref="ErrorMessage"/> with new messages related to <see cref="Collections"/>
    /// </summary>
    public static class CollectionsErrorMessageExtensions
    {
        /// <summary>
        /// Gets the default message for an empty collection
        /// </summary>
        /// <param name="_">The factory to extend</param>
        /// <returns>The default message for an empty collection</returns>
        public static string SequenceEmpty(this ErrorMessage _)
        {
            return "Sequence contains no elements";
        }

        /// <summary>
        /// Gets the default message for an empty collection, with the name of the collection included in the message
        /// </summary>
        /// <param name="_">The factory to extend</param>
        /// <param name="sequenceName">The name of the empty collection</param>
        /// <returns>The default message for an empty collection</returns>
        public static string SequenceEmpty(this ErrorMessage _, string sequenceName)
        {
            return $"{sequenceName} contains no elements";
        }
    }
}
