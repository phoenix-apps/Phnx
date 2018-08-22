using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MarkSFrancis.Collections.Tests
{
    public class KeyValueTests
    {
        [Test]
        public void CreatingKeyValue_SetsKeyAndValue()
        {
            var keyValue = new KeyValue<string, string>("testKey", "testValue");

            Assert.AreEqual("testKey", keyValue.Key);
            Assert.AreEqual("testValue", keyValue.Value);
        }

        [Test]
        public void CreatingKeyValue_WithNoKeyValue_SetsKeyAndValueToDefault()
        {
            var keyValue = new KeyValue<int, string>();

            Assert.AreEqual(0, keyValue.Key);
            Assert.IsNull(keyValue.Value);
        }

        [Test]
        public void ChangingKeyValue_SetKey_ChangesKey()
        {
            var keyValue = new KeyValue<int, string>(5, "val");

            keyValue.Key = 17;

            Assert.AreEqual(17, keyValue.Key);
        }

        [Test]
        public void ChangingKeyValue_SetValue_ChangesKey()
        {
            var keyValue = new KeyValue<int, string>(5, "val");

            keyValue.Value = "val2";

            Assert.AreEqual("val2", keyValue.Value);
        }

        [Test]
        public void ToString_GetsKeyValueAsString()
        {
            var keyValue = new KeyValue<long, Guid>(1748734, new Guid("8c660c4f-885d-49c2-aacc-507faa7dea4e"));
            var expected = "[1748734, 8c660c4f-885d-49c2-aacc-507faa7dea4e]";

            var result = keyValue.ToString();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Equals_Self_ReturnsTrue()
        {
            var keyValue = new KeyValue<int, int>(1, 2);

            var isSame = keyValue.Equals(keyValue);

            Assert.IsTrue(isSame);
        }

        [Test]
        public void Equals_IdenticalKeyValue_ReturnsTrue()
        {
            var keyValue1 = new KeyValue<int, int>(1, 2);
            var keyValue2 = new KeyValue<int, int>(1, 2);

            var isSame = keyValue1.Equals(keyValue2);

            Assert.IsTrue(isSame);
        }

        [Test]
        public void Equals_DifferentValue_ReturnsFalse()
        {
            var keyValue1 = new KeyValue<int, int>(1, 2);
            var keyValue2 = new KeyValue<int, int>(1, 3);

            var isSame = keyValue1.Equals(keyValue2);

            Assert.IsFalse(isSame);
        }

        [Test]
        public void Equals_DifferentKey_ReturnsFalse()
        {
            var keyValue1 = new KeyValue<int, int>(1, 2);
            var keyValue2 = new KeyValue<int, int>(2, 2);

            var isSame = keyValue1.Equals(keyValue2);

            Assert.IsFalse(isSame);
        }

        [Test]
        public void Equals_SameKeyValuePair_ReturnsTrue()
        {
            var keyValue1 = new KeyValue<int, int>(1, 2);
            var keyValue2 = new KeyValuePair<int, int>(1, 2);

            var isSame = keyValue1.Equals(keyValue2);

            Assert.IsTrue(isSame);
        }

        [Test]
        public void Equals_DifferentKeyValuePair_ReturnsFalse()
        {
            var keyValue1 = new KeyValue<int, int>(1, 2);

            var keyValue2 = new KeyValuePair<int, int>(2, 2);
            var keyValue3 = new KeyValuePair<int, int>(1, 3);
            var keyValue4 = new KeyValuePair<int, int>(2, 3);

            var isSame = keyValue1.Equals(keyValue2);
            Assert.IsFalse(isSame);

            isSame = keyValue1.Equals(keyValue3);
            Assert.IsFalse(isSame);

            isSame = keyValue1.Equals(keyValue4);
            Assert.IsFalse(isSame);
        }

        [Test]
        public void Equals_DifferentObject_ReturnsFalse()
        {
            var keyValue1 = new KeyValue<int, int>(1, 2);

            int randomItem = 16;

            var isSame = keyValue1.Equals(randomItem);
            Assert.IsFalse(isSame);
        }

        [Test]
        public void Equals_SameKeyValuePairAsObject_ReturnsTrue()
        {
            var keyValue1 = new KeyValue<int, int>(1, 2);
            object keyValue2 = new KeyValue<int, int>(1, 2);

            var isSame = keyValue1.Equals(keyValue2);
            Assert.IsTrue(isSame);
        }

        [Test]
        public void Equals_DifferentKeyValuePairAsObject_ReturnsFalse()
        {
            var keyValue1 = new KeyValue<int, int>(1, 2);
            object keyValue2 = new KeyValue<int, int>(2, 2);

            var isSame = keyValue1.Equals(keyValue2);
            Assert.IsFalse(isSame);
        }
    }
}
