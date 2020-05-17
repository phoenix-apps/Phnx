using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Collections.Tests.Extensions.IEnumerableLinqExtensions
{
    public class BreadthFirstFlattenTests
    {
        private IEnumerable<string> StringSplitter(string s)
        {
            if (s.Length <= 1)
            {
                yield break;
            }

            int halfway = s.Length / 2;
            yield return s.Substring(0, halfway);
            yield return s.Substring(halfway);
        }

        [Test]
        public void BreadthFirstFlatten_WhenSourceIsNull_ThrowsArgumentNullException()
        {
            List<string> collection = null;

            Assert.Throws<ArgumentNullException>(() => collection.BreadthFirstFlatten(s => new List<string>(0)).ToList());
        }

        [Test]
        public void BreadthFirstFlatten_WhenChildSelectorIsNull_ThrowsArgumentNullException()
        {
            var collection = new List<string>(0);

            Assert.Throws<ArgumentNullException>(() => collection.BreadthFirstFlatten(null).ToList());
        }

        [Test]
        public void BreadthFirstFlatten_WhenSourceIsEmpty_GetsEmpty()
        {
            var expected = new List<string>(0);
            var collection = new List<string>(0);

            var flattened = collection.BreadthFirstFlatten(StringSplitter);

            CollectionAssert.AreEqual(expected, flattened);
        }

        [Test]
        public void BreadthFirstFlatten_WhenSourceIsShallow_GetsShallow()
        {
            var expected = new List<string> { "ab", "a", "b" };
            var collection = new List<string> { "ab" };

            var flattened = collection.BreadthFirstFlatten(StringSplitter);

            CollectionAssert.AreEqual(expected, flattened);
        }

        [Test]
        public void BreadthFirstFlatten_WhenSourceIsDeep_GetsBreadthFirst()
        {
            var expected = new List<string>
            {
                "abcdefgh",
                "abcd", "efgh", "ab", "cd", "ef", "gh",
                "a", "b", "c", "d", "e", "f", "g", "h"
            };

            var collection = new List<string> { "abcdefgh" };

            var flattened = collection.BreadthFirstFlatten(StringSplitter);

            CollectionAssert.AreEqual(expected, flattened);
        }
    }
}
