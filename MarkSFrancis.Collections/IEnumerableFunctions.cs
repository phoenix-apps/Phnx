using System.Collections.Generic;

namespace MarkSFrancis.Collections
{
    public static class IEnumerableFunctions
    {
        public static IEnumerable<T> Flatten<T>(params IEnumerable<T>[] collections)
        {
            return Flatten((IEnumerable<IEnumerable<T>>)collections);
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> collections)
        {
            foreach (var collection in collections)
            {
                foreach (var value in collection)
                {
                    yield return value;
                }
            }
        }
    }
}