using NUnit.Framework;
using Phnx.IO;
using Phnx.Serialization.Tests.TestClasses;
using System;
using System.IO;

namespace Phnx.Serialization.Tests
{
    public class JsonSerializerTests
    {
        public JsonSerializerTests()
        {
            JsonSerializer = new JsonSerializer();
        }

        public JsonSerializer JsonSerializer { get; }

        [Test]
        public void Serialize_NullValue_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => JsonSerializer.Serialize<object>(null));
        }

        [Test]
        public void Serialize_NullValueWithStream_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => JsonSerializer.Serialize<object>(null, new TestStream()));
        }

        [Test]
        public void Serialize_WhenOutputIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => JsonSerializer.Serialize(new object(), null));
        }

        [Test]
        public void Serialize_WhenOutputIsReadOnly_ThrowsArgumentException()
        {
            var testStream = new TestStream(canWrite: false);
            Assert.Throws<ArgumentException>(() => JsonSerializer.Serialize(new object(), testStream));
        }

        [Test]
        public void Deserialize_NullValue_ThrowsArgumentNullException()
        {
            string nullString = null;
            Assert.Throws<ArgumentNullException>(() => JsonSerializer.Deserialize<object>(nullString));
        }

        [Test]
        public void Deserialize_NullValueWithStream_ThrowsArgumentNullException()
        {
            Stream nullStream = null;
            Assert.Throws<ArgumentNullException>(() => JsonSerializer.Deserialize<object>(nullStream));
        }

        [Test]
        public void Deserialize_WhenInputIsWriteOnly_ThrowsArgumentException()
        {
            var testStream = new TestStream(canRead: false);
            Assert.Throws<ArgumentException>(() => JsonSerializer.Deserialize<object>(testStream));
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

            var serializedObject = JsonSerializer.Serialize(objectToClone);
            var clone = JsonSerializer.Deserialize<ComplexClass>(serializedObject);

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
            JsonSerializer.Serialize(objectToClone, dataStream);
            var clone = JsonSerializer.Deserialize<ComplexClass>(dataStream);

            Assert.AreEqual(objectToClone.ComplexProperty.MyValue, clone.ComplexProperty.MyValue);
        }
    }
}
