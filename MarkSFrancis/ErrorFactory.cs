using System;

namespace MarkSFrancis
{
    /// <summary>
    /// An exception factory which contains a series of common errors and messages. To extend this object, use extension methods instead of inheriting
    /// </summary>
    public sealed class ErrorFactory
    {
        /// <summary>
        /// This <see cref="ErrorFactory"/> available as a static object for use across the rest of the application
        /// </summary>
        public static readonly ErrorFactory Default = new ErrorFactory();

        /// <summary>
        /// Create an <see cref="InvalidCastException"/> for a given failed cast
        /// </summary>
        /// <param name="paramName">The name of the parameter that could not be cast</param>
        /// <param name="castingFrom">The type being cast from</param>
        /// <param name="castingTo">The type being cast to</param>
        /// <returns></returns>
        public InvalidCastException InvalidCast(string paramName, Type castingFrom, Type castingTo)
        {
            return InvalidCast(paramName, castingFrom.FullName, castingTo.FullName);
        }

        /// <summary>
        /// Create an <see cref="InvalidCastException"/> for a given failed cast
        /// </summary>
        /// <param name="paramName">The name of the parameter that could not be cast</param>
        /// <param name="castingFrom">The type being cast from's full name</param>
        /// <param name="castingTo">The type being cast to's full name</param>
        /// <returns></returns>
        public InvalidCastException InvalidCast(string paramName, string castingFrom, string castingTo)
        {
            return new InvalidCastException(nameof(paramName) + " cannot be cast from type " + castingFrom + " to type " + castingTo);
        }
    }
}
