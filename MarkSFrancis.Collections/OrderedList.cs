using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MarkSFrancis.Collections
{
    public class OrderedList<T> : ICollection<T> where T : IComparable<T>
    {
        private const int DefaultCapacity = 4;
        private readonly List<T> _items;
        private Comparison<T> _orderedBy;

        public Comparison<T> OrderedBy
        {
            get => this._orderedBy;
            set
            {
                if (value == null)
                {
                    throw ErrorFactory.Default.ArgumentNull(nameof(value));
                }
                _items.Sort(value);
                _orderedBy = value;
            }
        }

        public OrderedList()
        {
            _orderedBy = (x, y) => x.CompareTo(y);
            _items = new List<T>(DefaultCapacity);
        }

        public OrderedList(Comparison<T> sortFunc)
        {
            _orderedBy = sortFunc;
            _items = new List<T>();
        }

        public OrderedList(int capacity)
        {
            _items = new List<T>(capacity);
        }

        public OrderedList(int capacity, Comparison<T> sortFunc)
        {
            _items = new List<T>(capacity);
            _orderedBy = sortFunc;
        }

        public T this[int index]
        {
            get => _items[index];
            private set => _items[index] = value;
        }

        public int Count => _items.Count;

        public bool IsReadOnly => false;

        public void Add(T value)
        {
            if (Count > 0)
            {
                if (value == null)
                {
                    _items.Insert(0, value);
                    return;
                }

                T compareTo = _items[_items.Count - 1];
                if (compareTo == null || _orderedBy(value, compareTo) >= 0)
                {
                    _items.Add(value);
                    return;
                }
            }

            int index = IndexOf(value);

            if (index < 0)
            {
                if (~index == Count)
                {
                    _items.Add(value);
                    return;
                }
                _items.Insert(~index, value);
            }
            else
            {
                _items.Insert(index, value);
            }
        }

        public void AddRange(IEnumerable<T> values)
        {
            List<Tuple<int, T>> valueBuffer;

            // Get where in the list to insert each new value
            {
                var newValues = values.ToList();
                newValues.Sort();

                valueBuffer = new List<Tuple<int, T>>(newValues.Count);

                foreach (T value in newValues)
                {
                    int insertAt = this._items.BinarySearch(value);
                    if (insertAt < 0)
                        insertAt = ~insertAt;

                    valueBuffer.Add(new Tuple<int, T>(insertAt, value));
                }
            }

            int valuesInsertedCount = 0;
            LinkedQueue<T> oldValuesQueue = new LinkedQueue<T>();
            int expectedFinalCount = valueBuffer.Count + Count;

            for (int index = 0; index < expectedFinalCount; index++)
            {
                if (valueBuffer.Count > 0)
                {
                    if (valuesInsertedCount < valueBuffer.Count)
                    {
                        while (valueBuffer[valuesInsertedCount].Item1 == index)
                        {
                            oldValuesQueue.Enqueue(valueBuffer[valuesInsertedCount].Item2);

                            valuesInsertedCount++;

                            if (valuesInsertedCount == valueBuffer.Count)
                                break;
                        }
                    }

                    if (index < Count)
                    {
                        oldValuesQueue.Enqueue(this._items[index]);
                        this._items[index] = oldValuesQueue.Dequeue();
                    }
                    else
                    {
                        this._items.Add(oldValuesQueue.Dequeue());
                    }
                }
            }
        }

        public void AddRange(params T[] values)
        {
            AddRange((IEnumerable<T>)values);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public int IndexOf(T item)
        {
            // Given the ordered by, binary search
            var defaultComparer = EqualityComparer<T>.Default;

            var index = BinarySearch(0, _items.Count - 1);

            while (!defaultComparer.Equals(_items[index], item))
            {
                index++;

                if (OrderedBy(item, _items[index]) != 0)
                {
                    return ~index;
                }
            }

            return index;

            int BinarySearch(int left, int right)
            {
                if (left > right)
                {
                    return -1;
                }

                int mid = (left + right) / 2;

                var orderResult = OrderedBy(_items[mid], item);
                if (orderResult == 0)
                {
                    // Could be more than one match. Check to see if there are any earlier in the array
                    var foundIndex = mid - 1;
                    while (OrderedBy(_items[foundIndex], item) == 0)
                    {
                        --foundIndex;
                    }

                    return ++foundIndex;
                }
                else if (orderResult == 1)
                {
                    return BinarySearch(left, mid - 1);
                }

                return BinarySearch(mid + 1, right);
            }
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index < 0)
            {
                return false;
            }

            _items.RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this);
        }
    }
}