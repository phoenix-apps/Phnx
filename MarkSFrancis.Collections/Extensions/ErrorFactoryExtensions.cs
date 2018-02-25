using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Extensions
{
    public static class ErrorFactoryExtensions
    {
        public static IndexOutOfRangeException CollectionEmpty(this ErrorFactory factory, string paramName)
        {
            return new IndexOutOfRangeException($"{paramName} cannot be an empty collection");
        }

        public static KeyNotFoundException KeyNotFound(this ErrorFactory factory, string key)
        {
            return new KeyNotFoundException($"The key \"{key}\" was not found in the collection");
        }

        public static KeyNotFoundException KeyNotFound(this ErrorFactory factory, string key, string collectionName)
        {
            return new KeyNotFoundException($"The key \"{key}\" was not found in the collection \"{collectionName}\"");
        }
    }
}
