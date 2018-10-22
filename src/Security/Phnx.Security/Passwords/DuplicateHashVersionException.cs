using Phnx.Security.Passwords;
using System;

namespace Phnx.Security.Passwords
{
    /// <summary>
    /// The exception that is thrown when two or more <see cref="IPasswordHashVersion"/> with the same version number are added to the <see cref="PasswordHashManager"/>
    /// </summary>
    [Serializable]
    public class DuplicateHashVersionException : InvalidOperationException
    {
        /// <summary>
        /// Create a new instance of the <see cref="DuplicateHashVersionException"/> with an error message describing that <paramref name="hashVersion"/> has already been added
        /// </summary>
        /// <param name="hashVersion">The hash version which is a duplicate</param>
        public DuplicateHashVersionException(int hashVersion) : base($"{hashVersion} is already configured and cannot be re-added")
        {
        }

        /// <summary>
        /// Create a new instance of the <see cref="DuplicateHashVersionException"/> with an error message
        /// </summary>
        /// <param name="message">The message to diplay</param>
        public DuplicateHashVersionException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateHashVersionException"/> class with serialized data
        /// </summary>
        /// <param name="info">The object that holds the serialized object data</param>
        /// <param name="context">The contextual information about the source or destination</param>
        protected DuplicateHashVersionException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
