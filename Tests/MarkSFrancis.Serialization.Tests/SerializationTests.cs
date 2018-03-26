using MarkSFrancis.Serialization.Tests.TestClasses;
using NUnit.Framework;

namespace MarkSFrancis.Serialization.Tests
{
    public class SerializationTests
    {
        [Test]
        public void GettingADeepCopy_DoesNotThrowError_WithEmptyClass()
        {
            var emptyClass = new EmptyClass();

            Assert.DoesNotThrow(() =>
            {
                var serialized = emptyClass.Serialize();
                serialized.Deserialize<EmptyClass>();
            });
        }

        [Test]
        public void GettingADeepCopy_CopiesPublicMembers_WithComplexClass()
        {
            var objectToClone = new ComplexClass
            {
                MyValue = "asdf"
            };

            var serializedObject = objectToClone.Serialize();

            var restoredObject = serializedObject.Deserialize<ComplexClass>();

            Assert.AreEqual(objectToClone.MyValue, restoredObject.MyValue);
        }

        [Test]
        public void GettingADeepCopy_CopiesInheritedMembers_WithComplexClass()
        {
            var objectToClone = new ComplexClass
            {
                BaseValue = 12
            };

            var serializedObject = objectToClone.Serialize();

            var restoredObject = serializedObject.Deserialize<ComplexClass>();

            Assert.AreEqual(objectToClone.BaseValue, restoredObject.BaseValue);

            restoredObject.BaseValue = 14;

            Assert.AreNotEqual(objectToClone.BaseValue, restoredObject.BaseValue);
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

            var serializedObject = objectToClone.Serialize();

            var restoredObject = serializedObject.Deserialize<ComplexClass>();

            Assert.AreEqual(objectToClone.ComplexProperty.MyValue, restoredObject.ComplexProperty.MyValue);

            restoredObject.ComplexProperty.MyValue = 200;

            Assert.AreNotEqual(objectToClone.ComplexProperty.MyValue, restoredObject.ComplexProperty.MyValue);
        }
    }
}
