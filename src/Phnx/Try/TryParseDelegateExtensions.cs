using System;

namespace Phnx.Try
{
    /// <summary>
    /// Provides helpers for typical try parse methods in C#, such as <see cref="int.TryParse(string?, out int)"/>
    /// </summary>
    public static class TryParseDelegateExtensions
    {
        /// <summary>
        /// A delegate for typical try parse methods in C#, such as <see cref="int.TryParse(string?, out int)"/>
        /// </summary>
        /// <typeparam name="TIn">The input type to the try parse</typeparam>
        /// <typeparam name="TOut">The result type of the try parse</typeparam>
        /// <param name="input">The value to try to parse</param>
        /// <param name="value">The result of the parse</param>
        /// <returns>Whether the parse was successful</returns>
        public delegate bool TryParse<TIn, TOut>(TIn input, out TOut value);

        /// <summary>
        /// Converts a typical try parse method delegate to an executable <see cref="TryResult{TOut}"/>
        /// </summary>
        /// <typeparam name="TIn">The input type to the try parse</typeparam>
        /// <typeparam name="TOut">The result type of the try parse</typeparam>
        /// <param name="tryParse">The delegate to convert</param>
        /// <param name="errorMessage">The error message to show if the try parse returns <see langword="false"/></param>
        /// <returns>An executable <see cref="TryResult{T}"/> that converts from <typeparamref name="TIn"/> to <typeparamref name="TOut"/></returns>
        public static Func<TIn, TryResult<TOut>> ToTryResult<TIn, TOut>(this TryParse<TIn, TOut> tryParse, string errorMessage)
        {
            return s =>
            {
                if (!tryParse(s, out TOut result))
                {
                    return errorMessage;
                }

                return result;
            };
        }
    }
}
