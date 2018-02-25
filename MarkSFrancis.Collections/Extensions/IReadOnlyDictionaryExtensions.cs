using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MarkSFrancis.Collections.Extensions
{
    /// <summary>
    /// A set of extension methods for converting the stored type into the desired type (such as getting values from an object cache)
    /// </summary>
    public static class IReadOnlyDictionaryExtensions
    {
        /// <summary>
        /// Get a key's value from the dictionary, using <see cref="ConverterHelpers.GetDefaultConverter{TStored,TGetAs}"/> as the type converter
        /// </summary>
        /// <typeparam name="TKey">The key type for entries in the <see cref="IReadOnlyDictionary{TKey,TStored}"/></typeparam>
        /// <typeparam name="TStored">The type stored in the <see cref="IReadOnlyDictionary{TKey,TStored}"/></typeparam>
        /// <typeparam name="TGetAs">The type to convert the <typeparamref name="TStored"/> to</typeparam>
        /// <param name="dictionary">The <see cref="IReadOnlyDictionary{TKey,TStored}"/> to get the key's value from</param>
        /// <param name="key">The key to the value to get</param>
        /// <returns>The key's value converted to <typeparamref name="TGetAs"/></returns>
        public static TGetAs GetAs<TKey, TStored, TGetAs>(this IReadOnlyDictionary<TKey, TStored> dictionary, TKey key)
        {
            return GetAs(dictionary, key, ConverterHelpers.GetDefaultConverter<TStored, TGetAs>());
        }

        /// <summary>
        /// Get a key's value from the dictionary, using a function to convert the stored type into the desired type
        /// </summary>
        /// <typeparam name="TKey">The key type for entries in the <see cref="IDictionary{TKey,TStored}"/></typeparam>
        /// <typeparam name="TStored">The type stored in the <see cref="IDictionary{TKey,TStored}"/></typeparam>
        /// <typeparam name="TGetAs">The type to convert the <typeparamref name="TStored"/> to</typeparam>
        /// <param name="dictionary">The <see cref="IReadOnlyDictionary{TKey,TStored}"/> to get the key's value from</param>
        /// <param name="key">The key to the value to get</param>
        /// <param name="convertFunc">The function to use to convert <typeparamref name="TStored"/> to <typeparamref name="TGetAs"/></param>
        /// <returns>The key's value converted to <typeparamref name="TGetAs"/></returns>
        public static TGetAs GetAs<TKey, TStored, TGetAs>(this IReadOnlyDictionary<TKey, TStored> dictionary, TKey key, Func<TStored, TGetAs> convertFunc)
        {
            if (dictionary == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(key));
            }

            if (key == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(key));
            }

            if (convertFunc == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(convertFunc));
            }

            TStored valueAsStoredType = dictionary[key];

            var convertedValue = convertFunc(valueAsStoredType);

            return convertedValue;
        }
        
        /// <summary>
        /// Try to get a key's value from the dictionary, using <see cref="ConverterHelpers.GetDefaultConverter{TStored,TGetAs}"/> as the type converter
        /// </summary>
        /// <typeparam name="TKey">The key type for entries in the <see cref="IReadOnlyDictionary{TKey,TStored}"/></typeparam>
        /// <typeparam name="TStored">The type stored in the <see cref="IReadOnlyDictionary{TKey,TStored}"/></typeparam>
        /// <typeparam name="TGetAs">The type to convert the <typeparamref name="TStored"/> to</typeparam>
        /// <param name="dictionary">The <see cref="IReadOnlyDictionary{TKey,TStored}"/> to get the key's value from</param>
        /// <param name="key">The key to the value to get</param>
        /// <param name="value">The key's value converted to <typeparamref name="TGetAs"/></param>
        /// <returns>Whether the value was retrieved and converted successfully</returns>
        public static bool TryGetAs<TKey, TStored, TGetAs>(this IReadOnlyDictionary<TKey, TStored> dictionary, TKey key, out TGetAs value)
        {
            try
            {
                value = GetAs<TKey, TStored, TGetAs>(dictionary, key);
                return true;
            }
            catch
            {
                value = default(TGetAs);
                return false;
            }
        }
        
        /// <summary>
        /// Try to get a key's value from the dictionary, using <see cref="ConverterHelpers.GetDefaultConverter{TStored,TGetAs}"/> as the type converter
        /// </summary>
        /// <typeparam name="TKey">The key type for entries in the <see cref="IReadOnlyDictionary{TKey,TStored}"/></typeparam>
        /// <typeparam name="TStored">The type stored in the <see cref="IReadOnlyDictionary{TKey,TStored}"/></typeparam>
        /// <typeparam name="TGetAs">The type to convert the <typeparamref name="TStored"/> to</typeparam>
        /// <param name="dictionary">The <see cref="IReadOnlyDictionary{TKey,TStored}"/> to get the key's value from</param>
        /// <param name="key">The key to the value to get</param>
        /// <param name="convertFunc">The function to use to convert <typeparamref name="TStored"/> to <typeparamref name="TGetAs"/></param>
        /// <param name="value">The key's value converted to <typeparamref name="TGetAs"/></param>
        /// <returns>Whether the value was retrieved and converted successfully</returns>
        public static bool TryGetAs<TKey, TStored, TGetAs>(this IReadOnlyDictionary<TKey, TStored> dictionary, TKey key, Func<TStored, TGetAs> convertFunc, out TGetAs value)
        {
            try
            {
                value = GetAs(dictionary, key, convertFunc);
                return true;
            }
            catch
            {
                value = default(TGetAs);
                return false;
            }
        }
    }
}