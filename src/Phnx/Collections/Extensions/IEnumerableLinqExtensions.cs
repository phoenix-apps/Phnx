using System;
using System.Collections.Generic;

namespace Phnx.Collections
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/> related to querying collections
    /// </summary>
    public static class IEnumerableLinqExtensions
    {
        /// <summary>Gets unique elements from a collection according to the key selector</summary>
        /// <param name="source">The collection to get distinct elements from</param>
        /// <param name="keySelector">A function to extract the key for each value</param>
        /// <typeparam name="TSource">The type of the elements of source</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/></typeparam>
        /// <returns>Unique elements by the <paramref name="keySelector"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null</exception>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            else if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            HashSet<TKey> knownDistincts = new HashSet<TKey>();

            foreach (var item in source)
            {
                var key = keySelector(item);
                if (knownDistincts.Contains(key))
                {
                    continue;
                }

                yield return item;
                knownDistincts.Add(key);
            }
        }

        /// <summary>
        /// Get the value from a collection of values with the maximum value by a key
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <typeparam name="TKey">The type of the key to use for comparison</typeparam>
        /// <param name="source">The collection of values to get the maximum value from</param>
        /// <param name="keySelector">The selector to get the key to use for comparing values</param>
        /// <returns>The value from the collection with the maximum key value</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null or <paramref name="keySelector"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> was empty</exception>
        public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            else if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            T curMax = default;
            TKey curMaxValue = default;
            bool firstRun = true;

            foreach (var value in source)
            {
                if (firstRun)
                {
                    curMax = value;
                    curMaxValue = keySelector(value);
                    firstRun = false;
                    continue;
                }

                TKey valueMaxBy = keySelector(value);
                if (valueMaxBy.CompareTo(curMaxValue) == 1)
                {
                    // New max
                    curMax = value;
                    curMaxValue = valueMaxBy;
                }
            }

            if (firstRun)
            {
                throw new InvalidOperationException(ErrorMessage.Factory.SequenceEmpty(nameof(source)));
            }

            return curMax;
        }

        /// <summary>
        /// Get the value from a collection of values with the minimum value by a key
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <typeparam name="TKey">The type of the key to use for comparison</typeparam>
        /// <param name="source">The collection of values to get the minimum value from</param>
        /// <param name="keySelector">The selector to get the key to use for comparing values</param>
        /// <returns>The value from the collection with the minimum key value</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null or <paramref name="keySelector"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> was empty</exception>
        public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            else if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            T curMin = default;
            TKey curMinValue = default;
            bool firstRun = true;

            foreach (var value in source)
            {
                if (firstRun)
                {
                    curMin = value;
                    curMinValue = keySelector(value);
                    firstRun = false;
                    continue;
                }

                TKey valueMinBy = keySelector(value);
                if (valueMinBy.CompareTo(curMinValue) == -1)
                {
                    // New max
                    curMin = value;
                    curMinValue = valueMinBy;
                }
            }

            if (firstRun)
            {
                throw new InvalidOperationException(nameof(source));
            }

            return curMin;
        }

        /// <summary>
        /// Converts this collection to a <see cref="List{T}"/> with a given default capacity. Useful if you know how many values will be in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <param name="source">The collection of values</param>
        /// <param name="capacity">The capacity to assign to the new <see cref="List{T}"/></param>
        /// <returns>A list of values as a copy of an <see cref="IEnumerable{T}"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is less than 0</exception>
        public static List<T> ToList<T>(this IEnumerable<T> source, int capacity)
        {
            var newList = new List<T>(capacity);
            newList.AddRange(source);

            return newList;
        }

        /// <summary>
        /// Append multiple collections of <see cref="IEnumerable{T}"/> onto a single collection
        /// </summary>
        /// <typeparam name="T">The type of values in the collections</typeparam>
        /// <param name="source">The collection to extend</param>
        /// <param name="collections">The collections to extend onto the <paramref name="source"/>. If any of these collections are <see langword="null"/>, they are treated as empty, and skipped</param>
        /// <returns>A collection containing all given collection's values</returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, params IEnumerable<T>[] collections)
        {
            return source.Append((IEnumerable<IEnumerable<T>>)collections);
        }

        /// <summary>
        /// Append multiple collections of <see cref="IEnumerable{T}"/> onto a single collection
        /// </summary>
        /// <typeparam name="T">The type of values in the collections</typeparam>
        /// <param name="source">The collection to extend</param>
        /// <param name="collections">The collections to extend onto the <paramref name="source"/>. If any of these collections are <see langword="null"/>, they are treated as empty, and skipped</param>
        /// <returns>A collection containing all given collection's values</returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, IEnumerable<IEnumerable<T>> collections)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (collections is null)
            {
                throw new ArgumentNullException(nameof(collections));
            }

            foreach (var value in source)
            {
                yield return value;
            }

            foreach (var value in collections.Flatten())
            {
                yield return value;
            }
        }

        /// <summary>
        /// Flatten a two dimensional collection into a single one dimensional collection
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <param name="source">The collection to flatten. If any collections within <paramref name="source"/> are <see langword="null"/>, they are skipped</param>
        /// <returns>A one dimensional collection containing all the two dimensional collection's values</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (var subCollection in source)
            {
                if (subCollection is null)
                {
                    // Skip
                    continue;
                }

                foreach (var value in subCollection)
                {
                    yield return value;
                }
            }
        }

        /// <summary>
        /// Flatten a tree into a single one dimensional collection, using a selector to get the children of <typeparamref name="T"/>, traversing down the tree before traversing across the tree
        /// </summary>
        /// <typeparam name="T">The type of items in the tree</typeparam>
        /// <param name="source">The tree source</param>
        /// <param name="childSelector">The selector to get children of the current item</param>
        /// <remarks>This method is non-recursive</remarks>
        /// <returns>A flattened tree, with the deepest elements of the first item first</returns>
        public static IEnumerable<T> DepthFirstFlatten<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (childSelector is null)
            {
                throw new ArgumentNullException(nameof(childSelector));
            }

            var list = new LinkedList<T>(source);

            while (list.Count > 0)
            {
                var item = list.First.Value;
                yield return item;
                list.RemoveFirst();

                var node = list.First;
                foreach (var child in childSelector(item))
                {
                    if (node is null)
                    {
                        // List is empty, add to list
                        list.AddLast(child);
                    }
                    else
                    {
                        list.AddBefore(node, child);
                    }
                }
            }
        }

        /// <summary>
        /// Flatten a tree into a single one dimensional collection, using a selector to get the children of <typeparamref name="T"/>, traversing across the tree before traversing down the tree
        /// </summary>
        /// <typeparam name="T">The type of items in the tree</typeparam>
        /// <param name="source">The tree source</param>
        /// <param name="childSelector">The selector to get children of the current item</param>
        /// <returns>A flattened tree, with the children of the topmost elements first</returns>
        public static IEnumerable<T> BreadthFirstFlatten<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (childSelector is null)
            {
                throw new ArgumentNullException(nameof(childSelector));
            }

            var queue = new Queue<T>(source);

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                yield return item;

                foreach (var child in childSelector(item))
                {
                    // Prepare child for traversal after traversing current children
                    queue.Enqueue(child);
                }
            }
        }
    }
}
