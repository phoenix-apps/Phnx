using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Tests.Extensions
{
    public class ListExtensionsTests
    {
        [Test]
        public void InsertList_WhenSourceIsNull_ThrowsArgumentNullException()
        {
            List<int> items = null;
            var itemsToAdd = new List<int> { 1, 2, 3 };

            Assert.Throws<ArgumentNullException>(() => items.InsertList(1, items));
        }

        [Test]
        public void InsertList_WhenItemsToAddAreNull_ThrowsArgumentNullException()
        {
            var items = new List<int> { 1, 2, 3 };
            List<int> itemsToAdd = null;

            Assert.Throws<ArgumentNullException>(() => items.InsertList(1, itemsToAdd));
        }

        [Test]
        public void InsertList_WhenStartIndexLessThan0_ThrowsArgumentLessThanZeroException()
        {
            var items = new List<int> { 1, 2, 3 };
            var itemsToAdd = new List<int> { 4, 5, 6 };

            Assert.Throws<ArgumentLessThanZeroException>(() => items.InsertList(-1, itemsToAdd));
        }

        [Test]
        public void InsertList_WhenStartIndexMoreThanSizeOfCollection_ThrowsArgumentOutOfRangeException()
        {
            var items = new List<int> { 1, 2, 3 };
            var itemsToAdd = new List<int> { 4, 5, 6 };

            Assert.Throws<ArgumentOutOfRangeException>(() => items.InsertList(4, itemsToAdd));
        }

        [Test]
        public void InsertList_ToEnd_AppendsCollection()
        {
            var expected = new List<int> { 1, 2, 3, 4, 5, 6 };

            var items = new List<int> { 1, 2, 3 };
            var itemsToAdd = new List<int> { 4, 5, 6 };

            items.InsertList(items.Count, itemsToAdd);

            CollectionAssert.AreEqual(expected, items);
        }

        [Test]
        public void InsertList_ToStart_PrependsCollection()
        {
            var expected = new List<int> { 4, 5, 6, 1, 2, 3 };

            var items = new List<int> { 1, 2, 3 };
            var itemsToAdd = new List<int> { 4, 5, 6 };

            items.InsertList(0, itemsToAdd);

            CollectionAssert.AreEqual(expected, items);
        }

        [Test]
        public void InsertList_ToMiddle_InsertsCollection()
        {
            var expected = new List<int> { 1, 2, 4, 5, 6, 3 };

            var items = new List<int> { 1, 2, 3 };
            var itemsToAdd = new List<int> { 4, 5, 6 };

            items.InsertList(2, itemsToAdd);

            CollectionAssert.AreEqual(expected, items);
        }

        [Test]
        public void InsertList_ToMiddleShorterThanOriginalCollection_InsertsCollection()
        {
            var expected = new List<int> { 1, 2, 6, 3, 4, 5 };

            var items = new List<int> { 1, 2, 3, 4, 5 };
            var itemsToAdd = new List<int> { 6 };

            items.InsertList(2, itemsToAdd);

            CollectionAssert.AreEqual(expected, items);
        }
    }
}
