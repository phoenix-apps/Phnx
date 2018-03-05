using System;

namespace MarkSFrancis.Windows.Extensions
{
    /// <summary>
    /// Extensions for <see cref="ErrorFactory"/>
    /// </summary>
    public static class ErrorFactoryExtensions
    {
        /// <summary>
        /// An error to desribe that the MemberwiseClone method is missing or not available
        /// </summary>
        /// <param name="factory">The error factory to extend</param>
        /// <param name="typeMissingMethod">The <see cref="Type"/> that was missing the MemberwiseClone method</param>
        /// <returns></returns>
        public static NotSupportedException MemberwiseCloneNotAvailable(this ErrorFactory factory,
            Type typeMissingMethod)
        {
            return new NotSupportedException(
                $"The MemberwiseClone method is missing for the type {typeMissingMethod.FullName}, and therefore it cannot be shallow cloned");
        }
    }
}
