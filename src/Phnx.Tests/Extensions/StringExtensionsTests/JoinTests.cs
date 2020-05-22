using NUnit.Framework;
using System;
using System.Linq;

namespace Phnx.Tests.Extensions.StringExtensionsTests
{
    public class JoinTests
    {
        [Test]
        public void JoiningCollection_ThatIsEmpty_ReturnsEmptyString()
        {
            byte[] arr = Array.Empty<byte>();

            var results = arr.Join(",");

            Assert.AreEqual(string.Empty, results);
        }

        [Test]
        public void JoiningCollection_WithSingleEntry_DoesNotContainDelimiter()
        {
            byte[] arr = new byte[] { 26 };

            var results = arr.Join(",");

            Assert.IsFalse(results.Contains(","));
        }

        [Test]
        public void JoiningCollection_WithSingleEntry_MatchesSingleEntryConvertedToString()
        {
            byte entry = 26;
            byte[] arr = new byte[] { entry };

            var results = arr.Join(",");

            Assert.AreEqual(entry.ToString(), results);
        }

        [Test]
        public void JoiningCollection_WithThreeEntries_ContainsTwoDelimiters()
        {
            byte[] arr = new byte[] { 26, 28, 63 };

            var results = arr.Join(",");
            var commaCount = results.Where(r => r == ',').Count();

            Assert.AreEqual(2, commaCount);
        }

        [Test]
        public void JoiningCollection_WithThreeEntries_MatchesThreeEntriesJoinedWithDelimiters()
        {
            string resultsShouldBe = "26, 28, 63";
            byte[] arr = new byte[] { 26, 28, 63 };

            var results = arr.Join(", ");

            Assert.AreEqual(resultsShouldBe, results);
        }
    }
}
