using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Collections.Extensions;

namespace MarkSFrancis.Collections
{
    public class MultiKeyDictionary<TKey, TValue> : List<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>
    {
        public void Add(TKey key, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public bool ContainsKey(TKey key)
        {
            var defaultEquatable = ComparerHelpers.DefaultEqualityComparer<TKey>();
            return this.Any(entry => defaultEquatable.Equals(entry.Key, key));
        }

        public bool ContainsValue(TValue value)
        {
            var defaultEquatable = ComparerHelpers.DefaultEqualityComparer<TValue>();
            return this.Any(entry => defaultEquatable.Equals(entry.Value, value));
        }

        public bool Remove(TKey key)
        {
            var defaultEquatable = ComparerHelpers.DefaultEqualityComparer<TKey>();
            return RemoveAll(entry => defaultEquatable.Equals(entry.Key, key)) > 0;
        }

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

        public IEnumerable<TKey> Keys => this.Select(kv => kv.Key);
        public IEnumerable<TValue> Values => this.Select(kv => kv.Value);

    }
}
