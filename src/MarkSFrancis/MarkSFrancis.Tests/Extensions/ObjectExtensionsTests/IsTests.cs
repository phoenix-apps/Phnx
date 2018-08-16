using MarkSFrancis.Extensions;
using MarkSFrancis.Tests.Fakes.TypeFakes;
using NUnit.Framework;

namespace MarkSFrancis.Tests.Extensions.ObjectExtensionsTests
{
    public class IsTests
    {

        [Test]
        public void GetIs_WhenObjectIsType_ReturnsTrue()
        {
            var testObj = new ChildClass();

            var _is = testObj.Is(typeof(ChildClass));

            Assert.IsTrue(_is);
        }

        [Test]
        public void GetIs_WhenTypeMatches_ReturnsTrue()
        {
            var _is = typeof(ChildClass).Is(typeof(ChildClass));

            Assert.IsTrue(_is);
        }

        [Test]
        public void GetIsForParentType_WhenObjectIsChildType_ReturnsTrue()
        {
            var testObj = new ChildClass();

            var _is = testObj.Is(typeof(ParentClass));

            Assert.IsTrue(_is);
        }

        [Test]
        public void GetIsForParentType_WhenTypeIsChildType_ReturnsTrue()
        {
            var _is = typeof(ChildClass).Is(typeof(ParentClass));

            Assert.IsTrue(_is);
        }

        [Test]
        public void GetIsForChildType_WhenObjectIsParentType_ReturnsFalse()
        {
            var testObj = new ParentClass();

            var _is = testObj.Is(typeof(ChildClass));

            Assert.IsFalse(_is);
        }

        [Test]
        public void GetIsForChildType_WhenTypeIsParentType_ReturnsFalse()
        {
            var _is = typeof(ParentClass).Is(typeof(ChildClass));

            Assert.IsFalse(_is);
        }
    }
}
