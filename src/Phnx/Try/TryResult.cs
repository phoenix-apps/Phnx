namespace Phnx.Try
{
    /// <summary>
    /// Represents the result of something that could have failed, and its error message if it failed
    /// </summary>
    public class TryResult
    {
        /// <summary>
        /// Creates a new <see cref="TryResult"/> that was successful
        /// </summary>
        protected TryResult()
        {
            Success = true;
        }

        /// <summary>
        /// Creates a new <see cref="TryResult"/> that failed
        /// </summary>
        /// <param name="error">The message that describes the reason for failure</param>
        protected TryResult(string error)
        {
            Success = false;
            _error = error;
        }

        /// <summary>
        /// Whether the result was successful
        /// </summary>
        public bool Success { get; }

        private readonly string _error;

        /// <summary>
        /// The error message if <see cref="Success"/> is <see langword="false" />, otherwise <see langword="null"/>
        /// </summary>
        public string ErrorMessage => Success ? null : _error;

        /// <summary>
        /// Converts this <see cref="TryResult"/> to a string, showing its success, or <see cref="ErrorMessage"/>
        /// </summary>
        public override string ToString()
        {
            if (Success)
            {
                return $"Success: {Success}";
            }
            else
            {
                return $"Success: {Success}, Error: {ErrorMessage}";
            }
        }

        /// <summary>
        /// Creates a new failed <see cref="TryResult"/>, with a specified <paramref name="error"/>
        /// </summary>
        /// <param name="error">The error message that describes the reason for failure</param>
        /// <returns>A new non-successful <see cref="TryResult"/></returns>
        public static TryResult Fail(string error)
        {
            return new TryResult(error);
        }

        /// <summary>
        /// Creates a new success <see cref="TryResult"/>
        /// </summary>
        /// <returns>A successful <see cref="TryResult"/></returns>
        public static TryResult Succeed()
        {
            return new TryResult();
        }

        /// <summary>
        /// Deconstructs into its separate <see cref="Success"/> and <see cref="ErrorMessage"/> components
        /// </summary>
        /// <param name="success"><see cref=" Success"/></param>
        /// <param name="error"><see cref="ErrorMessage"/></param>
        public void Deconstruct(out bool success, out string error)
        {
            success = Success;
            error = ErrorMessage;
        }

        /// <summary>
        /// Converts to a <see cref="bool"/>, based on <see cref="Success"/>
        /// </summary>
        public static implicit operator bool(TryResult result)
        {
            return result.Success;
        }

        /// <summary>
        /// Converts to a failed <see cref="TryResult"/>, with an error message
        /// </summary>
        /// <param name="error">The reason for failure</param>
        public static implicit operator TryResult(string error)
        {
            return Fail(error);
        }

        /// <summary>
        /// Converts to a successful or failed <see cref="TryResult"/>, with an error message if applicable
        /// </summary>
        /// <param name="result">The combined success and error message for the new <see cref="TryResult"/></param>
        public static implicit operator TryResult((bool success, string error) result)
        {
            return result.success ? Succeed() : Fail(result.error);
        }

        /// <summary>
        /// Deconstructs to its <see cref="Success"/> and <see cref="ErrorMessage"/>
        /// </summary>
        /// <param name="result">The combined success and error message for the new <see cref="TryResult"/></param>
        public static implicit operator (bool success, string error)(TryResult result)
        {
            return (result.Success, result.ErrorMessage);
        }
    }
}
