using System;

namespace MarkSFrancis.ThrowHelpers
{
    /// <summary>
    /// Provides helpers for creating exceptions for <see cref="ErrorFactory"/>
    /// </summary>
    public interface IThrowHelper
    {
        /// <summary>
        /// Create the exception
        /// </summary>
        Exception Create();

        /// <summary>
        /// Create the exception with an inner exception
        /// </summary>
        Exception Create(Exception innerException);
    }
}