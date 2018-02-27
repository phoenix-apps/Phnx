using System;

namespace MarkSFrancis.Windows.Extensions
{
    /// <summary>
    /// Extensions for <see cref="ErrorFactory"/>
    /// </summary>
    public static class ErrorFactoryExtensions
    {
        public static NotSupportedException OperationInvalidException(this ErrorFactory factory, string message)
        {
            return new NotSupportedException(message);
        }
    }
}
