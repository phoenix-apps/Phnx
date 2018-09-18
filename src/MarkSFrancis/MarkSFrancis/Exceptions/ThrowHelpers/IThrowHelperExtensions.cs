using System;

namespace MarkSFrancis.ThrowHelpers
{
    /// <summary>
    /// Extensions <see cref="IThrowHelper"/> with Throw methods, using the existing Create methods
    /// </summary>
    public static class IThrowHelperExtensions
    {
        /// <summary>
        /// Throw the exception
        /// </summary>
        /// <param name="throwHelper">The <see cref="IThrowHelper"/> to extend</param>
        public static void Throw(this IThrowHelper throwHelper)
        {
            throw throwHelper.Create();
        }

        /// <summary>
        /// Throw the exception with an inner exception
        /// </summary>
        /// <param name="throwHelper">The <see cref="IThrowHelper"/> to extend</param>
        /// <param name="innerException">The inner <see cref="Exception"/> to put into the thrown <see cref="Exception"/></param>
        public static void Throw(this IThrowHelper throwHelper, Exception innerException)
        {
            throw throwHelper.Create(innerException);
        }
    }
}
