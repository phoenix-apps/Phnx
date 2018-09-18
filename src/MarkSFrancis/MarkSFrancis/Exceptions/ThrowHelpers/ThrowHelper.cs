using System;

namespace MarkSFrancis.ThrowHelpers
{
    /// <summary>
    /// Makes it easier to throw custom errors with custom messages, and inner exceptions from the <see cref="ErrorFactory"/>
    /// </summary>
    /// <typeparam name="T">The type of exception to throw</typeparam>
    public class ThrowHelper<T> : IThrowHelper where T : Exception
    {
        private readonly string message;

        /// <summary>
        /// Only use this if the default constructors for <typeparamref name="T"/> match those of <see cref="Exception"/>. Useful for most exception types, but not if you're using implementations of <see cref="Exception"/> that use different constructors (such as <see cref="ArgumentNullException"/>)
        /// </summary>
        /// <param name="message">The message to use for exceptions</param>
        public ThrowHelper(string message)
        {
            this.message = message;
        }

        /// <summary>
        /// Creates the exception
        /// </summary>
        /// <returns>An instance of <typeparamref name="T"/></returns>
        public Exception Create()
        {
            return (T)Activator.CreateInstance(typeof(T), message);
        }

        /// <summary>
        /// Creates the exception with an inner exception
        /// </summary>
        /// <returns>An instance of <typeparamref name="T"/> with the given inner exception</returns>
        public Exception Create(Exception innerException)
        {
            return (T)Activator.CreateInstance(typeof(T), message, innerException);
        }
    }
}
