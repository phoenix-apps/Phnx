using System;
using System.Threading.Tasks;

namespace Phnx.Try
{
    /// <summary>
    /// Provides extensions for async follow-up <see cref="TryResult"/> actions after a <see cref="TryResult"/>
    /// </summary>
    public static class TryResultThenTryAsyncExtensions
    {
        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        public static Task<TryResult> ThenTryAsync(this TryResult input, Func<Task<TryResult>> execute)
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
                return Task.FromResult(input);
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
        /// <exception cref="InvalidOperationException"><paramref name="execute"/>'s result is <see langword="null"/></exception>
        public static async Task<TryResult<TIn>> ThenTryAsync<TIn>(this TryResult<TIn> input, Func<TIn, Task<TryResult>> execute)
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

            var execResult = await execute(input.Result);

            if (execResult is null)
            {
                throw new InvalidOperationException(ErrorMessage.Factory.TryResultIsNull(nameof(execute)));
            }
            if (!execResult)
            {
                return execResult.ErrorMessage;
            }

            return input;
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        public static Task<TryResult<TOut>> ThenTryAsync<TOut>(this TryResult input, Func<Task<TryResult<TOut>>> execute)
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
                return Task.FromResult<TryResult<TOut>>(input.ErrorMessage);
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
        public static Task<TryResult<TOut>> ThenTryAsync<TIn, TOut>(this TryResult<TIn> input, Func<TIn, Task<TryResult<TOut>>> execute)
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
                return Task.FromResult<TryResult<TOut>>(input.ErrorMessage);
            }

            return execute(input.Result);
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException"><paramref name="input"/>'s result is <see langword="null"/></exception>
        public static async Task<TryResult> ThenTryAsync(this Task<TryResult> input, Func<TryResult> execute)
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

            return execute();
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>, passing the successful result of <paramref name="input"/> to <paramref name="execute"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException"><paramref name="input"/>'s result is <see langword="null"/></exception>
        public static async Task<TryResult<TIn>> ThenTryAsync<TIn>(this Task<TryResult<TIn>> input, Func<TIn, TryResult> execute)
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

            var execResult = execute(inputResult.Result);
            if (execResult is null)
            {
                throw new InvalidOperationException(ErrorMessage.Factory.TryResultIsNull(nameof(execute)));
            }

            return execResult ? inputResult : execResult.ErrorMessage;
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException"><paramref name="input"/>'s result is <see langword="null"/></exception>
        public static async Task<TryResult<TOut>> ThenTryAsync<TOut>(this Task<TryResult> input, Func<TryResult<TOut>> execute)
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
        public static async Task<TryResult<TOut>> ThenTryAsync<TIn, TOut>(this Task<TryResult<TIn>> input, Func<TIn, TryResult<TOut>> execute)
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
        public static async Task<TryResult> ThenTryAsync(this Task<TryResult> input, Func<Task<TryResult>> execute)
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

            return await execute();
        }

        /// <summary>
        /// Execute <paramref name="execute"/> if <paramref name="input"/>'s <see cref="TryResult.Success"/> is <see langword="true"/>, passing the successful result of <paramref name="input"/> to <paramref name="execute"/>
        /// </summary>
        /// <param name="input">The result of the previous tried task</param>
        /// <param name="execute">The next action to execute</param>
        /// <returns><paramref name="input"/> if it was not successful, otherwise, the result of <paramref name="execute"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/> or <paramref name="execute"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException"><paramref name="input"/> or <paramref name="execute"/>'s result is <see langword="null"/></exception>
        public static async Task<TryResult<TIn>> ThenTryAsync<TIn>(this Task<TryResult<TIn>> input, Func<TIn, Task<TryResult>> execute)
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

            var execResult = await execute(inputResult.Result);
            if (execResult is null)
            {
                throw new InvalidOperationException(ErrorMessage.Factory.TryResultIsNull(nameof(execute)));
            }
            if (!execResult)
            {
                return execResult.ErrorMessage;
            }

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
        public static async Task<TryResult<TOut>> ThenTryAsync<TOut>(this Task<TryResult> input, Func<Task<TryResult<TOut>>> execute)
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
        public static async Task<TryResult<TOut>> ThenTryAsync<TIn, TOut>(this Task<TryResult<TIn>> input, Func<TIn, Task<TryResult<TOut>>> execute)
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

            var result = await execute(inputResult.Result);
            return result;
        }
    }
}
