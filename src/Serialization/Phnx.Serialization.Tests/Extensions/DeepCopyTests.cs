using NUnit.Framework;
using Phnx.Serialization.Tests.TestClasses;

namespace Phnx.Serialization.Tests.Extensions
{
    public class DeepCopyTests
    {
        [Test]
        public void DeepCopy_NullObject_ReturnsNull()
        {
            object testValue = null;

            var copy = testValue.DeepCopy();

            Assert.AreEqual(testValue, copy);
        }

        [Test]
        public void DeepCopy_WithEmptyClass_DoesNotThrowError()
        {
            var emptyClass = new EmptyClass();

            Assert.DoesNotThrow(() => emptyClass.DeepCopy());
        }

        [Test]
        public void DeepCopy_WithComplexClass_CopiesPublicMembers()
        {
            var objectToClone = new ComplexClass
            {
                MyValue = "asdf"
            };

            var clonedObject = objectToClone.DeepCopy();

            Assert.AreEqual(objectToClone.MyValue, clonedObject.MyValue);

            clonedObject.MyValue = "changed";

            Assert.AreNotEqual(objectToClone.MyValue, clonedObject.MyValue);
        }

        [Test]
        public void DeepCopy_WithComplexClass_CopiesInheritedMembers()
        {
            var objectToClone = new ComplexClass
            {
                BaseValue = 12
            };

            var clonedObject = objectToClone.DeepCopy();

            Assert.AreEqual(objectToClone.BaseValue, clonedObject.BaseValue);

            clonedObject.BaseValue = 14;

            Assert.AreNotEqual(objectToClone.BaseValue, clonedObject.BaseValue);
        }

        [Test]
        public void DeepCopy_WithComplexClass_CopiesComplexMembers()
        {
            var objectToClone = new ComplexClass
            {
                ComplexProperty = new ComplexPropertyClass
                {
                    MyValue = 251
                }
            };

            var clonedObject = objectToClone.DeepCopy();

            Assert.AreEqual(objectToClone.ComplexProperty.MyValue, clonedObject.ComplexProperty.MyValue);

            clonedObject.ComplexProperty.MyValue = 200;

            Assert.AreNotEqual(objectToClone.ComplexProperty.MyValue, clonedObject.ComplexProperty.MyValue);
        }
    }
}
