using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Extensions
{
    /// <summary>
    /// Extensions for <see cref="ErrorFactory"/>
    /// </summary>
    public static class ErrorFactoryExtensions
    {
        /// <summary>
        /// An error to describe that an action cannot be performed because the collection is empty
        /// </summary>
        /// <param name="factory">The factory to extend</param>
        /// <param name="paramName">The name of the empty collection</param>
        /// <returns></returns>
        public static IndexOutOfRangeException CollectionEmpty(this ErrorFactory factory, string paramName)
        {
            return new IndexOutOfRangeException($"{paramName} cannot be an empty collection");
        }

        /// <summary>
        /// An error to describe that the key was not found within a collection
        /// </summary>
        /// <param name="factory">The factory to extend</param>
        /// <param name="key">The name of the key</param>
        /// <returns></returns>
        public static KeyNotFoundException KeyNotFound(this ErrorFactory factory, string key)
        {
            return new KeyNotFoundException($"The key \"{key}\" was not found in the collection");
        }

        /// <summary>
        /// An error to describe that the key was not found within a collection
        /// </summary>
        /// <param name="factory">The factory to extend</param>
        /// <param name="key">The name of the key</param>
        /// <param name="collectionName">The name of the collection</param>
        /// <returns></returns>
        public static KeyNotFoundException KeyNotFound(this ErrorFactory factory, string key, string collectionName)
        {
            return new KeyNotFoundException($"The key \"{key}\" was not found in the collection \"{collectionName}\"");
        }

        /// <summary>
        /// An error to describe that the key was already present within a collection
        /// </summary>
        /// <param name="factory">The factory to extend</param>
        /// <param name="key">The name of the key</param>
        /// <param name="collectionName">The name of the collection</param>
        /// <returns></returns>
        public static ArgumentException DuplicateKey(this ErrorFactory factory, string key, string collectionName)
        {
            return new ArgumentException($"The key \"{key}\" is already present in the collection \"{collectionName}\"", key);
        }

        /// <summary>
        /// An error to describe that the key was already present within a collection
        /// </summary>
        /// <param name="factory">The factory to extend</param>
        /// <param name="key">The name of the key</param>
        /// <returns></returns>
        public static ArgumentException DuplicateKey(this ErrorFactory factory, string key)
        {
            return new ArgumentException($"The key \"{key}\" is already present in the collection", key);
        }
    }
}
