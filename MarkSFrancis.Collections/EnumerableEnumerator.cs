using System.Collections;
using System.Collections.Generic;

namespace MarkSFrancis.Collections
{
    public class EnumerableEnumerator<T> : IEnumerable<T>
    {
        public EnumerableEnumerator(IEnumerator<T> enumerator)
        {
            Enumerator = enumerator;
        }

        public IEnumerator<T> Enumerator { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return Enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerator;
        }
    }

    public class EnumerableEnumerator : IEnumerable
    {
        public EnumerableEnumerator(IEnumerator enumerator)
        {
            Enumerator = enumerator;
        }

        public IEnumerator Enumerator { get; set; }

        public IEnumerator GetEnumerator()
        {
            return Enumerator;
        }
    }

    /// <summary>
    /// Extensions for <see cref="IEnumerator{T}"/>
    /// </summary>
    public static class IEnumeratorExtensions
    {
        public static EnumerableEnumerator<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            return new EnumerableEnumerator<T>(enumerator);
        }
    }
}