using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Collections.Tests.Extensions.IEnumerableLinqExtensions
{
    public class DepthFirstFlattenTests
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
        public void DepthFirstFlatten_WhenSourceIsNull_ThrowsArgumentNullException()
        {
            List<string> collection = null;

            Assert.Throws<ArgumentNullException>(() => collection.DepthFirstFlatten(s => new List<string>(0)).ToList());
        }

        [Test]
        public void DepthFirstFlatten_WhenChildSelectorIsNull_ThrowsArgumentNullException()
        {
            var collection = new List<string>(0);

            Assert.Throws<ArgumentNullException>(() => collection.DepthFirstFlatten(null).ToList());
        }

        [Test]
        public void DepthFirstFlatten_WhenSourceIsEmpty_GetsEmpty()
        {
            var expected = new List<string>(0);
            var collection = new List<string>(0);

            var flattened = collection.DepthFirstFlatten(StringSplitter);

            CollectionAssert.AreEqual(expected, flattened);
        }

        [Test]
        public void DepthFirstFlatten_WhenSourceIsShallow_GetsShallow()
        {
            var expected = new List<string> { "ab", "a", "b" };
            var collection = new List<string> { "ab" };

            var flattened = collection.DepthFirstFlatten(StringSplitter);

            CollectionAssert.AreEqual(expected, flattened);
        }

        [Test]
        public void DepthFirstFlatten_WhenSourceIsDeep_GetsDepthFirst()
        {
            var expected = new List<string>
            {
                "abcdefgh",
                "abcd", "ab", "a", "b", "cd", "c", "d",
                "efgh", "ef", "e", "f", "gh", "g", "h"
            };

            var collection = new List<string> { "abcdefgh" };

            var flattened = collection.DepthFirstFlatten(StringSplitter);

            CollectionAssert.AreEqual(expected, flattened);
        }
    }
}
