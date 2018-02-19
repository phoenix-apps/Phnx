using System.Collections;
using System.Collections.Generic;

namespace MarkSFrancis.Collections
{
    public class LinkedQueue<T> : IEnumerable<T>
    {
        private readonly LinkedList<T> items;

        public int Count => items.Count;

        public LinkedQueue()
        {
            items = new LinkedList<T>();
        }

        public LinkedQueue(IEnumerable<T> collection)
        {
            this.items = new LinkedList<T>(collection);
        }

        public void Enqueue(T value)
        {
            items.AddLast(value);
        }

        public T Dequeue()
        {
            T first = items.First.Value;
            items.RemoveFirst();
            return first;
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}