using System;
using System.Reflection;

namespace MarkSFrancis.Extensions.Reflection
{
    public static class MemberInfoExtensions
    {
        /// <summary>Retrieves a custom attribute of a specified type that is applied to a specified member.</summary>
        /// <param name="member">The member to inspect.</param>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <returns>A custom attribute that matches <paramref name="T" />, or null if no such attribute is found.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="member" /> is null. </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// <paramref name="member" /> is not a constructor, method, property, event, type, or field. </exception>
        /// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
        /// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
        public static T GetAttribute<T>(this MemberInfo member) where T : Attribute
        {
            return member.GetCustomAttribute<T>();
        }
    }
}