using System;

namespace Phnx.Try
{
    /// <summary>
    /// Provides extensions for follow-up <see cref="TryResult"/> actions after a <see cref="TryResult"/>
    /// </summary>
    public static class TryResultThenTryExtensions
    {
        /// <summary>
        /// Execute <paramref name="tryExecute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="tryExecute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="tryExecute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="tryExecute"/> is <see langword="null"/></exception>
        public static TryResult ThenTry(this TryResult input, Func<TryResult> tryExecute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (tryExecute is null)
            {
                throw new ArgumentNullException(nameof(tryExecute));
            }

            if (!input)
            {
                return input;
            }

            return tryExecute();
        }

        /// <summary>
        /// Execute <paramref name="tryExecute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="tryExecute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="tryExecute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="tryExecute"/> is <see langword="null"/></exception>
        public static TryResult<TOut> ThenTry<TOut>(this TryResult input, Func<TryResult<TOut>> tryExecute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (tryExecute is null)
            {
                throw new ArgumentNullException(nameof(tryExecute));
            }

            if (!input)
            {
                return TryResult<TOut>.Fail(input.ErrorMessage);
            }

            return tryExecute();
        }

        /// <summary>
        /// Execute <paramref name="tryExecute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>, passing the successful result of <paramref name="input"/> to <paramref name="tryExecute"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="tryExecute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="tryExecute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="tryExecute"/> is <see langword="null"/></exception>
        public static TryResult<TIn> ThenTry<TIn>(this TryResult<TIn> input, Func<TIn, TryResult> tryExecute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (tryExecute is null)
            {
                throw new ArgumentNullException(nameof(tryExecute));
            }

            if (!input)
            {
                return input;
            }

            var execResult = tryExecute(input.Result);

            if (execResult is null)
            {
                throw new InvalidOperationException(ErrorMessage.Factory.TryResultIsNull(nameof(tryExecute)));
            }
            if (!execResult)
            {
                return execResult.ErrorMessage;
            }

            return input;
        }

        /// <summary>
        /// Execute <paramref name="tryExecute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>, passing the successful result of <paramref name="input"/> to <paramref name="tryExecute"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="tryExecute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="tryExecute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="tryExecute"/> is <see langword="null"/></exception>
        public static TryResult<TOut> ThenTry<TIn, TOut>(this TryResult<TIn> input, Func<TIn, TryResult<TOut>> tryExecute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (tryExecute is null)
            {
                throw new ArgumentNullException(nameof(tryExecute));
            }

            if (!input)
            {
                return TryResult<TOut>.Fail(input.ErrorMessage);
            }

            return tryExecute(input.Result);
        }
    }
}
