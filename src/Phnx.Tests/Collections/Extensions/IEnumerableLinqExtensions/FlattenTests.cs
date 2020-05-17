using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Collections.Tests.Extensions.IEnumerableLinqExtensions
{
    public class FlattenTests
    {
        [Test]
        public void Append_WhenSourceIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<IEnumerable<int>> source = null;

            Assert.Throws<ArgumentNullException>(() => source.Flatten().ToList());
        }

        [Test]
        public void Append_WhenCollectionContains3Lists_FlattensCollections()
        {
            var expected = new List<int> { 167, 36, 21, 12, 372165, 234733 };
            var items = new List<List<int>>
            {
                new List<int> { 167, 36 },
                new List<int> { 21 },
                new List<int> { 12, 372165, 234733 }
            };

            var result = items.Flatten();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void Append_WhenSubCollectionIsNull_SkipsSubCollection()
        {
            var expected = new List<int> { 167, 36, 21, 12, 372165, 234733, 14, 247 };
            var items = new List<List<int>>
            {
                new List<int> { 167, 36 },
                new List<int> { 21 },
                null,
                new List<int> { },
                new List<int> { 12, 372165, 234733, 14, 247 },
            };

            var result = items.Flatten();

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
