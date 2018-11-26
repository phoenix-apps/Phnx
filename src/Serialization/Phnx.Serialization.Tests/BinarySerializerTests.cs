using NUnit.Framework;
using Phnx.IO;
using Phnx.Serialization.Tests.TestClasses;
using System;
using System.IO;

namespace Phnx.Serialization.Tests
{
    public class BinarySerializerTests
    {
        public BinarySerializerTests()
        {
            BinarySerializer = new BinarySerializer();
        }

        public BinarySerializer BinarySerializer { get; }

        [Test]
        public void Serialize_NullValue_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => BinarySerializer.Serialize<object>(null));
        }

        [Test]
        public void Serialize_NullValueWithStream_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => BinarySerializer.Serialize<object>(null, new TestStream()));
        }

        [Test]
        public void Serialize_WhenOutputIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => BinarySerializer.Serialize(new object(), null));
        }

        [Test]
        public void Serialize_WhenOutputIsReadOnly_ThrowsArgumentException()
        {
            var testStream = new TestStream(canWrite: false);
            Assert.Throws<ArgumentException>(() => BinarySerializer.Serialize(new object(), testStream));
        }

        [Test]
        public void Deserialize_NullValue_ThrowsArgumentNullException()
        {
            byte[] nullBytes = null;
            Assert.Throws<ArgumentNullException>(() => BinarySerializer.Deserialize<object>(nullBytes));
        }

        [Test]
        public void Deserialize_NullValueWithStream_ThrowsArgumentNullException()
        {
            Stream nullStream = null;
            Assert.Throws<ArgumentNullException>(() => BinarySerializer.Deserialize<object>(nullStream));
        }

        [Test]
        public void Deserialize_WhenInputIsWriteOnly_ThrowsArgumentException()
        {
            var testStream = new TestStream(canRead: false);
            Assert.Throws<ArgumentException>(() => BinarySerializer.Deserialize<object>(testStream));
        }

        [Test]
        public void SerializeAndDeserialize_ComplexObject_ConvertsBackToOriginal()
        {
            var objectToClone = new ComplexClass
            {
                ComplexProperty = new ComplexPropertyClass
                {
                    MyValue = 251
                }
            };

            var serializedObject = BinarySerializer.Serialize(objectToClone);
            var clone = BinarySerializer.Deserialize<ComplexClass>(serializedObject);

            Assert.AreEqual(objectToClone.ComplexProperty.MyValue, clone.ComplexProperty.MyValue);
        }

        [Test]
        public void SerializeAndDeserialize_WithStreamsAndComplexObject_ConvertsBackToOriginal()
        {
            var objectToClone = new ComplexClass
            {
                ComplexProperty = new ComplexPropertyClass
                {
                    MyValue = 251
                }
            };

            var dataStream = new PipeStream();
            BinarySerializer.Serialize(objectToClone, dataStream);
            var clone = BinarySerializer.Deserialize<ComplexClass>(dataStream);

            Assert.AreEqual(objectToClone.ComplexProperty.MyValue, clone.ComplexProperty.MyValue);
        }
    }
}
