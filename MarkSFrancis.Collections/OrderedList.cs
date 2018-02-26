using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MarkSFrancis.Collections
{
    public class OrderedList<T> : ICollection<T> where T : IComparable<T>
    {
        private const int DefaultCapacity = 4;
        private readonly List<T> _values;
        private Comparison<T> _orderedBy;

        public Comparison<T> OrderedBy
        {
            get => _orderedBy;
            set
            {
                if (value == null)
                {
                    throw ErrorFactory.Default.ArgumentNull(nameof(value));
                }
                _values.Sort(value);
                _orderedBy = value;
            }
        }

        public OrderedList()
        {
            _orderedBy = (x, y) => x.CompareTo(y);
            _values = new List<T>(DefaultCapacity);
        }

        public OrderedList(Comparison<T> sortFunc)
        {
            _orderedBy = sortFunc;
            _values = new List<T>();
        }

        public OrderedList(int capacity)
        {
            _values = new List<T>(capacity);
        }

        public OrderedList(int capacity, Comparison<T> sortFunc)
        {
            _values = new List<T>(capacity);
            _orderedBy = sortFunc;
        }

        public T this[int index]
        {
            get => _values[index];
            private set => _values[index] = value;
        }

        public int Count => _values.Count;

        public bool IsReadOnly => false;

        public void Add(T value)
        {
            if (Count > 0)
            {
                if (value == null)
                {
                    _values.Insert(0, value);
                    return;
                }

                T compareTo = _values[_values.Count - 1];
                if (compareTo == null || _orderedBy(value, compareTo) >= 0)
                {
                    _values.Add(value);
                    return;
                }
            }

            int index = IndexOf(value);

            if (index < 0)
            {
                if (~index == Count)
                {
                    _values.Add(value);
                    return;
                }
                _values.Insert(~index, value);
            }
            else
            {
                _values.Insert(index, value);
            }
        }

        public void AddRange(IEnumerable<T> values)
        {
            List<KeyValuePair<int, T>> valueBuffer;

            // Get where in the list to insert each new value
            {
                var newValues = values.ToList();
                newValues.Sort();

                valueBuffer = new List<KeyValuePair<int, T>>(newValues.Count);

                foreach (T value in newValues)
                {
                    int insertAt = _values.BinarySearch(value);
                    if (insertAt < 0)
                        insertAt = ~insertAt;

                    valueBuffer.Add(new KeyValuePair<int, T>(insertAt, value));
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
                        while (valueBuffer[valuesInsertedCount].Key == index)
                        {
                            oldValuesQueue.Enqueue(valueBuffer[valuesInsertedCount].Value);

                            valuesInsertedCount++;

                            if (valuesInsertedCount == valueBuffer.Count)
                                break;
                        }
                    }

                    if (index < Count)
                    {
                        oldValuesQueue.Enqueue(_values[index]);
                        _values[index] = oldValuesQueue.Dequeue();
                    }
                    else
                    {
                        _values.Add(oldValuesQueue.Dequeue());
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
            _values.Clear();
        }

        public bool Contains(T value)
        {
            return IndexOf(value) >= 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _values.CopyTo(array, arrayIndex);
        }

        public int IndexOf(T value)
        {
            // binary search using the ordered by function
            var defaultComparer = EqualityComparer<T>.Default;

            var index = BinarySearch(0, _values.Count - 1);

            while (!defaultComparer.Equals(_values[index], value))
            {
                index++;

                if (OrderedBy(value, _values[index]) != 0)
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

                var orderResult = OrderedBy(_values[mid], value);
                if (orderResult == 0)
                {
                    // Could be more than one match. Check to see if there are any earlier in the array
                    var foundIndex = mid - 1;
                    while (OrderedBy(_values[foundIndex], value) == 0)
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

        public bool Remove(T value)
        {
            int index = IndexOf(value);
            if (index < 0)
            {
                return false;
            }

            _values.RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            _values.RemoveAt(index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this);
        }
    }
}