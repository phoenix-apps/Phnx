using System;

namespace Phnx.Try
{
    /// <summary>
    /// Provides extensions for follow-up actions after a <see cref="TryResult"/>
    /// </summary>
    public static class TryResultThenExtensions
    {
        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        public static TryResult Then(this TryResult input, Action execute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (execute is null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            if (!input)
            {
                return input;
            }

            execute();
            return TryResult.Succeed();
        }


        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>, passing the successful result of <paramref name="input"/> to <paramref name="execute"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        public static TryResult<TIn> Then<TIn>(this TryResult<TIn> input, Action<TIn> execute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (execute is null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            if (!input)
            {
                return input;
            }

            execute(input.Result);
            return input;
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        public static TryResult<TOut> Then<TOut>(this TryResult input, Func<TOut> execute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (execute is null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            if (!input)
            {
                return TryResult<TOut>.Fail(input.ErrorMessage);
            }

            return execute();
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>, passing the successful result of <paramref name="input"/> to <paramref name="execute"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        public static TryResult<TOut> Then<TIn, TOut>(this TryResult<TIn> input, Func<TIn, TOut> execute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (execute is null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            if (!input)
            {
                return TryResult<TOut>.Fail(input.ErrorMessage);
            }

            return execute(input.Result);
        }
    }
}
