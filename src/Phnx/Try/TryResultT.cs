namespace Phnx.Try
{
    /// <summary>
    /// Represents the result of something that could have failed, with its result if it succeeded, and its error message if it failed
    /// </summary>
    public class TryResult<T> : TryResult
    {
        /// <summary>
        /// Creates a new <see cref="TryResult"/> that failed
        /// </summary>
        /// <param name="error">The message that describes the reason for failure</param>
        protected TryResult(string error) : base(error)
        {
        }

        /// <summary>
        /// Creates a new <see cref="TryResult"/> that succeeded
        /// </summary>
        /// <param name="result">The result of the success</param>
        protected TryResult(T result) : base()
        {
            Result = result;
        }

        /// <summary>
        /// The result, if <see cref="TryResult.Success"/> is <see langword="true"/>
        /// </summary>
        public T Result { get; }

        /// <summary>
        /// Deconstructs into its separate <see cref="TryResult.Success"/>, <see cref="Result"/> and <see cref="TryResult.ErrorMessage"/> components
        /// </summary>
        /// <param name="success"><see cref="TryResult.Success"/></param>
        /// <param name="result"><see cref="Result"/></param>
        /// <param name="error"><see cref="TryResult.ErrorMessage"/></param>
        public void Deconstruct(out bool success, out T result, out string error)
        {
            success = Success;
            result = Result;
            error = ErrorMessage;
        }

        /// <summary>
        /// Creates a new failed <see cref="TryResult{T}"/>, with a specified <paramref name="error"/>
        /// </summary>
        /// <param name="error">The error message that describes the reason for failure</param>
        /// <returns>A new non-successful <see cref="TryResult{T}"/></returns>
        public static new TryResult<T> Fail(string error)
        {
            return new TryResult<T>(error);
        }

        /// <summary>
        /// Creates a new success <see cref="TryResult{T}"/>
        /// </summary>
        /// <param name="result">The result of the success</param>
        /// <returns>A successful <see cref="TryResult{T}"/></returns>
        public static TryResult<T> Succeed(T result)
        {
            return new TryResult<T>(result);
        }

        /// <summary>
        /// Converts to a failed <see cref="TryResult{T}"/>, with an error message
        /// </summary>
        /// <param name="error">The reason for failure</param>
        public static implicit operator TryResult<T>(string error)
        {
            return Fail(error);
        }

        /// <summary>
        /// Converts to a successfull <see cref="TryResult{T}"/>, with a given <paramref name="result"/>
        /// </summary>
        public static implicit operator TryResult<T>(T result)
        {
            return Succeed(result);
        }

        /// <summary>
        /// Converts to a successful or failed <see cref="TryResult"/>, with a result or error message as applicable
        /// </summary>
        /// <param name="result">The combined success and error message for the new <see cref="TryResult"/></param>
        public static implicit operator TryResult<T>((bool success, T result, string error) result)
        {
            return result.success ? Succeed(result.result) : Fail(result.error);
        }

        /// <summary>
        /// Deconstructs to its <see cref="TryResult.Success"/>, <see cref="Result"/> and <see cref="TryResult.ErrorMessage"/>
        /// </summary>
        /// <param name="result">The combined success and error message for the new <see cref="TryResult"/></param>
        public static implicit operator (bool success, T result, string error)(TryResult<T> result)
        {
            return (result.Success, result.Result, result.ErrorMessage);
        }
    }
}
