using Phnx.Try;

namespace Phnx
{
    /// <summary>
    /// Extensions for <see cref="ErrorMessage"/>
    /// </summary>
    public static class TryResultErrorMessageExtensions
    {
        /// <summary>
        /// Generates an error when a <see cref="TryResult"/> was <see langword="null"/>
        /// </summary>
        /// <param name="_">The error message to extend</param>
        /// <param name="argName">The name of the argument that contained the function that was executed, which produced a <see langword="null"/> result</param>
        public static string TryResultIsNull(this ErrorMessage _, string argName)
        {
            return $"The result of {argName} cannot be null";
        }
    }
}
