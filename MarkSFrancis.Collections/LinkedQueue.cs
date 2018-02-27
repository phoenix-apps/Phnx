using System;
using System.Collections;
using System.Collections.Generic;

namespace MarkSFrancis.Collections
{
    /// <summary>
    /// A more efficient queue for quicker removing entries from the start of the <see cref="LinkedList{T}"/> underneath than a standard <see cref="Queue{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedQueue<T> : IReadOnlyCollection<T>, ICollection
    {
        private readonly LinkedList<T> values;

        /// <summary>
        /// Gets the number of nodes contained in the <see cref="LinkedQueue{T}"/>
        /// </summary>
        /// <returns>The number of nodes contained in the <see cref="LinkedQueue{T}"/></returns>
        public int Count => values.Count;

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="ICollection"/> is synchronized (thread safe)
        /// </summary>
        /// <returns><see langword="true"/> if access to the <see cref="ICollection"/> is synchronized (thread safe). Otherwise, <see langword="false"/></returns>
        bool ICollection.IsSynchronized => ((ICollection)values).IsSynchronized;

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="ICollection"/>
        /// </summary>
        /// <returns>An object that can be used to synchronize access to the <see cref="ICollection"></see></returns>
        object ICollection.SyncRoot => ((ICollection)values).SyncRoot;

        /// <summary>
        /// Create a new empty <see cref="LinkedQueue{T}"/>
        /// </summary>
        public LinkedQueue()
        {
            values = new LinkedList<T>();
        }

        /// <summary>
        /// Create a new <see cref="LinkedQueue{T}"/> from a collection of values
        /// </summary>
        /// <param name="collection">The collection of values to fill the <see cref="LinkedQueue{T}"/> with</param>
        public LinkedQueue(IEnumerable<T> collection)
        {
            values = new LinkedList<T>(collection);
        }

        /// <summary>
        /// Adds an object to the end of the <see cref="LinkedQueue{T}"/>
        /// </summary>
        /// <param name="value">The object to add to the <see cref="T:System.Collections.Generic.Queue`1"></see>. The value can be null for reference types.</param>
        public void Enqueue(T value)
        {
            values.AddLast(value);
        }

        /// <summary>
        /// Removes and returns the object at the beginning of the <see cref="LinkedQueue{T}"/></summary>
        /// <returns>The object that is removed from the beginning of the <see cref="LinkedQueue{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">The <see cref="LinkedQueue{T}"></see> is empty.</exception>
        public T Dequeue()
        {
            T first = values.First.Value;
            values.RemoveFirst();
            return first;
        }

        /// <summary>
        /// Removes all values from the <see cref="LinkedQueue{T}"/>
        /// </summary>
        public void Clear()
        {
            values.Clear();
        }

        /// <summary>
        /// Determines whether a value is in the <see cref="LinkedQueue{T}"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(T value)
        {
            return values.Contains(value);
        }

        /// <summary>
        /// Copies the elements of the <see cref="ICollection"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ICollection"/>. The <see cref="Array"/> must have zero-based indexing</param>
        /// <param name="index">The zero-based index in array at which copying begins</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.</exception>
        /// <exception cref="ArgumentException"><paramref name="array"/> is multidimensional, the number of elements in the source <see cref="ICollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>, or the type of the source <see cref="ICollection"/> cannot be cast automatically to the type of the destination <paramref name="array"/></exception>
        public void CopyTo(Array array, int index)
        {
            ((ICollection)values).CopyTo(array, index);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="LinkedQueue{T}"/>
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> for the <see cref="LinkedQueue{T}"/></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="LinkedQueue{T}"/>
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> for the <see cref="LinkedQueue{T}"/></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }
    }
}