using Phnx.Collections.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Collections.Tests.Extensions.IEnumerableExtensions
{
    public class CopyRangeToEndTests
    {
        [Test]
        public void CopyRange_WithNullValues_ThrowsArgumentNullException()
        {
            List<string> items = null;

            Assert.Throws<ArgumentNullException>(() => items.CopyRange(0).ToList());
        }

        [Test]
        public void CopyRange_WithEmptyCollection_MakesEmptyCollection()
        {
            var items = new List<string>();

            var results = items.CopyRange(0);

            CollectionAssert.IsEmpty(results);
        }

        [Test]
        public void CopyRange_StartingAt0_CopiesCollection()
        {
            var expected = new List<string>
            {
                "item1",
                "item2",
                "item3"
            };

            var items = new List<string>
            {
                "item1",
                "item2",
                "item3"
            };

            var results = items.CopyRange(0);

            CollectionAssert.AreEqual(expected, results);
        }

        [Test]
        public void CopyRange_StartingAtEnd_CreatesEmptyCollection()
        {
            var expected = new List<string>
            {
            };

            var items = new List<string>
            {
                "item1",
                "item2",
                "item3"
            };

            var results = items.CopyRange(3);

            CollectionAssert.AreEqual(expected, results);
        }

        [Test]
        public void CopyRange_StartingLessThanZero_ThrowsArgumentLessThanZeroException()
        {
            var items = new List<string>
            {
                "item1",
                "item2",
                "item3"
            };

            Assert.Throws<ArgumentLessThanZeroException>(() => items.CopyRange(-1).ToList());
        }

        [Test]
        public void CopyRange_StartingAfterEnd_ThrowsArgumentOutOfRangeException()
        {
            var items = new List<string>
            {
                "item1",
                "item2",
                "item3"
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => items.CopyRange(4).ToList());
        }
    }
}
