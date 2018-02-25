using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Extensions
{
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Set (or add if it does not already exist) a key to the dictionary, using <see cref="ConverterHelpers.GetDefaultConverter{TSetAs,TStored}"/> as the type converter
        /// </summary>
        /// <typeparam name="TKey">The key type for entries in the <see cref="IDictionary{TKey,TStored}"/></typeparam>
        /// <typeparam name="TStored">The type stored in the <see cref="IDictionary{TKey,TStored}"/></typeparam>
        /// <typeparam name="TSetAs">The type to convert to <typeparamref name="TStored"/></typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey,TStored}"/> to set the key's value in</param>
        /// <param name="key">The key to the value to set</param>
        /// <param name="value">The value to set</param>
        public static void SetAs<TKey, TStored, TSetAs>(this IDictionary<TKey, TStored> dictionary, TKey key, TSetAs value)
        {
            SetAs(dictionary, key, value, ConverterHelpers.GetDefaultConverter<TSetAs, TStored>());
        }

        /// <summary>
        /// Set (or add if it does not already exist) a key to the dictionary, using a function to convert the desired type into the stored type
        /// </summary>
        /// <typeparam name="TKey">The key type for entries in the <see cref="IDictionary{TKey,TStored}"/></typeparam>
        /// <typeparam name="TStored">The type stored in the <see cref="IDictionary{TKey,TStored}"/></typeparam>
        /// <typeparam name="TSetAs">The type to convert to <typeparamref name="TStored"/></typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey,TStored}"/> to set the key's value in</param>
        /// <param name="key">The key to the value to set</param>
        /// <param name="value">The value to set</param>
        /// <param name="convertFunc">The function to use to convert <typeparamref name="TSetAs"/> to <typeparamref name="TStored"/></param>
        public static void SetAs<TKey, TStored, TSetAs>(this IDictionary<TKey, TStored> dictionary, TKey key, TSetAs value, Func<TSetAs, TStored> convertFunc)
        {
            TStored valueAsStoredType;
            if (value is TStored)
            {
                valueAsStoredType = (TStored)(object)value;
            }
            else
            {
                valueAsStoredType = convertFunc(value);
            }

            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, valueAsStoredType);
            }
            else
            {
                dictionary[key] = valueAsStoredType;
            }
        }
    }
}
