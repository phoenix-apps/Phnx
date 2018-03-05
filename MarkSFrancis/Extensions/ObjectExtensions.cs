using System;
using System.Collections.Generic;

namespace MarkSFrancis.Extensions
{
    /// <summary>
    /// Extensions for <see cref="object"/>
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Check whether an object is assignable to a type
        /// </summary>
        /// <param name="o">The object to check</param>
        /// <param name="t">The type to check if the object is assinable to</param>
        /// <returns></returns>
        public static bool Is(this object o, Type t)
        {
            Type oType = o.GetType();

            return oType.Is(t);
        }
        
        /// <summary>
        /// Check whether a type is assignable to a type
        /// </summary>
        /// <param name="childType">The type to check</param>
        /// <param name="parentType">The type to check if the object is assinable to</param>
        /// <returns></returns>
        public static bool Is(this Type childType, Type parentType)
        {
            if (childType == parentType)
            {
                return true;
            }

            return parentType.IsAssignableFrom(childType);
        }

        /// <summary>
        /// Allow an action to be chained onto this object. Useful for chaining methods all related to the same object onto a single line
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
        /// Converts a single object to an <see cref="IEnumerable{T}"/> of length 1
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IEnumerable{T}"/> to create</typeparam>
        /// <param name="single">The value to place in the collection</param>
        /// <returns></returns>
        public static IEnumerable<T> SingleToIEnumerable<T>(this T single)
        {
            yield return single;
        }
    }
}