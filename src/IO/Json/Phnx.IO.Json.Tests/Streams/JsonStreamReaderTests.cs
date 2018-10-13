using Newtonsoft.Json;
using NUnit.Framework;
using Phnx.IO.Json.Streams;
using Phnx.IO.Json.Tests.Fakes;
using System;
using System.Collections.Generic;
using System.IO;

namespace Phnx.IO.Json.Tests.Streams
{
    public class JsonStreamReaderTests
    {
        [Test]
        public void New_WithNullTextReader_ThrowsArgumentNullException()
        {
            TextReader reader = null;
            Assert.Throws<ArgumentNullException>(() => new JsonStreamReader(reader));
        }

        [Test]
        public void New_WithNullJsonReader_ThrowsArgumentNullException()
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
    }
}
