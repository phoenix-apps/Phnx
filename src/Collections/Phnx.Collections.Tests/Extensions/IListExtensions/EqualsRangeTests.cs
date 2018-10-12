using NUnit.Framework;
using System.Collections.Generic;

namespace Phnx.Collections.Tests.Extensions.IListExtensions
{
    public class EqualsRangeTests
    {
        [Test]
        public void EqualsRange_WhenSourceIsNull_ReturnsFalse()
        {
            IList<int> source = null;
            IList<int> rangeToCompare = new List<int> { 1, 252, 1 };

            Assert.IsFalse(source.EqualsRange(rangeToCompare));
        }

        [Test]
        public void EqualsRange_WhenRangeToCompareIsNull_ReturnsFalse()
        {
            IList<int> source = new List<int> { 1, 252, 1 };
            IList<int> rangeToCompare = null;

            Assert.IsFalse(source.EqualsRange(rangeToCompare));
        }

        [Test]
        public void EqualsRange_WhenSourceAndRangeToCompareAreNull_ReturnsFalse()
        {
            IList<int> source = null;
            IList<int> rangeToCompare = null;

            Assert.IsFalse(source.EqualsRange(rangeToCompare));
        }

        [Test]
        public void EqualsRange_WhenCountIsDifferent_ReturnsFalse()
        {
            IList<int> source = new List<int> { 1, 252, 1 };
            IList<int> rangeToCompare = new List<int> { 1, 252 };

            Assert.IsFalse(source.EqualsRange(rangeToCompare));
            Assert.IsFalse(rangeToCompare.EqualsRange(source));
        }

        [Test]
        public void EqualsRange_WhenItemsAreDifferent_ReturnsFalse()
        {
            IList<int> source = new List<int> { 1, 251, 1 };
            IList<int> rangeToCompare = new List<int> { 1, 252, 1 };

            Assert.IsFalse(source.EqualsRange(rangeToCompare));
            Assert.IsFalse(rangeToCompare.EqualsRange(source));
        }

        [Test]
        public void EqualsRange_WhenItemsAreSame_ReturnsTrue()
        {
            IList<int> source = new List<int> { 1, 251, 1 };
            IList<int> rangeToCompare = new List<int> { 1, 251, 1 };

            Assert.IsTrue(source.EqualsRange(rangeToCompare));
            Assert.IsTrue(rangeToCompare.EqualsRange(source));
        }
    }
}
