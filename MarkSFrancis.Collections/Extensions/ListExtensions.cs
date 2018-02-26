using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Extensions
{
    /// <summary>
    /// Extensions for <see cref="List{T}"/>
    /// </summary>
    public static class ListExtensions
    {
        public static void InsertRange<T>(this List<T> values, int startIndex, IEnumerable<T> itemsToInsert)
        {
            InsertRange(values, startIndex, itemsToInsert, null);
        }

        public static void InsertRange<T>(this List<T> values, int startIndex, IEnumerable<T> itemsToInsert, int numberToInsert)
        {
            InsertRange(values, startIndex, itemsToInsert, (int?)numberToInsert);
        }

        private static void InsertRange<T>(List<T> values, int startIndex, IEnumerable<T> itemsToInsert, int? numberToInsert)
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

                    if (index < values.Count)
                    {
                        buffer.Enqueue(values[index]);
                        values[index] = enumerator.Current;
                    }
                    else
                    {
                        values.Add(enumerator.Current);
                    }

                    ++valuesInserted;
                }
            }
            
            for (var curIndex = startIndex + valuesInserted; curIndex < values.Count; curIndex++)
            {
                // Move legacy values forward in list

                buffer.Enqueue(values[curIndex]);
                values[curIndex] = buffer.Dequeue();
            }

            while (buffer.Count > 0)
            {
                values.Add(buffer.Dequeue());
            }
        }
    }
}