using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Phnx.Collections.Tests.Extensions.IEnumerableLinqExtensions
{
    public class MaxByTests
    {
        [Test]
        public void MaxBy_WhenCollectionIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> values = null;

            Assert.Throws<ArgumentNullException>(() => values.MaxBy(v => v));
        }

        [Test]
        public void MaxBy_WhenSelectorIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> values = new List<int>();
            Func<int, int> selector = null;

            Assert.Throws<ArgumentNullException>(() => values.MaxBy(selector));
        }

        [Test]
        public void MaxBy_WhenCollectionIsEmpty_ThrowsInvalidOperationException()
        {
            IEnumerable<int> values = new List<int>();

            Assert.Throws<InvalidOperationException>(() => values.MaxBy(v => v));
        }

        [Test]
        public void MaxBy_WhenMaxIsAtStart_GetsMax()
        {
            IEnumerable<int> values = new List<int>
            {
                5, 4, 2, 3, 1
            };

            var result = values.MaxBy(v => v);

            Assert.AreEqual(5, result);
        }

        [Test]
        public void MaxBy_WhenMaxIsAtEnd_GetsMax()
        {
            IEnumerable<int> values = new List<int>
            {
                1, 2, 4, 3, 5
            };

            var result = values.MaxBy(v => v);

            Assert.AreEqual(5, result);
        }

        [Test]
        public void MaxBy_WhenCollectionValuesAreAllSame_GetsMax()
        {
            IEnumerable<int> values = new List<int>
            {
                4, 4, 4, 4
            };

            var result = values.MaxBy(v => v);

            Assert.AreEqual(4, result);
        }
    }
}
