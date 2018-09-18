using System;

namespace MarkSFrancis.ThrowHelpers
{
    /// <summary>
    /// Provides helpers for creating exceptions for <see cref="Errors"/>
    /// </summary>
    public abstract class ThrowHelper
    {
        /// <summary>
        /// Create the exception
        /// </summary>
        public abstract Exception Create();

        /// <summary>
        /// Create the exception with an inner exception
        /// </summary>
        public abstract Exception Create(Exception innerException);

        /// <summary>
        /// Convert a <see cref="ThrowHelper"/> to its <see cref="Exception"/> using the default <see cref="Create()"/> method
        /// </summary>
        /// <param name="throwHelper">The <see cref="ThrowHelper"/> to convert</param>
        public static implicit operator Exception(ThrowHelper throwHelper)
        {
            return throwHelper.Create();
        }
    }
}