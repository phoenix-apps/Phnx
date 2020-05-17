using NUnit.Framework;
using Phnx.Security.Algorithms;
using Phnx.Security.Passwords;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Security.Tests.Passwords
{
    public class PasswordHashManagerIDictionaryTests
    {
        [Test]
        public void Add_WhenGeneratorIsNotNull_Adds()
        {
            var hasher = new PasswordHashDefault(new Pbkdf2Hash());
            var hashManager = new PasswordHashManager
            {
                { 5, hasher }
            };

            Assert.AreEqual(1, hashManager.Count);
            Assert.AreEqual(5, hashManager.First().Key);
            Assert.AreEqual(hasher, hashManager.First().Value);
        }

        [Test]
        public void Add_WhenGeneratorIsNull_ThrowsArgumentNullException()
        {
            var hashManager = new PasswordHashManager();

            Assert.Throws<ArgumentNullException>(() => hashManager.Add(0, null));
        }

        [Test]
        public void Add_WhenVersionIsNegative_Adds()
        {
            var hasher = new PasswordHashDefault(new Pbkdf2Hash());
            var hashManager = new PasswordHashManager
            {
                { -10, hasher }
            };

            Assert.AreEqual(1, hashManager.Count);
            Assert.AreEqual(-10, hashManager.First().Key);
            Assert.AreEqual(hasher, hashManager.First().Value);
        }

        [Test]
        public void AddPair_WhenGeneratorIsNotNull_Adds()
        {
            var hasher = new PasswordHashDefault(new Pbkdf2Hash());
            var hashManager = new PasswordHashManager
            {
                new KeyValuePair<int, IPasswordHash>(5, hasher)
            };

            Assert.AreEqual(1, hashManager.Count);
            Assert.AreEqual(5, hashManager.First().Key);
            Assert.AreEqual(hasher, hashManager.First().Value);
        }

        [Test]
        public void AddPair_WhenGeneratorIsNull_ThrowsArgumentException()
        {
            var hashManager = new PasswordHashManager();

            var pairToAdd = new KeyValuePair<int, IPasswordHash>(0, null);
            Assert.Throws<ArgumentException>(() => hashManager.Add(pairToAdd));
        }

        [Test]
        public void AddPair_WhenVersionIsNegative_Adds()
        {
            var hasher = new PasswordHashDefault(new Pbkdf2Hash());
            var hashManager = new PasswordHashManager
            {
                new KeyValuePair<int, IPasswordHash>(-10, hasher)
            };

            Assert.AreEqual(1, hashManager.Count);
            Assert.AreEqual(-10, hashManager.First().Key);
            Assert.AreEqual(hasher, hashManager.First().Value);
        }

        [Test]
        public void IndexGet_WhenDoesContain_GetsValue()
        {
            var hasher = new PasswordHashDefault(new Pbkdf2Hash());
            var hashManager = new PasswordHashManager
            {
                { 0, hasher }
            };

            var result = hashManager[0];

            Assert.AreEqual(hasher, result);
        }

        [Test]
        public void IndexGet_WhenDoesNotContain_ThrowsKeyNotFoundException()
        {
            var hashManager = new PasswordHashManager
            {
                { 1, new PasswordHashDefault(new Pbkdf2Hash()) }
            };

            Assert.Throws<KeyNotFoundException>(() => _ = hashManager[0]);
        }

        [Test]
        public void IndexSet_WhenDoesContain_SetsValue()
        {
            var hasher = new PasswordHashVersionMock();
            var hashManager = new PasswordHashManager
            {
                { 0, new PasswordHashDefault(new Pbkdf2Hash()) }
            };

            hashManager[0] = hasher;

            Assert.AreEqual(hasher, hashManager[0]);
        }

        [Test]
        public void IndexSet_WhenDoesContainButValueIsNull_ThrowsArgumentNullException()
        {
            var hasher = new PasswordHashDefault(new Pbkdf2Hash());
            var hashManager = new PasswordHashManager
            {
                { 0, hasher }
            };

            Assert.Throws<ArgumentNullException>(() => hashManager[0] = null);
        }

        [Test]
        public void IndexSet_WhenDoesNotContain_Adds()
        {
            var hasher = new PasswordHashVersionMock();
            var hashManager = new PasswordHashManager
            {
                { 1, new PasswordHashDefault(new Pbkdf2Hash()) }
            };

            hashManager[5] = hasher;

            Assert.AreEqual(2, hashManager.Count);
            Assert.AreEqual(hasher, hashManager[5]);
        }

        [Test]
        public void KeysGet_GetsAllKeys()
        {
            var expected = new List<int> { 0, 1 };
            var hashManager = new PasswordHashManager
            {
                { 0, new PasswordHashDefault(new Pbkdf2Hash()) },
                { 1, new PasswordHashVersionMock() }
            };

            CollectionAssert.AreEqual(expected, hashManager.Keys);
            CollectionAssert.AreEqual(expected, ((IReadOnlyDictionary<int, IPasswordHash>)hashManager).Keys);
        }

        [Test]
        public void ValuesGet_GetsAllValues()
        {
            var expected = new IPasswordHash[]
            {
                new PasswordHashDefault(new Pbkdf2Hash()), new PasswordHashVersionMock()
            };

            var hashManager = new PasswordHashManager
            {
                { 0, expected[0] },
                { 1, expected[1] }
            };

            CollectionAssert.AreEqual(expected, hashManager.Values);
            CollectionAssert.AreEqual(expected, ((IReadOnlyDictionary<int, IPasswordHash>)hashManager).Values);
        }

        [Test]
        public void IsReadOnly_IsFalse()
        {
            var hashManager = new PasswordHashManager();

            Assert.IsFalse(((ICollection<KeyValuePair<int, IPasswordHash>>)hashManager).IsReadOnly);
        }

        [Test]
        public void ContainsKey_WhenEmpty_ReturnsFalse()
        {
            var hashManager = new PasswordHashManager();

            var result = hashManager.ContainsKey(0);

            Assert.IsFalse(result);
        }

        [Test]
        public void ContainsKey_WhenContainsKeys_ReturnsTrue()
        {
            var hashManager = new PasswordHashManager
            {
                { 0, new PasswordHashDefault(new Pbkdf2Hash()) }
            };

            var result = hashManager.ContainsKey(0);

            Assert.IsTrue(result);
        }

        [Test]
        public void ContainsKey_WhenDoesNotContainKey_ReturnsFalse()
        {
            var hashManager = new PasswordHashManager
            {
                { 1, new PasswordHashDefault(new Pbkdf2Hash()) }
            };

            var result = hashManager.ContainsKey(0);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryGetValue_WhenContainsKeys_ReturnsTrueAndValue()
        {
            var hasher = new PasswordHashDefault(new Pbkdf2Hash());
            var hashManager = new PasswordHashManager
            {
                { 0, hasher }
            };

            var result = hashManager.TryGetValue(0, out var value);

            Assert.IsTrue(result);
            Assert.AreEqual(hasher, value);
        }

        [Test]
        public void TryGetValue_WhenDoesNotContainKey_ReturnsFalseAndNull()
        {
            var hashManager = new PasswordHashManager
            {
                { 1, new PasswordHashDefault(new Pbkdf2Hash()) }
            };

            var result = hashManager.TryGetValue(0, out var value);

            Assert.IsFalse(result);
            Assert.IsNull(value);
        }

        [Test]
        public void Contains_WhenDoesNotContainMatch_ReturnsFalse()
        {
            var hashManager = new PasswordHashManager();

            var result = hashManager.Contains(
                new KeyValuePair<int, IPasswordHash>(0, new PasswordHashDefault(new Pbkdf2Hash())));

            Assert.IsFalse(result);
        }

        [Test]
        public void Contains_WhenContainsMatch_ReturnsTrue()
        {
            var defaultHasher = new PasswordHashDefault(new Pbkdf2Hash());
            var hashManager = new PasswordHashManager
            {
                { 0, defaultHasher }
            };

            var result = hashManager.Contains(new KeyValuePair<int, IPasswordHash>(0, defaultHasher));

            Assert.IsTrue(result);
            Assert.AreEqual(1, hashManager.Count);
        }

        [Test]
        public void Contains_WhenValueToSearchForIsNull_ReturnsFalse()
        {
            var hashManager = new PasswordHashManager
            {
                { 0, new PasswordHashDefault(new Pbkdf2Hash()) }
            };

            var result = hashManager.Contains(new KeyValuePair<int, IPasswordHash>(0, null));

            Assert.IsFalse(result);
        }

        [Test]
        public void CopyTo_WhenEmpty_DoesNothing()
        {
            var expected = new KeyValuePair<int, IPasswordHash>[0];

            var hashManager = new PasswordHashManager();
            var result = new KeyValuePair<int, IPasswordHash>[0];

            hashManager.CopyTo(result, 0);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void CopyTo_WhenTargetIsNull_ThrowsArgumentNullException()
        {
            var hashManager = new PasswordHashManager
            {
                { 0, new PasswordHashDefault(new Pbkdf2Hash()) }
            };

            var result = new KeyValuePair<int, IPasswordHash>[0];

            Assert.Throws<ArgumentNullException>(() => hashManager.CopyTo(null, 0));
        }

        [Test]
        public void CopyTo_WhenStartIndexIsLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var hashManager = new PasswordHashManager
            {
                { 0, new PasswordHashDefault(new Pbkdf2Hash()) }
            };

            var result = new KeyValuePair<int, IPasswordHash>[0];

            Assert.Throws<ArgumentOutOfRangeException>(() => hashManager.CopyTo(result, -1));
        }

        [Test]
        public void CopyTo_WhenTargetIsTooSmall_ThrowsArgumentException()
        {
            var hashManager = new PasswordHashManager
            {
                { 0, new PasswordHashDefault(new Pbkdf2Hash()) }
            };

            var result = new KeyValuePair<int, IPasswordHash>[0];

            Assert.Throws<ArgumentException>(() => hashManager.CopyTo(result, 0));
        }

        [Test]
        public void Remove_WhenDoesNotContainMatch_ReturnsFalse()
        {
            var hashManager = new PasswordHashManager();

            var result = hashManager.Remove(
                new KeyValuePair<int, IPasswordHash>(0, new PasswordHashDefault(new Pbkdf2Hash())));

            Assert.IsFalse(result);

            result = hashManager.Remove(0);

            Assert.IsFalse(result);
        }

        [Test]
        public void Remove_WhenContainsMatch_ReturnsTrueAndRemoves()
        {
            var defaultHasher = new PasswordHashDefault(new Pbkdf2Hash());
            var hashManager = new PasswordHashManager
            {
                { 0, defaultHasher }
            };

            var result = hashManager.Remove(new KeyValuePair<int, IPasswordHash>(0, defaultHasher));

            Assert.IsTrue(result);
            Assert.AreEqual(0, hashManager.Count);

            hashManager.Add(0, defaultHasher);
            result = hashManager.Remove(0);

            Assert.IsTrue(result);
            Assert.AreEqual(0, hashManager.Count);
        }

        [Test]
        public void Remove_WhenValueToRemoveIsNull_DoesNotRemove()
        {
            var hashManager = new PasswordHashManager
            {
                { 0, new PasswordHashDefault(new Pbkdf2Hash()) }
            };

            var result = hashManager.Remove(new KeyValuePair<int, IPasswordHash>(0, null));

            Assert.IsFalse(result);
            Assert.AreEqual(1, hashManager.Count);
        }

        [Test]
        public void Clear_WhenEmpty_DoesNothing()
        {
            var hashManager = new PasswordHashManager();

            hashManager.Clear();

            Assert.AreEqual(0, hashManager.Count);
        }

        [Test]
        public void Clear_WhenHasValues_Clears()
        {
            var hashManager = new PasswordHashManager
            {
                { 1, new PasswordHashDefault(new Pbkdf2Hash()) }
            };

            hashManager.Clear();

            Assert.AreEqual(0, hashManager.Count);
        }

        [Test]
        public void GetEnumerator_WhenTwoItems_GoesThroughAllItemsInHasManager()
        {
            var hashManager = new PasswordHashManager
            {
                { 0, new PasswordHashDefault(new Pbkdf2Hash()) },
                { 1, new PasswordHashVersionMock() }
            };

            var totalT = 0;
            foreach (var item in hashManager)
            {
                totalT++;
            }
            Assert.AreEqual(2, totalT);

            var total = 0;
            foreach (var item in (IEnumerable)hashManager)
            {
                total++;
            }
            Assert.AreEqual(2, totalT);
        }
    }
}
