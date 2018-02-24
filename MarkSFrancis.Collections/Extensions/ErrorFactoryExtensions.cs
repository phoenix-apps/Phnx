using System;

namespace MarkSFrancis.Collections.Extensions
{
    public static class ErrorFactoryExtensions
    {
        public static IndexOutOfRangeException CollectionEmpty(this ErrorFactory factory, string paramName)
        {
            return new IndexOutOfRangeException($"{paramName} cannot be an empty collection");
        }
    }
}
