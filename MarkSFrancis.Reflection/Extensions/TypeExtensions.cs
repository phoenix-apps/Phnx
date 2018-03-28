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
        /// Get all public properties and fields for <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to get all properties and fields of</typeparam>
        /// <param name="tType">The type to get all properties and fields of</param>
        /// <param name="getProperties">Whether to get all the properties of the given type</param>
        /// <param name="getFields">Whether to get all the fields of the given type</param>
        /// <returns>All properties and fields for <typeparamref name="T"/></returns>
        public static IEnumerable<PropertyFieldInfo<T, object>> GetPropertyFieldInfos<T>(this Type tType, bool getProperties = true, bool getFields = true)
        {
            if (getProperties)
            {
                foreach (var property in tType.GetProperties())
                {
                    yield return new PropertyFieldInfo<T, object>(property);
                }
            }

            if (getFields)
            {
                foreach (var field in tType.GetFields())
                {
                    yield return new PropertyFieldInfo<T, object>(field);
                }
            }
        }

        /// <summary>
        /// Get all public properties and fields for <paramref name="tType"/>
        /// </summary>
        /// <param name="tType">The type to get all properties and fields of</param>
        /// <param name="getProperties">Whether to get all the properties of the given type</param>
        /// <param name="getFields">Whether to get all the fields of the given type</param>
        /// <returns>All properties and fields for <paramref name="tType"/></returns>
        public static IEnumerable<PropertyFieldInfo> GetPropertyFieldInfos(this Type tType, bool getProperties = true, bool getFields = true)
        {
            if (getProperties)
            {
                foreach (var property in tType.GetProperties())
                {
                    yield return new PropertyFieldInfo(property);
                }
            }

            if (getFields)
            {
                foreach (var field in tType.GetFields())
                {
                    yield return new PropertyFieldInfo(field);
                }
            }
        }
    }
}
