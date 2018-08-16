using System;

namespace MarkSFrancis.Data.Extensions
{
    /// <summary>
    /// Extensions for <see cref="ErrorFactory"/>
    /// </summary>
    public static class ErrorFactoryExtensions
    {
        /// <summary>
        /// Create an error to describe that the value/ property cannot be set as the operation is not supported
        /// </summary>
        /// <param name="factory">The factory to extend</param>
        /// <returns>A <see cref="NotSupportedException"/> describing that a value cannot be set</returns>
        public static NotSupportedException CannotSetValue(this ErrorFactory factory)
        {
            return new NotSupportedException("Cannot set value. Operation not supported");
        }
    }
}
