using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections
{
    /// <summary>
    /// Defines a key/value pair that can be set or retrieved
    /// </summary>
    /// <typeparam name="TKey">The type of the key</typeparam>
    /// <typeparam name="TValue">The type of the value</typeparam>
    public class KeyValue<TKey, TValue> : IEquatable<KeyValue<TKey, TValue>>
    {
        /// <summary>
        /// Create a new <see cref="KeyValue{TKey, TValue}"/> without a key or value
        /// </summary>
        public KeyValue()
        {

        }

        /// <summary>
        /// Create a new <see cref="KeyValue{TKey, TValue}"/> with a specified key and value
        /// </summary>
        /// <param name="key">The key for the value</param>
        /// <param name="value">The value to associate with the key</param>
        public KeyValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Get or set the key for the value
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Get or set the value associated with the key
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// Convert a <see cref="KeyValue{TKey, TValue}"/> to a read-only <see cref="KeyValuePair{TKey, TValue}"/>
        /// </summary>
        /// <param name="keyValue">The <see cref="KeyValue{TKey, TValue}"/> to convert</param>
        public static implicit operator KeyValuePair<TKey, TValue>(KeyValue<TKey, TValue> keyValue)
        {
            return new KeyValuePair<TKey, TValue>(keyValue.Key, keyValue.Value);
        }

        /// <summary>
        /// Convert a <see cref="KeyValuePair{TKey, TValue}"/> to a read and writable <see cref="KeyValue{TKey, TValue}"/>
        /// </summary>
        /// <param name="keyValuePair">The <see cref="KeyValuePair{TKey, TValue}"/> to convert</param>
        public static implicit operator KeyValue<TKey, TValue>(KeyValuePair<TKey, TValue> keyValuePair)
        {
            return new KeyValue<TKey, TValue>(keyValuePair.Key, keyValuePair.Value);
        }

        /// <summary>
        /// Get whether this <see cref="KeyValue{TKey, TValue}"/> equals another
        /// </summary>
        /// <param name="other">The <see cref="KeyValue{TKey, TValue}"/> to compare</param>
        /// <returns>Whether this is the same as <paramref name="other"/></returns>
        public bool Equals(KeyValue<TKey, TValue> other)
        {
            return other.Key.Equals(Key) && other.Value.Equals(Value);
        }

        /// <summary>
        /// Get whether this <see cref="KeyValue{TKey, TValue}"/> equals another <see cref="object"/>
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare</param>
        /// <returns>Whether this is the same as <paramref name="obj"/></returns>
        public override bool Equals(object obj)
        {
            if (obj is KeyValue<TKey, TValue> otherKeyValue)
            {
                return Equals(otherKeyValue);
            }
            else if (obj is KeyValue<TKey, TValue> otherKeyValuePair)
            {
                return Equals(otherKeyValuePair);
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Get a string representation of this item, including the key and value
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[{Key}, {Value}]";
        }
    }
}
