using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Extensions
{
    /// <summary>
    /// Extensions for <see cref="List{T}"/>
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Insert a range of values into a collection, increasing the size of the collection to accomodate for the new values
        /// </summary>
        /// <typeparam name="T">The type of values in the collection</typeparam>
        /// <param name="values">The values in the original collection</param>
        /// <param name="startIndex">The index at which inserting into <paramref name="values"/> begins</param>
        /// <param name="valuesToInsert">The values to insert into the collection</param>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> or <paramref name="valuesToInsert"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero</exception>
        public static void InsertRange<T>(this List<T> values, int startIndex, IEnumerable<T> valuesToInsert)
        {
            if (values == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(values));
            }
            if (valuesToInsert == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(valuesToInsert));
            }
            if (startIndex < 0)
            {
                throw ErrorFactory.Default.ArgumentLessThanZero(nameof(startIndex));
            }

            var replacedValues = new Queue<T>();

            var valuesInserted = 0;
            var index = startIndex;
            foreach (var valueToInsert in valuesToInsert)
            {
                if (index < values.Count)
                {
                    replacedValues.Enqueue(values[index]);
                    values[index] = valueToInsert;
                }
                else
                {
                    values.Add(valueToInsert);
                }

                ++valuesInserted;
            }

            for (var curIndex = startIndex + valuesInserted; curIndex < values.Count; ++curIndex)
            {
                // Restore replaced values at the end of the inserted values
                replacedValues.Enqueue(values[curIndex]);
                values[curIndex] = replacedValues.Dequeue();
            }

            while (replacedValues.Count > 0)
            {
                // Restore replaced values at the end of the collection
                values.Add(replacedValues.Dequeue());
            }
        }
    }
}