using System.Collections.Generic;

namespace MarkSFrancis.Collections
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> collection, params IEnumerable<T>[] collections)
        {
            foreach (var item in collection)
            {
                yield return item;
            }

            foreach (var item in Flatten((IEnumerable<IEnumerable<T>>) collections))
            {
                yield return item;
            }
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