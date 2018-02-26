using System.Collections.Generic;

namespace MarkSFrancis.Collections
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> collection, params IEnumerable<T>[] collections)
        {
            foreach (var value in collection)
            {
                yield return value;
            }

            foreach (var value in Flatten((IEnumerable<IEnumerable<T>>) collections))
            {
                yield return value;
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