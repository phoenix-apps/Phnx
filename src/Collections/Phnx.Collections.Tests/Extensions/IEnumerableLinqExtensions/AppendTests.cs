using Phnx.Collections.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Collections.Tests.Extensions.IEnumerableLinqExtensions
{
    public class AppendTests
    {
        [Test]
        public void Append_WhenSourceIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;
            var items = new List<int> { 1, 356, 365, 125 };

            Assert.Throws<ArgumentNullException>(() => source.Append(items).ToList());
        }

        [Test]
        public void Append_WhenCollectionsIsNull_ThrowsArgumentNullException()
        {
            // new List<List<int>> { new List<int> { 1, 356, 365, 125 } }
            IEnumerable<int> source = new List<int> { 1, 356, 365, 125 };
            List<List<int>> items = null;

            Assert.Throws<ArgumentNullException>(() => source.Append(items).ToList());
        }

        [Test]
        public void Append_WhenCollectionsIsOneDimension_AddsToSource()
        {
            var expected = new List<int> { 1, 356, 365, 125, 167, 36, 21 };

            IEnumerable<int> source = new List<int> { 1, 356, 365, 125 };
            List<int> items = new List<int> { 167, 36, 21 };

            var result = source.Append(items);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void Append_WhenCollectionsIsThreeDimensions_AddsToSource()
        {
            var expected = new List<int> { 1, 356, 365, 125, 167, 36, 21, 13672, 372165, 234733, 4276, 247 };

            IEnumerable<int> source = new List<int> { 1, 356, 365, 125 };
            var items = new List<List<int>>
            {
                new List<int> { 167, 36, 21 },
                new List<int> { 13672, 372165, 234733, 4276, 247 },
                new List<int> { }
            };

            var result = source.Append(items);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void Append_WhenCollectionsContainsNullChildCollection_SkipsNull()
        {
            var expected = new List<int> { 1, 356, 365, 125, 167, 36, 21, 12, 372165, 234733, 14, 247 };

            IEnumerable<int> source = new List<int> { 1, 356, 365, 125 };
            var items = new List<List<int>>
            {
                new List<int> { 167, 36 },
                new List<int> { 21 },
                null,
                new List<int> { },
                new List<int> { 12, 372165, 234733, 14, 247 },
            };

            var result = source.Append(items);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
