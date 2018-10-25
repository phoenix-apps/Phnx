using NUnit.Framework;
using Phnx.Security.Passwords;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Security.Tests.Passwords
{
    public class PasswordHashManagerIDictionaryTests
    {
        [Test]
        public void GetEnumerator_WhenTwoItems_GoesThroughAllItemsInHasManager()
        {
            var hashManager = new PasswordHashManager
            {
                { 0, new PasswordHashDefault() },
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

        [Test]
        public void Remove_WhenDoesNotContainMatch_ReturnsFalse()
        {
            var hashManager = new PasswordHashManager();

            var result = hashManager.Remove(new KeyValuePair<int, IPasswordHash>(0, new PasswordHashDefault()));

            Assert.IsFalse(result);

            result = hashManager.Remove(0);

            Assert.IsFalse(result);
        }

        [Test]
        public void Remove_WhenContainsMatch_ReturnsTrueAndRemoves()
        {
            var defaultHasher = new PasswordHashDefault();
            var hashManager = new PasswordHashManager {
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
            var hashManager = NewPasswordManager();

            var result = hashManager.Remove(new KeyValuePair<int, IPasswordHash>(0, null));

            Assert.IsFalse(result);
            Assert.AreEqual(1, hashManager.Count);
        }
    }
}
