using System.Collections;
using System.Collections.Generic;

namespace MarkSFrancis.Collections
{
    public class LinkedQueue<T> : IEnumerable<T>
    {
        private readonly LinkedList<T> values;

        public int Count => values.Count;

        public LinkedQueue()
        {
            values = new LinkedList<T>();
        }

        public LinkedQueue(IEnumerable<T> collection)
        {
            values = new LinkedList<T>(collection);
        }

        public void Enqueue(T value)
        {
            values.AddLast(value);
        }

        public T Dequeue()
        {
            T first = values.First.Value;
            values.RemoveFirst();
            return first;
        }

        public void Clear()
        {
            values.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }
    }
}