using MarkSFrancis.Serialization.Extensions;
using MarkSFrancis.Serialization.Tests.TestClasses;
using NUnit.Framework;

namespace MarkSFrancis.Serialization.Tests.Extensions
{
    public class DeepCopyTests
    {
        [Test]
        public void GettingADeepCopy_DoesNotThrowError_WithEmptyClass()
        {
            var emptyClass = new EmptyClass();

            Assert.DoesNotThrow(() => emptyClass.DeepCopy());
        }

        [Test]
        public void GettingADeepCopy_CopiesPublicMembers_WithComplexClass()
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
        public void GettingADeepCopy_CopiesInheritedMembers_WithComplexClass()
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
        public void GettingADeepCopy_CopiesComplexMembers_WithComplexClass()
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
