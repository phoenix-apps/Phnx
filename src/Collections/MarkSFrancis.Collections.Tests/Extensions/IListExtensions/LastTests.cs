using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Tests.Extensions.IListExtensions
{
    public class LastTests
    {
        [Test]
        public void Last_WhenSourceIsNull_ThrowsArgumentNullException()
        {
            IList<int> items = null;

            Assert.Throws<ArgumentNullException>(() => items.Last());
        }

        [Test]
        public void Last_WhenSourceIsEmpty_ThrowsInvalidOperationException()
        {
            IList<int> items = new List<int> { };

            Assert.Throws<InvalidOperationException>(() => items.Last());
        }

        [Test]
        public void Last_WhenSourceContains1Item_ReturnsItem()
        {
            const int expected = 1;
            IList<int> items = new List<int> { expected };

            var result = items.Last();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Last_WhenSourceContains5Items_ReturnsLastItem()
        {
            const int expected = 23;
            IList<int> items = new List<int> { 167, 1361, 136, 1, expected };

            var result = items.Last();

            Assert.AreEqual(expected, result);
        }
    }
}
