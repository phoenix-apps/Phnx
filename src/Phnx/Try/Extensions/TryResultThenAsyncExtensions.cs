using System;
using System.Threading.Tasks;

namespace Phnx.Try
{
    /// <summary>
    /// Provides extensions for async follow-up actions after a <see cref="TryResult"/>
    /// </summary>
    public static class TryResultThenAsyncExtensions
    {
        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        public static async Task<TryResult> ThenAsync(this TryResult input, Func<Task> execute)
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

            await execute();
            return input;
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>, passing the successful result of <paramref name="input"/> to <paramref name="execute"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        public static async Task<TryResult<TIn>> ThenAsync<TIn>(this TryResult<TIn> input, Func<TIn, Task> execute)
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

            await execute(input.Result);
            return input;
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        public static async Task<TryResult<TOut>> ThenAsync<TOut>(this TryResult input, Func<Task<TOut>> execute)
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
                return input.ErrorMessage;
            }

            var result = await execute();
            return result;
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>, passing the successful result of <paramref name="input"/> to <paramref name="execute"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        public static async Task<TryResult<TOut>> ThenAsync<TIn, TOut>(this TryResult<TIn> input, Func<TIn, Task<TOut>> execute)
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
                return input.ErrorMessage;
            }

            var result = await execute(input.Result);
            return result;
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException"><paramref name="input"/>'s result is <see langword="null"/></exception>
        public static async Task<TryResult> ThenAsync(this Task<TryResult> input, Action execute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (execute is null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            var inputResult = await input;
            if (inputResult is null)
            {
                throw new InvalidOperationException(ErrorMessage.Factory.TryResultIsNull(nameof(input)));
            }

            if (!inputResult)
            {
                return inputResult;
            }

            execute();
            return inputResult;
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>, passing the successful result of <paramref name="input"/> to <paramref name="execute"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException"><paramref name="input"/>'s result is <see langword="null"/></exception>
        public static async Task<TryResult<TIn>> ThenAsync<TIn>(this Task<TryResult<TIn>> input, Action<TIn> execute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (execute is null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            var inputResult = await input;
            if (inputResult is null)
            {
                throw new InvalidOperationException(ErrorMessage.Factory.TryResultIsNull(nameof(input)));
            }

            if (!inputResult)
            {
                return inputResult;
            }

            execute(inputResult.Result);
            return inputResult;
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException"><paramref name="input"/>'s result is <see langword="null"/></exception>
        public static async Task<TryResult<TOut>> ThenAsync<TOut>(this Task<TryResult> input, Func<TOut> execute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (execute is null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            var inputResult = await input;
            if (inputResult is null)
            {
                throw new InvalidOperationException(ErrorMessage.Factory.TryResultIsNull(nameof(input)));
            }

            if (!inputResult)
            {
                return inputResult.ErrorMessage;
            }

            var result = execute();
            return result;
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>, passing the successful result of <paramref name="input"/> to <paramref name="execute"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException"><paramref name="input"/>'s result is <see langword="null"/></exception>
        public static async Task<TryResult<TOut>> ThenAsync<TIn, TOut>(this Task<TryResult<TIn>> input, Func<TIn, TOut> execute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (execute is null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            var inputResult = await input;
            if (inputResult is null)
            {
                throw new InvalidOperationException(ErrorMessage.Factory.TryResultIsNull(nameof(input)));
            }

            if (!inputResult)
            {
                return inputResult.ErrorMessage;
            }

            var result = execute(inputResult.Result);
            return result;
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException"><paramref name="input"/>'s result is <see langword="null"/></exception>
        public static async Task<TryResult> ThenAsync(this Task<TryResult> input, Func<Task> execute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (execute is null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            var inputResult = await input;
            if (inputResult is null)
            {
                throw new InvalidOperationException(ErrorMessage.Factory.TryResultIsNull(nameof(input)));
            }

            if (!inputResult)
            {
                return inputResult;
            }

            await execute();
            return inputResult;
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>, passing the successful result of <paramref name="input"/> to <paramref name="execute"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException"><paramref name="input"/>'s result is <see langword="null"/></exception>
        public static async Task<TryResult<TIn>> ThenAsync<TIn>(this Task<TryResult<TIn>> input, Func<TIn, Task> execute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (execute is null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            var inputResult = await input;
            if (inputResult is null)
            {
                throw new InvalidOperationException(ErrorMessage.Factory.TryResultIsNull(nameof(input)));
            }

            if (!inputResult)
            {
                return inputResult;
            }

            await execute(inputResult.Result);
            return inputResult;
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException"><paramref name="input"/>'s result is <see langword="null"/></exception>
        public static async Task<TryResult<TOut>> ThenAsync<TOut>(this Task<TryResult> input, Func<Task<TOut>> execute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (execute is null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            var inputResult = await input;
            if (inputResult is null)
            {
                throw new InvalidOperationException(ErrorMessage.Factory.TryResultIsNull(nameof(input)));
            }

            if (!inputResult)
            {
                return inputResult.ErrorMessage;
            }

            var result = await execute();
            return result;
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>, passing the successful result of <paramref name="input"/> to <paramref name="execute"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException"><paramref name="input"/>'s result is <see langword="null"/></exception>
        public static async Task<TryResult<TOut>> ThenAsync<TIn, TOut>(this Task<TryResult<TIn>> input, Func<TIn, Task<TOut>> execute)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (execute is null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            var inputResult = await input;
            if (inputResult is null)
            {
                throw new InvalidOperationException(ErrorMessage.Factory.TryResultIsNull(nameof(input)));
            }

            if (!inputResult)
            {
                return TryResult<TOut>.Fail(inputResult.ErrorMessage);
            }

            var result = await execute(inputResult.Result);
            return result;
        }
    }
}
