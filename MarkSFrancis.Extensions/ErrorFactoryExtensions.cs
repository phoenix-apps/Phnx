using System.Collections.Generic;

namespace MarkSFrancis.Extensions
{
    public static class ErrorFactoryExtensions
    {
        public static KeyNotFoundException KeyNotFound(this ErrorFactory factory, string key)
        {
            return new KeyNotFoundException($"The key \"{key}\" was not found in the given collection");
        }

        public static KeyNotFoundException KeyNotFound(this ErrorFactory factory, string key, string collectionName)
        {
            return new KeyNotFoundException($"The key \"{key}\" was not found in the given collection \"{collectionName}\"");
        }
    }
}
