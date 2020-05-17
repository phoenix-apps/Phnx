using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Phnx.Collections.Tests.Extensions.IEnumerableLinqExtensions
{
    public class MinByTests
    {
        [Test]
        public void MinBy_WhenCollectionIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> values = null;

            Assert.Throws<ArgumentNullException>(() => values.MinBy(v => v));
        }

        [Test]
        public void MinBy_WhenSelectorIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> values = new List<int>();
            Func<int, int> selector = null;

            Assert.Throws<ArgumentNullException>(() => values.MinBy(selector));
        }

        [Test]
        public void MinBy_WhenCollectionIsEmpty_ThrowsInvalidOperationException()
        {
            IEnumerable<int> values = new List<int>();

            Assert.Throws<InvalidOperationException>(() => values.MinBy(v => v));
        }

        [Test]
        public void MinBy_WhenMinIsAtStart_GetsMin()
        {
            IEnumerable<int> values = new List<int>
            {
                1, 2, 4, 3, 5
            };

            var result = values.MinBy(v => v);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void MinBy_WhenMinIsAtEnd_GetsMin()
        {
            IEnumerable<int> values = new List<int>
            {
                5, 4, 2, 3, 1
            };

            var result = values.MinBy(v => v);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void MinBy_WhenCollectionValuesAreAllSame_GetsMin()
        {
            IEnumerable<int> values = new List<int>
            {
                4, 4, 4, 4
            };

            var result = values.MinBy(v => v);

            Assert.AreEqual(4, result);
        }
    }
}
