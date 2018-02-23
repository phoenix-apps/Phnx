using System;
using System.Collections.Generic;
using MarkSFrancis.Extensions;

namespace MarkSFrancis.Collections.Extensions
{
    /// <summary>
    /// A set of extension methods for converting the stored type into the desired type (such as getting values from an object cache)
    /// </summary>
    public static class IReadOnlyDictionaryExtensions
    {
        /// <summary>
        /// Get a given key from the service, using <see cref="o:Convert.ChangeType"/> to convert the stored type into the desired type. <see cref="o:Convert.ChangeType"/>  is only used if the stored type is not already the desired type
        /// </summary>
        /// <typeparam name="TKey">The key type for entries in the <see cref="IReadOnlyDictionary{TKey,TValue}"/></typeparam>
        /// <typeparam name="TStored">The type stored in the <see cref="IReadOnlyDictionary{TKey,TValue}"/></typeparam>
        /// <typeparam name="TConvertTo">The type to get from the <see cref="IReadOnlyDictionary{TKey,TValue}"/></typeparam>
        /// <param name="dictionary">The <see cref="IReadOnlyDictionary{TKey,TValue}"/> to get the given key from</param>
        /// <param name="key">The key to the item to load</param>
        /// <returns>Returns the value of a given key converted to T</returns>
        public static TConvertTo GetAs<TKey, TStored, TConvertTo>(this IReadOnlyDictionary<TKey, TStored> dictionary, TKey key)
        {
            return GetAs(dictionary, key, s => (TConvertTo)Convert.ChangeType(s, typeof(TConvertTo)));
        }

        /// <summary>
        /// Get a given key from the service, using a given function to convert the stored type into the desired type
        /// </summary>
        /// <typeparam name="TKey">The key type for entries in the <see cref="IDictionary{TKey,TValue}"/></typeparam>
        /// <typeparam name="TStored">The type stored in the <see cref="IDictionary{TKey,TValue}"/></typeparam>
        /// <typeparam name="TConvertTo">The type to get from the <see cref="IDictionary{TKey,TValue}"/></typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey,TValue}"/> to get the given key from</param>
        /// <param name="key">The key to the item to load</param>
        /// <param name="convertFunc">The function to use to convert the stored type into the desired type. This is only used if the stored type is not already the desired type</param>
        /// <returns>Returns the value of a given key converted to T</returns>
        public static TConvertTo GetAs<TKey, TStored, TConvertTo>(this IReadOnlyDictionary<TKey, TStored> dictionary, TKey key, Func<TStored, TConvertTo> convertFunc)
        {
            TStored valueAsStoredType = dictionary[key];

            if (valueAsStoredType == null)
            {
                throw ErrorFactory.Default.KeyNotFound(key.ToString());
            }

            if (valueAsStoredType is TConvertTo)
            {
                return (TConvertTo)(object)valueAsStoredType;
            }

            var convertedValue = convertFunc(valueAsStoredType);

            return convertedValue;
        }


        /// <summary>
        /// Get a given key from the service, using <see cref="o:Convert.ChangeType"/> to convert the stored type into the desired type. <see cref="o:Convert.ChangeType"/>  is only used if the stored type is not already the desired type
        /// </summary>
        /// <typeparam name="TKey">The key type for entries in the <see cref="IReadOnlyDictionary{TKey,TValue}"/></typeparam>
        /// <typeparam name="TStored">The type stored in the <see cref="IReadOnlyDictionary{TKey,TValue}"/></typeparam>
        /// <typeparam name="TConvertTo">The type to get from the <see cref="IReadOnlyDictionary{TKey,TValue}"/></typeparam>
        /// <param name="dictionary">The <see cref="IReadOnlyDictionary{TKey,TValue}"/> to get the given key from</param>
        /// <param name="key">The key to the item to load</param>
        /// <param name="value">The value of a given key converted to T</param>
        /// <returns>Whether the value was retrieved and converted successfully</returns>
        public static bool TryGetAs<TKey, TStored, TConvertTo>(this IReadOnlyDictionary<TKey, TStored> dictionary, TKey key, out TConvertTo value)
        {
            try
            {
                value = GetAs<TKey, TStored, TConvertTo>(dictionary, key);
                return true;
            }
            catch
            {
                value = default(TConvertTo);
                return false;
            }
        }

        /// <summary>
        /// Get a given key from the service, using a given function to convert the stored type into the desired type
        /// </summary>
        /// <typeparam name="TKey">The key type for entries in the <see cref="IDictionary{TKey,TValue}"/></typeparam>
        /// <typeparam name="TStored">The type stored in the <see cref="IDictionary{TKey,TValue}"/></typeparam>
        /// <typeparam name="TConvertTo">The type to get from the <see cref="IDictionary{TKey,TValue}"/></typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey,TValue}"/> to get the given key from</param>
        /// <param name="key">The key to the item to load</param>
        /// <param name="convertFunc">The function to use to convert the stored type into the desired type. This is only used if the stored type is not already the desired type</param>
        /// <param name="value">The value of a given key converted to T</param>
        /// <returns>Whether the value was retrieved and converted successfully</returns>
        public static bool TryGetAs<TKey, TStored, TConvertTo>(this IReadOnlyDictionary<TKey, TStored> dictionary, TKey key, Func<TStored, TConvertTo> convertFunc, out TConvertTo value)
        {
            try
            {
                value = GetAs(dictionary, key, convertFunc);
                return true;
            }
            catch
            {
                value = default(TConvertTo);
                return false;
            }
        }
    }
}