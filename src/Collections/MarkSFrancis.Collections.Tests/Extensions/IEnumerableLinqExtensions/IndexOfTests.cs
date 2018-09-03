using MarkSFrancis.Collections.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Tests.Extensions.IEnumerableLinqExtensions
{
    public class IndexOfTests
    {
        [Test]
        public void IndexOf_WhenSourceIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;

            Assert.Throws<ArgumentNullException>(() => source.IndexOf(_ => true));
        }

        [Test]
        public void IndexOf_WhenIsMatchIsNull_ThrowsArgumentNullException()
        {
            var source = new List<int> { };
            Func<int, bool> isMatch = null;

            Assert.Throws<ArgumentNullException>(() => source.IndexOf(isMatch));
        }

        [Test]
        public void IndexOf_WhenItemsIsEmpty_ReturnsMinus1()
        {
            var source = new List<int> { };

            Assert.AreEqual(-1, source.IndexOf(_ => true));
        }

        [Test]
        public void IndexOf_WhenItemsDoesNotContainItem_ReturnsMinus1()
        {
            var source = new List<int> { 6321, 3126312, 2345, 523 };

            Assert.AreEqual(-1, source.IndexOf(_ => false));
        }

        [Test]
        public void IndexOfLong_WhenItemsDoesContainItem_ReturnsIndex()
        {
            var source = new List<int> { 6321, 3126312, 2345, 523 };

            Assert.AreEqual(1, source.IndexOf(item => item == 3126312));
        }

        /*
        [Test]
        public void IndexOf_WhenItemsIsTooLong_ThrowsOverflowException()
        {
            // WARNING: THIS TEST TAKES A VERY LONG TIME TO RUN (4 * 2,147,483,647 cycles)
            var source = new UnlimitedEnumerable<int>(0);

            Assert.Throws<OverflowException>(() => source.IndexOf(_ => false));
        }
        */
    }
}
