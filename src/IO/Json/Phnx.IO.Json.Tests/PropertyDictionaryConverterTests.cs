using NUnit.Framework;
using Phnx.IO.Json.Tests.Fakes;
using System;
using System.Collections.Generic;

namespace Phnx.IO.Json.Tests
{
    public class PropertyDictionaryConverterTests
    {
        private readonly PropertyDictionaryConverter _converter;

        public PropertyDictionaryConverterTests()
        {
            _converter = new PropertyDictionaryConverter();
        }

        private void ValidateDictionariesMatch<TKey, TValue>(Dictionary<TKey, TValue> expected, Dictionary<TKey, TValue> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            foreach (var item in expected)
            {
                var match = actual[item.Key];

                Assert.AreEqual(item.Value, match);
            }
        }

        [Test]
        public void ObjectTo_WhenNull_ThrowsArgumentNullException()
        {
            object o = null;
            Assert.Throws<ArgumentNullException>(() => _converter.To(o));
        }

        [Test]
        public void ObjectTo_WhenShallow_GetsDictionary()
        {
            var expected = new Dictionary<string, string>
            {
                { nameof(ShallowFake.Id), "7"}
            };

            object o = new ShallowFake
            {
                Id = 7
            };

            var propDict = _converter.To(o);
            ValidateDictionariesMatch(expected, propDict);
        }

        [Test]
        public void ObjectTo_WhenDeep_GetsDictionary()
        {
            var expected = new Dictionary<string, string>
            {
                { nameof(DeepFake.Single) + "." + nameof(ShallowFake.Id), "7"},
                { nameof(DeepFake.Collection), string.Empty }
            };

            object o = new DeepFake
            {
                Single = new ShallowFake
                {
                    Id = 7
                }
            };

            var propDict = _converter.To(o);
            ValidateDictionariesMatch(expected, propDict);
        }

        [Test]
        public void ObjectTo_WhenReallyDeep_GetsDictionary()
        {
            var expected = new Dictionary<string, string>
            {
                { nameof(ReallyDeepFake.First) + "." + nameof(DeepFake.Single) + "." + nameof(ShallowFake.Id), "7" },
                { nameof(ReallyDeepFake.First) + "." + nameof(DeepFake.Collection), string.Empty },
                { nameof(ReallyDeepFake.Second) + "." + nameof(DeepFake.Single) +"." + nameof(ShallowFake.Id), "12"},
                { nameof(ReallyDeepFake.Second) + "." + nameof(DeepFake.Collection), string.Empty }
            };

            object o = new ReallyDeepFake
            {
                First = new DeepFake
                {
                    Single = new ShallowFake
                    {
                        Id = 7
                    }
                },
                Second = new DeepFake
                {
                    Single = new ShallowFake
                    {
                        Id = 12
                    }
                }
            };

            var propDict = _converter.To(o);
            ValidateDictionariesMatch(expected, propDict);
        }

        [Test]
        public void FromT_WhenNull_ThrowsArgumentNullException()
        {
            Dictionary<string, string> dict = null;
            Assert.Throws<ArgumentNullException>(() => _converter.From<object>(dict));
        }

        [Test]
        public void FromT_WhenShallow_GetsObject()
        {
            ShallowFake expected = new ShallowFake
            {
                Id = 7
            };

            var dict = new Dictionary<string, string>
            {
                { nameof(ShallowFake.Id), "7"}
            };

            var converted = _converter.From<ShallowFake>(dict);

            Assert.AreEqual(expected.Id, converted.Id);
        }

        [Test]
        public void FromT_WhenDeep_GetsObject()
        {
            var expected = new DeepFake
            {
                Single = new ShallowFake
                {
                    Id = 7
                }
            };

            var dict = new Dictionary<string, string>
            {
                { nameof(DeepFake.Single) + "." + nameof(ShallowFake.Id), "7"},
                { nameof(DeepFake.Collection), string.Empty }
            };

            var converted = _converter.From<DeepFake>(dict);

            Assert.AreEqual(expected.Single.Id, converted.Single.Id);
            CollectionAssert.AreEqual(expected.Collection, converted.Collection);
        }

        [Test]
        public void FromT_WhenReallyDeep_GetsObject()
        {
            var expected = new ReallyDeepFake
            {
                First = new DeepFake
                {
                    Single = new ShallowFake
                    {
                        Id = 7
                    }
                },
                Second = new DeepFake
                {
                    Single = new ShallowFake
                    {
                        Id = 12
                    }
                }
            };

            var dict = new Dictionary<string, string>
            {
                { nameof(ReallyDeepFake.First) + "." + nameof(DeepFake.Single) + "." + nameof(ShallowFake.Id), "7" },
                { nameof(ReallyDeepFake.First) + "." + nameof(DeepFake.Collection), string.Empty },
                { nameof(ReallyDeepFake.Second) + "." + nameof(DeepFake.Single) +"." + nameof(ShallowFake.Id), "12"},
                { nameof(ReallyDeepFake.Second) + "." + nameof(DeepFake.Collection), string.Empty }
            };

            var converted = _converter.From<ReallyDeepFake>(dict);

            Assert.AreEqual(expected.First.Single.Id, converted.First.Single.Id);
            Assert.AreEqual(expected.Second.Single.Id, converted.Second.Single.Id);
        }
    }
}
