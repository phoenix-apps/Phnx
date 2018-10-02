using System;
using System.Runtime.Serialization;

namespace Phnx
{
    /// <summary>
    /// The exception that is thrown when the value of an argument is less than zero, which is outside the range of allowable values as defined by the invoked method
    /// </summary>
    [Serializable]
    public class ArgumentLessThanZeroException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentLessThanZeroException"/> class
        /// </summary>
        public ArgumentLessThanZeroException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentLessThanZeroException"/> class with the name of the parameter that causes this exception
        /// </summary>
        /// <param name="paramName">The name of the parameter that causes this exception</param>
        public ArgumentLessThanZeroException(string paramName) : base(paramName, "Value cannot be less than zero") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentLessThanZeroException"/> class with a specified error message and the exception that is the cause of this exception
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> reference if no inner exception is specified</param>
        public ArgumentLessThanZeroException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentLessThanZeroException"/> class with the name of the parameter that causes this exception and a specified error message
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception</param>
        /// <param name="message">The message that describes the error</param>
        public ArgumentLessThanZeroException(string paramName, string message) : base(paramName, message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentLessThanZeroException"/> class with serialized data
        /// </summary>
        /// <param name="info">The object that holds the serialized object data</param>
        /// <param name="context">An object that describes the source or destination of the serialized data</param>
        protected ArgumentLessThanZeroException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
