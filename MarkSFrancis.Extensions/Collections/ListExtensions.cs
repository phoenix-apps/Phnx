using System;
using System.Collections.Generic;

namespace MarkSFrancis.Extensions.Collections
{
    public static class ListExtensions
    {
        public static void InsertRange<T>(this List<T> items, int startIndex, IEnumerable<T> itemsToInsert)
        {
            InsertRange(items, startIndex, itemsToInsert, null);
        }

        public static void InsertRange<T>(this List<T> items, int startIndex, IEnumerable<T> itemsToInsert, int numberToInsert)
        {
            InsertRange(items, startIndex, itemsToInsert, (int?)numberToInsert);
        }

        private static void InsertRange<T>(List<T> items, int startIndex, IEnumerable<T> itemsToInsert, int? numberToInsert)
        {
            Queue<T> buffer;
            if (numberToInsert.HasValue)
            {
                buffer = new Queue<T>(numberToInsert.Value);
            }
            else
            {
                buffer = new Queue<T>();
            }

            var valuesInserted = 0;

            using (var enumerator = itemsToInsert.GetEnumerator())
            {
                for (var index = startIndex; !numberToInsert.HasValue || index < numberToInsert; index++)
                {
                    if (!enumerator.MoveNext())
                    {
                        if (numberToInsert.HasValue)
                        {
                            throw new IndexOutOfRangeException();
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (index < items.Count)
                    {
                        buffer.Enqueue(items[index]);
                        items[index] = enumerator.Current;
                    }
                    else
                    {
                        items.Add(enumerator.Current);
                    }

                    ++valuesInserted;
                }
            }
            
            for (var curIndex = startIndex + valuesInserted; curIndex < items.Count; curIndex++)
            {
                // Move legacy items forward in list

                buffer.Enqueue(items[curIndex]);
                items[curIndex] = buffer.Dequeue();
            }

            while (buffer.Count > 0)
            {
                items.Add(buffer.Dequeue());
            }
        }
    }
}