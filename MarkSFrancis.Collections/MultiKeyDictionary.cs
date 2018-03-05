using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Collections.Extensions;

namespace MarkSFrancis.Collections
{
    /// <summary>
    /// A version of <see cref="Dictionary{TKey,TValue}"/> designed to work with <see cref="KeyValuePair{TKey,TValue}"/>, with multiple keys
    /// </summary>
    /// <typeparam name="TKey">The type of key</typeparam>
    /// <typeparam name="TValue">The type of value</typeparam>
    public class MultiKeyDictionary<TKey, TValue> : List<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>
    {
        /// <summary>
        /// Adds a <see cref="KeyValuePair{TKey,TValue}"/> to the end of the <see cref="MultiKeyDictionary{TKey,TValue}"/>
        /// </summary>
        /// <param name="key">The key to add</param>
        /// <param name="value">The value to add</param>
        public void Add(TKey key, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        /// <summary>
        /// Gets whether this <see cref="KeyValuePair{TKey,TValue}"/> contains a key
        /// </summary>
        /// <param name="key">The key to search for</param>
        /// <returns>Whether this <see cref="KeyValuePair{TKey,TValue}"/> contains a key</returns>
        public bool ContainsKey(TKey key)
        {
            var defaultEquatable = ComparerHelpers.DefaultEqualityComparer<TKey>();
            return this.Any(entry => defaultEquatable.Equals(entry.Key, key));
        }

        /// <summary>
        /// Gets whether this <see cref="KeyValuePair{TKey,TValue}"/> contains a value
        /// </summary>
        /// <param name="value">The value to search for</param>
        /// <returns>Whether this <see cref="KeyValuePair{TKey,TValue}"/> contains a value</returns>
        public bool ContainsValue(TValue value)
        {
            var defaultEquatable = ComparerHelpers.DefaultEqualityComparer<TValue>();
            return this.Any(entry => defaultEquatable.Equals(entry.Value, value));
        }

        /// <summary>
        /// Remove a key and all its values
        /// </summary>
        /// <param name="key">The key to remove</param>
        /// <returns>The number of entries removed</returns>
        public int Remove(TKey key)
        {
            var defaultEquatable = ComparerHelpers.DefaultEqualityComparer<TKey>();
            return RemoveAll(entry => defaultEquatable.Equals(entry.Key, key));
        }

        /// <summary>
        /// Try to get the value of the first instance of the key
        /// </summary>
        /// <param name="key">The key to get the first instance of</param>
        /// <param name="value">The value of the found key</param>
        /// <returns>Whether the key was present</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            var defaultEquatable = ComparerHelpers.DefaultEqualityComparer<TKey>();
            foreach (var entry in this)
            {
                if (defaultEquatable.Equals(entry.Key, key))
                {
                    value = entry.Value;
                    return true;
                }
            }

            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Try to get the key of the first instance of the value
        /// </summary>
        /// <param name="value">The value to get the first instance of</param>
        /// <param name="key">The key of the found value</param>
        /// <returns>Whether the value was present</returns>
        public bool TryGetKey(TValue value, out TKey key)
        {
            var defaultEquatable = ComparerHelpers.DefaultEqualityComparer<TValue>();
            foreach (var entry in this)
            {
                if (defaultEquatable.Equals(entry.Value, value))
                {
                    key = entry.Key;
                    return true;
                }
            }

            key = default(TKey);
            return false;
        }

        /// <summary>
        /// Get all values for a given key
        /// </summary>
        /// <param name="key">The key to search for</param>
        /// <returns>All values for a given key</returns>
        public List<TValue> GetValues(TKey key)
        {
            var defaultEquatable = ComparerHelpers.DefaultEqualityComparer<TKey>();
            List<TValue> values = new List<TValue>();

            foreach (var entry in this)
            {
                if (defaultEquatable.Equals(entry.Key, key))
                {
                    values.Add(entry.Value);
                }
            }

            return values;
        }

        /// <summary>
        /// Get all keys for a given value
        /// </summary>
        /// <param name="value">The value to search for</param>
        /// <returns>All keys for a given value</returns>
        public List<TKey> GetKeys(TValue value)
        {
            List<TKey> keys = new List<TKey>();
            var defaultEquatable = ComparerHelpers.DefaultEqualityComparer<TValue>();

            foreach (var entry in this)
            {
                if (defaultEquatable.Equals(entry.Value, value))
                {
                    keys.Add(entry.Key);
                }
            }

            return keys;
        }

        /// <summary>
        /// Get the value of the first instance of the key
        /// </summary>
        /// <param name="key">The key to get the first instance of</param>
        /// <returns>The value of the found key</returns>
        public TValue this[TKey key]
        {
            get
            {
                if (!TryGetValue(key, out TValue val))
                {
                    throw ErrorFactory.Default.KeyNotFound(nameof(key));
                }

                return val;
            }
        }

        /// <summary>
        /// All the keys in the collection
        /// </summary>
        public IEnumerable<TKey> Keys => this.Select(kv => kv.Key);

        /// <summary>
        /// All the values in the collection
        /// </summary>
        public IEnumerable<TValue> Values => this.Select(kv => kv.Value);

    }
}
