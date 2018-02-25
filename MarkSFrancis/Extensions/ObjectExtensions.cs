using System;
using System.Collections.Generic;

namespace MarkSFrancis.Extensions
{
    /// <summary>
    /// Provides a series of extension methods onto all objects
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Check whether a given object is assignable to a given type
        /// </summary>
        /// <param name="o">The object to check</param>
        /// <param name="t">The type to check if the object is assinable to</param>
        /// <returns></returns>
        public static bool Is(this object o, Type t)
        {
            Type oType = o.GetType();
            if (oType == t)
            {
                return true;
            }

            return t.IsAssignableFrom(oType);
        }

        /// <summary>
        /// Allow a given action to be chained onto this object. Useful for chaining methods all related to the same object onto a single line
        /// </summary>
        /// <typeparam name="T">The type of object to chain actions to</typeparam>
        /// <param name="t">The object to chain actions to</param>
        /// <param name="action">The action to perform in the chain</param>
        /// <returns></returns>
        public static T Chain<T>(this T t, Action<T> action)
        {
            action(t);
            return t;
        }

        /// <summary>
        /// Uses <see cref="EqualityComparer{T}.Default"/> to determine whether two objects are identical
        /// </summary>
        /// <typeparam name="T">The type of objects to check</typeparam>
        /// <param name="me">The first object to check</param>
        /// <param name="other">The second object to check</param>
        /// <returns></returns>
        public static bool IsEqualTo<T>(this T me, T other)
        {
            var comparer = EqualityComparer<T>.Default;

            return comparer.Equals(me, other);
        }

        /// <summary>
        /// Converts a given single object to an <see cref="IEnumerable{T}"/> of length 1
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IEnumerable{T}"/> to create</typeparam>
        /// <param name="single">The item to place in the collection</param>
        /// <returns></returns>
        public static IEnumerable<T> SingleToIEnumerable<T>(this T single)
        {
            yield return single;
        }
    }
}