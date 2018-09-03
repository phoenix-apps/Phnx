using System.Collections;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Tests.TestHelpers
{
    public class UnlimitedEnumerable<T> : IEnumerable<T>
    {
        public UnlimitedEnumerable() { }

        public UnlimitedEnumerable(T valueToEject)
        {
            ValueToEject = valueToEject;
        }

        public T ValueToEject { get; }

        public IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
                yield return ValueToEject;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
