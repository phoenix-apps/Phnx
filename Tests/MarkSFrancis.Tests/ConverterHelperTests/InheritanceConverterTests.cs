using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace MarkSFrancis.Tests.ConverterHelperTests
{
    public class InheritanceConverterTests
    {
        [Test]
        public void Converting_FromChildTypeToParent_Converts()
        {
            var converter = ConverterHelpers.GetDefaultConverter<CustomType, IEnumerable<string>>();

            var resultShouldBe = new CustomType();
            var result = converter(resultShouldBe);

            Assert.AreEqual(resultShouldBe, result);
        }

        [Test]
        public void Converting_FromParentTypeToChild_Converts()
        {
            var converter = ConverterHelpers.GetDefaultConverter<IEnumerable<string>, CustomType>();

            var resultShouldBe = new CustomType();
            var result = converter(resultShouldBe);

            Assert.AreEqual(resultShouldBe, result);
        }

        private class CustomType : IEnumerable<string>
        {
            IEnumerable<string> Items { get; }

            public CustomType()
            {
                Items = new string[0];
            }

            public IEnumerator<string> GetEnumerator()
            {
                return Items.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
