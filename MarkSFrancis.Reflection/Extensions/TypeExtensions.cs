using System;
using System.Collections.Generic;

namespace MarkSFrancis.Reflection.Extensions
{
    /// <summary>
    /// Extensions for <see cref="Type"/>
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Get all properties and fields for <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to get all properties and fields of</typeparam>
        /// <returns>All properties and fields for <typeparamref name="T"/></returns>
        public static IEnumerable<PropertyFieldInfo<T, object>> GetPropertyFieldInfos<T>(this Type tType)
        {
            foreach (var property in tType.GetProperties())
            {
                yield return new PropertyFieldInfo<T, object>(property);
            }

            foreach (var field in tType.GetFields())
            {
                yield return new PropertyFieldInfo<T, object>(field);
            }
        }
    }
}
