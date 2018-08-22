using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Tests
{
    public class EnumerableEnumeratorTests
    {
        [Test]
        public void GetEnumerableT_WithType_ReturnsOriginalEnumerator()
        {
            var enumerator = new List<string>().GetEnumerator();

            var enumerableEnumerator = new EnumerableEnumerator<string>(enumerator);

            var result = enumerableEnumerator.GetEnumerator();

            Assert.AreEqual(enumerator, result);
        }

        [Test]
        public void GetEnumerableT_WithoutType_ReturnsOriginalEnumerator()
        {
            var enumerator = new List<string>().GetEnumerator();

            var enumerableEnumerator = new EnumerableEnumerator<string>(enumerator);

            var result = ((IEnumerable)enumerableEnumerator).GetEnumerator();

            Assert.AreEqual(enumerator, result);
        }

        [Test]
        public void GetEnumerable_ReturnsOriginalEnumerator()
        {
            var enumerator = new List<string>().GetEnumerator();

            var enumerableEnumerator = new EnumerableEnumerator(enumerator);

            var result = enumerableEnumerator.GetEnumerator();

            Assert.AreEqual(enumerator, result);
        }
    }
}
