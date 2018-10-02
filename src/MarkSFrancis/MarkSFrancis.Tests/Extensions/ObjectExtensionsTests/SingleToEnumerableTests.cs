using NUnit.Framework;
using System.Linq;

namespace MarkSFrancis.Tests.Extensions.ObjectExtensionsTests
{
    public class SingleToEnumerableTests
    {
        [Test]
        public void StringToEnumerable_WhenStringHasContent_GetsEnumerableWithLength1()
        {
            var testString = "asdf";

            var results = testString.SingleToIEnumerable();

            Assert.AreEqual(1, results.Count());
        }

        [Test]
        public void StringToEnumerable_WhenStringHasContent_GetsEnumerableFirstItemMatchingString()
        {
            var testString = "asdf";

            var results = testString.SingleToIEnumerable();

            Assert.AreEqual(testString, results.First());
        }

        [Test]
        public void ObjectToEnumerable_WhenObjectIsNull_GetsEnumerable()
        {
            object testObject = null;

            var results = testObject.SingleToIEnumerable();

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(null, results.First());
        }
    }
}
