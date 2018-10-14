using Newtonsoft.Json;
using NUnit.Framework;
using Phnx.IO.Json.Tests.Fakes;
using System;
using System.Collections.Generic;
using System.IO;

namespace Phnx.IO.Json.Tests.Streams
{
    public class JsonStreamReaderTests
    {
        [Test]
        public void New_WhenTextReaderIsNull_ThrowsArgumentNullException()
        {
            TextReader reader = null;
            Assert.Throws<ArgumentNullException>(() => new JsonStreamReader(reader));
        }

        [Test]
        public void New_WhenCloseStreamIsTrue_SetsCloseStreamToTrue()
        {
            var reader = new PipeStream().Out;

            var jsonReader = new JsonStreamReader(reader, true);

            Assert.IsTrue(jsonReader.CloseStreamWhenDisposed);
        }

        [Test]
        public void New_WhenCloseStreamIsFalse_SetsCloseStreamToFalse()
        {
            var reader = new PipeStream().Out;

            var jsonReader = new JsonStreamReader(reader, false);

            Assert.IsFalse(jsonReader.CloseStreamWhenDisposed);
        }

        [Test]
        public void New_WhenJsonReaderIsNull_ThrowsArgumentNullException()
        {
            JsonTextReader jsonReader = null;
            Assert.Throws<ArgumentNullException>(() => new JsonStreamReader(jsonReader));
        }

        [Test]
        public void New_WithJsonReader_SetsBaseJsonReaderToReader()
        {
            var pipe = new PipeStream();
            var reader = new JsonTextReader(pipe.Out);

            var jsonStream = new JsonStreamReader(reader);

            Assert.AreEqual(reader, jsonStream.BaseJsonReader);
        }

        [Test]
        public void Read_WhenJsonReaderContainsSingleJson_ReadsObject()
        {
            var expected = new DeepFake
            {
                Collection = new List<ShallowFake>
                {
                    new ShallowFake
                    {
                        Id = 123
                    }
                },
                Single = new ShallowFake
                {
                    Id = 25
                }
            };

            var pipe = new PipeStream();
            {
                var expectedAsJson = JsonConvert.SerializeObject(expected);
                pipe.In.Write(expectedAsJson);
            }
            var reader = new JsonTextReader(pipe.Out);

            var jsonStream = new JsonStreamReader(reader);

            var result = jsonStream.ReadObject<DeepFake>();

            CollectionAssert.AreEqual(expected.Collection, result.Collection);
            Assert.AreEqual(expected.Single, result.Single);
        }

        [Test]
        public void Read_WhenJsonReaderContainsTwoJson_ReadsOneThenNext()
        {
            var expectedFirst = new ShallowFake
            {
                Id = 123
            };

            var expectedSecond = new ShallowFake
            {
                Id = 25
            };

            var pipe = new PipeStream();
            {
                var expectedFirstAsJson = JsonConvert.SerializeObject(expectedFirst);
                pipe.In.Write(expectedFirstAsJson);

                var expectedSecondAsJson = JsonConvert.SerializeObject(expectedSecond);
                pipe.In.Write(expectedSecondAsJson);
            }

            var reader = new JsonTextReader(new StreamReader(pipe));

            var jsonStream = new JsonStreamReader(reader);

            var firstResult = jsonStream.ReadObject<ShallowFake>();
            var secondResult = jsonStream.ReadObject<ShallowFake>();

            Assert.AreEqual(expectedFirst, firstResult);
            Assert.AreEqual(expectedSecond, secondResult);
        }

        [Test]
        public void Read_WhenStreamIsEmpty_ThrowsEndOfStreamException()
        {
            var reader = new JsonStreamReader(new PipeStream().Out);

            Assert.Throws<EndOfStreamException>(() => reader.ReadJObject());
        }

        [Test]
        public void ReadJson_WhenStreamContainsUnformattedObject_ReadsAsJsonString()
        {
            var expectedObject = new ShallowFake
            {
                Id = 1245
            };

            var expected = JsonConvert.SerializeObject(expectedObject);

            var jsonStream = new JsonStreamReader(new PipeStream(expected).Out);

            var result = jsonStream.ReadJson(Formatting.None);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ReadJson_WhenStreamContainsIndentedObject_ReadsAsJsonString()
        {
            var expectedObject = new ShallowFake
            {
                Id = 1245
            };

            var expected = JsonConvert.SerializeObject(expectedObject, Formatting.Indented);

            var jsonStream = new JsonStreamReader(new PipeStream(expected).Out);

            var result = jsonStream.ReadJson(Formatting.Indented);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Dispose_WhenCloseStreamWhenDisposedIsTrue_ClosesStream()
        {
            var stream = new FakeReader();
            var reader = new JsonStreamReader(stream);
            reader.CloseStreamWhenDisposed = true;

            Assert.IsTrue(stream.IsOpen);
            reader.Dispose();

            Assert.IsFalse(stream.IsOpen);
        }

        [Test]
        public void Dispose_WhenCloseStreamWhenDisposedIsFalse_DoesNotCloseStream()
        {
            var stream = new FakeReader();
            var reader = new JsonStreamReader(stream);
            reader.CloseStreamWhenDisposed = false;

            Assert.IsTrue(stream.IsOpen);
            reader.Dispose();

            Assert.IsTrue(stream.IsOpen);
        }
    }
}
