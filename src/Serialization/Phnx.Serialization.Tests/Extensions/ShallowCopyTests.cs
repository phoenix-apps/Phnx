using NUnit.Framework;
using Phnx.Serialization.Tests.TestClasses;

namespace Phnx.Serialization.Tests
{
    public class ShallowCopyTests
    {
        [Test]
        public void ShallowCopy_WithNull_ReturnsNull()
        {
            object testObject = null;

            var copy = testObject.ShallowCopy();

            Assert.AreEqual(testObject, copy);
        }

        [Test]
        public void ShallowCopy_WithEmptyClass_DoesNotThrow()
        {
            var emptyClass = new EmptyClass();

            Assert.DoesNotThrow(() => emptyClass.ShallowCopy());
        }

        [Test]
        public void ShallowCopy_WithComplexClass_CopiesPublicMembers()
        {
            var objectToClone = new ComplexClass
            {
                MyValue = "asdf"
            };

            var clonedObject = objectToClone.ShallowCopy();

            Assert.AreEqual(objectToClone.MyValue, clonedObject.MyValue);

            clonedObject.MyValue = "changed";

            Assert.AreNotEqual(objectToClone.MyValue, clonedObject.MyValue);
        }

        [Test]
        public void ShallowCopy_WithComplexClass_CopiesInheritedMembers()
        {
            var objectToClone = new ComplexClass
            {
                BaseValue = 12
            };

            var clonedObject = objectToClone.ShallowCopy();

            Assert.AreEqual(objectToClone.BaseValue, clonedObject.BaseValue);

            clonedObject.BaseValue = 14;

            Assert.AreNotEqual(objectToClone.BaseValue, clonedObject.BaseValue);
        }

        [Test]
        public void ShallowCopy_WithComplexClass_DoesNotCopyComplexMembers()
        {
            var objectToClone = new ComplexClass
            {
                ComplexProperty = new ComplexPropertyClass
                {
                    MyValue = 251
                }
            };

            var clonedObject = objectToClone.ShallowCopy();

            Assert.AreEqual(objectToClone.ComplexProperty.MyValue, clonedObject.ComplexProperty.MyValue);

            clonedObject.ComplexProperty.MyValue = 200;

            Assert.AreEqual(objectToClone.ComplexProperty.MyValue, clonedObject.ComplexProperty.MyValue);
        }
    }
}
