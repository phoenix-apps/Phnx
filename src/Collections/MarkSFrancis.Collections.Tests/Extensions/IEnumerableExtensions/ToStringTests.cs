using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Tests.Extensions.IEnumerableExtensions
{
    public class ToStringTests
    {
        [Test]
        public void ToString_WithNullCollection_ThrowsArgumentNullException()
        {
            IEnumerable<int> items = null;

            Assert.Throws<ArgumentNullException>(() => items.ToString(string.Empty));
        }

        [Test]
        public void ToString_WithNullSeperator_ThrowsArgumentNullException()
        {
            var items = new List<int>();

            Assert.Throws<ArgumentNullException>(() => items.ToString(null));
        }

        [Test]
        public void ToString_With0Items_MakesEmptyString()
        {
            var items = new List<int>();

            var result = items.ToString(", ");

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void ToString_With5ItemsAndEmptySeperator_MakesString()
        {
            var expected = "12345";
            var items = new List<int>
            {
                1, 2, 3, 4, 5
            };

            var result = items.ToString(string.Empty);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_With5ItemsAndSeperator_MakesString()
        {
            var expected = "125, 36, 3672, 21, 1";
            var items = new List<int>
            {
                125, 36, 3672, 21, 1
            };

            var result = items.ToString(", ");

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithCustomToString_MakesStringUsingCustom()
        {
            var expected = ", , , , ";

            var items = new List<int>
            {
                125, 36, 3672, 21, 1
            };

            var result = items.ToString(", ", i => string.Empty);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToString_WithCustomToStringThatReturnsNull_ReplacesNullWithEmpty()
        {
            var expected = ", and , and , and , and ";

            var items = new List<int>
            {
                125, 36, 3672, 21, 1
            };

            var result = items.ToString(", and ", i => null);

            Assert.AreEqual(expected, result);
        }
    }
}
