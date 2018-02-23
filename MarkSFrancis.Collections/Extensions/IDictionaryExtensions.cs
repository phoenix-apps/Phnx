using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Extensions
{
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Set (or add if it does not already exist) a given key to the dictionary, using <see cref="o:Convert.ChangeType"/> to convert the given type into the stored type. <see cref="o:Convert.ChangeType"/> is only used if the given type is not inherited from the stored type
        /// </summary>
        /// <typeparam name="TKey">The key type for entries in the <see cref="IDictionary{TKey,TValue}"/></typeparam>
        /// <typeparam name="TStored">The type stored in the <see cref="IDictionary{TKey,TValue}"/></typeparam>
        /// <typeparam name="TConvertFrom">The type of value to set in the <see cref="IDictionary{TKey,TValue}"/></typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey,TValue}"/> to set the given key in</param>
        /// <param name="key">The key to the item to set</param>
        /// <param name="value">The value to set</param>
        public static void SetAs<TKey, TStored, TConvertFrom>(this IDictionary<TKey, TStored> dictionary, TKey key, TConvertFrom value)
        {
            SetAs(dictionary, key, value, s => (TStored)Convert.ChangeType(s, typeof(TStored)));
        }

        /// <summary>
        /// Set (or add if it does not already exist) a given key to the dictionary, using a given function to convert the given type to the stored type
        /// </summary>
        /// <typeparam name="TKey">The key type for entries in the <see cref="IDictionary{TKey,TValue}"/></typeparam>
        /// <typeparam name="TStored">The type stored in the <see cref="IDictionary{TKey,TValue}"/></typeparam>
        /// <typeparam name="TConvertFrom">The type of value to set in the <see cref="IDictionary{TKey,TValue}"/></typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey,TValue}"/> to set the given key in</param>
        /// <param name="key">The key to the item to set</param>
        /// <param name="convertFunc">The function to use to convert the stored type into the desired type. This is only used if the stored type is not inherited from the desired type</param>
        /// <param name="value">The value to set</param>
        public static void SetAs<TKey, TStored, TConvertFrom>(this IDictionary<TKey, TStored> dictionary, TKey key, TConvertFrom value, Func<TConvertFrom, TStored> convertFunc)
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
