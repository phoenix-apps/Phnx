namespace MarkSFrancis
{
    /// <summary>
    /// An exception factory which contains a series of common errors and messages. To extend this object, use extension methods instead of inheriting
    /// </summary>
    /// <example>
    /// To use an error message from the <see cref="ErrorMessage"/>
    /// <code>
    /// void MustNotBeNegative(int argument)
    /// {
    ///     if (argument &lt; 0)
    ///     {
    ///         string errorMessage = ErrorMessages.Factory.ArgumentLessThanZero();
    ///         throw new ArgumentOutOfRangeException(nameof(argument), errorMessage);
    ///     }
    /// }
    /// </code>
    /// 
    /// To add more errors messages to the factory, and improve it within your own applications with additional standardised error messages, use extension methods for <see cref="ErrorMessage"/>.
    /// <code>
    /// public static class ErrorMessagesExtensions
    /// {
    ///     public static string MyCustomError(this ErrorMessages messages)
    ///     {
    ///         return "This is a custom error message";
    ///     }
    /// }
    /// </code>
    /// </example>
    public sealed class ErrorMessage
    {
        /// <summary>
        /// Create a new instance of <see cref="ErrorMessage"/>. An instance is also available through <see cref="Factory"/>
        /// </summary>
        public ErrorMessage()
        {
        }

        /// <summary>
        /// The default singleton instance of <see cref="ErrorMessage"/>
        /// </summary>
        public static ErrorMessage Factory { get; } = new ErrorMessage();
    }
}
