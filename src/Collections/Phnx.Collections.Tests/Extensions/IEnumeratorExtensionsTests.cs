using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Phnx.Collections.Tests.Extensions
{
    public class IEnumeratorExtensionsTests
    {
        [Test]
        public void ToIEnumerableT_WhenIEnumeratorIsNull_ThrowsArgumentNullException()
        {
            IEnumerator<int> enumerator = null;

            Assert.Throws<ArgumentNullException>(() => enumerator.ToEnumerable());
        }

        [Test]
        public void ToIEnumerable_WhenIEnumeratorIsNull_ThrowsArgumentNullException()
        {
            IEnumerator enumerator = null;

            Assert.Throws<ArgumentNullException>(() => enumerator.ToEnumerable());
        }

        [Test]
        public void ToIEnumerableT_WhenIEnumeratorIsEmpty_GetsEmptyIEnumerable()
        {
            var collection = new List<int> { };
            IEnumerator<int> enumerator = collection.GetEnumerator();

            var result = enumerator.ToEnumerable();

            CollectionAssert.AreEqual(collection, result);
        }

        [Test]
        public void ToIEnumerable_WhenIEnumeratorIsEmpty_GetsEmptyIEnumerable()
        {
            var collection = new List<int> { };
            IEnumerator enumerator = collection.GetEnumerator();

            var result = enumerator.ToEnumerable();

            CollectionAssert.AreEqual(collection, result);
        }

        [Test]
        public void ToIEnumerableT_WhenIEnumeratorContainsItems_GetsMatchingIEnumerable()
        {
            var collection = new List<int> { };
            IEnumerator<int> enumerator = collection.GetEnumerator();

            var result = enumerator.ToEnumerable();

            CollectionAssert.AreEqual(collection, result);
        }

        [Test]
        public void ToIEnumerable_WhenIEnumeratorContainsItems_GetsMatchingIEnumerable()
        {
            var collection = new List<int> { 1256, 3261, 136, 125 };
            IEnumerator enumerator = collection.GetEnumerator();

            var result = enumerator.ToEnumerable();

            CollectionAssert.AreEqual(collection, result);
        }
    }
}
