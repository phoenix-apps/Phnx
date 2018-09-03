using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Tests.Extensions.IEnumerableLinqExtensions
{
    public class IndexOfLongTests
    {
        [Test]
        public void IndexOfLong_WhenSourceIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;

            Assert.Throws<ArgumentNullException>(() => source.IndexOfLong(_ => true));
        }

        [Test]
        public void IndexOf_WhenIsMatchIsNull_ThrowsArgumentNullException()
        {
            var source = new List<int> { };
            Func<int, bool> isMatch = null;

            Assert.Throws<ArgumentNullException>(() => source.IndexOfLong(isMatch));
        }

        [Test]
        public void IndexOfLong_WhenItemsIsEmpty_ReturnsMinus1()
        {
            var source = new List<int> { };

            Assert.AreEqual(-1, source.IndexOfLong(_ => true));
        }

        [Test]
        public void IndexOfLong_WhenItemsDoesNotContainItem_ReturnsMinus1()
        {
            var source = new List<int> { 6321, 3126312, 2345, 523 };

            Assert.AreEqual(-1, source.IndexOfLong(item => item == 1));
        }

        [Test]
        public void IndexOfLong_WhenItemsDoesContainItem_ReturnsIndex()
        {
            var source = new List<int> { 6321, 3126312, 2345, 523 };

            Assert.AreEqual(1, source.IndexOfLong(item => item == 3126312));
        }

        /*
        [Test]
        public void IndexOfLong_WhenItemsIsTooLong_ThrowsOverflowException()
        {
            // WARNING: THIS TEST TAKES A VERY LONG TIME TO RUN (4 * 9,223,372,036,854,775,807 cycles)
            // This test is purely here for illustrative purposes. Actually running this test would take many years

            var source = new UnlimitedEnumerable<int>(0);

            Assert.Throws<OverflowException>(() => source.IndexOfLong(_ => false));
        }
        */
    }
}
