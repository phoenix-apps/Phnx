using NUnit.Framework;
using Phnx.IO;
using Phnx.Serialization.Tests.TestClasses;
using System;
using System.IO;

namespace Phnx.Serialization.Tests
{
    public class XmlSerializerTests
    {

        public XmlSerializerTests()
        {
            XmlSerializer = new XmlSerializer();
        }

        public XmlSerializer XmlSerializer { get; }

        [Test]
        public void Serialize_NullValue_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => XmlSerializer.Serialize<object>(null));
        }

        [Test]
        public void Serialize_NullValueWithStream_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => XmlSerializer.Serialize<object>(null, new TestStream()));
        }

        [Test]
        public void Serialize_WhenOutputIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => XmlSerializer.Serialize(new object(), null));
        }

        [Test]
        public void Serialize_WhenOutputIsReadOnly_ThrowsArgumentException()
        {
            var testStream = new TestStream(canWrite: false);
            Assert.Throws<ArgumentException>(() => XmlSerializer.Serialize(new object(), testStream));
        }

        [Test]
        public void Deserialize_NullValue_ThrowsArgumentNullException()
        {
            string nullString = null;
            Assert.Throws<ArgumentNullException>(() => XmlSerializer.Deserialize<object>(nullString));
        }

        [Test]
        public void Deserialize_NullValueWithStream_ThrowsArgumentNullException()
        {
            Stream nullStream = null;
            Assert.Throws<ArgumentNullException>(() => XmlSerializer.Deserialize<object>(nullStream));
        }

        [Test]
        public void Deserialize_WhenInputIsWriteOnly_ThrowsArgumentException()
        {
            var testStream = new TestStream(canRead: false);
            Assert.Throws<ArgumentException>(() => XmlSerializer.Deserialize<object>(testStream));
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
            XmlSerializer.Serialize(objectToClone, dataStream);
            var clone = XmlSerializer.Deserialize<ComplexClass>(dataStream);

            Assert.AreEqual(objectToClone.ComplexProperty.MyValue, clone.ComplexProperty.MyValue);
        }
    }
}
