using System.Collections.Generic;

namespace Phnx
{
    /// <summary>
    /// A set of methods to help with comparing objects
    /// </summary>
    public static class ComparerHelpers
    {
        /// <summary>
        /// Get the default equality comparer for <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to get the default equality comparer for</typeparam>
        /// <returns>The default equality comparer for <typeparamref name="T"/></returns>
        public static EqualityComparer<T> DefaultEqualityComparer<T>()
        {
            return EqualityComparer<T>.Default;
        }

        /// <summary>
        /// Get the default comparer for <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to get the default comparer for</typeparam>
        /// <returns>The default comparer for <typeparamref name="T"/></returns>
        public static Comparer<T> DefaultComparer<T>()
        {
            return Comparer<T>.Default;
        }
    }
}
