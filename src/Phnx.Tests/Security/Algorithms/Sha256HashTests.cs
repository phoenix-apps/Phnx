using NUnit.Framework;
using Phnx.Security.Algorithms;
using System;
using System.Text;

namespace Phnx.Security.Tests.Algorithms
{
    public class Sha256HashTests
    {
        public Sha256HashTests()
        {
            Sha256Hash = new Sha256Hash();
        }

        private Sha256Hash Sha256Hash { get; }

        [Test]
        public void Hash_WithNullData_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Sha256Hash.Hash(null));
        }

        [Test]
        public void Hash_WithNegativeIterations_ThrowsArgumentOutOfRangeException()
        {
            var data = Encoding.UTF8.GetBytes("asdf");

            Assert.Throws<ArgumentOutOfRangeException>(() => Sha256Hash.Hash(data, -1));
        }

        [Test]
        public void Hash_WithZeroIterations_ReturnsOriginalData()
        {
            var data = Encoding.UTF8.GetBytes("asdf");

            var hashed = Sha256Hash.Hash(data, 0);

            CollectionAssert.AreEqual(data, hashed);
        }

        [Test]
        public void EncryptingText_WithMessage_CreatesUnreadableBytes()
        {
            string plainText = "This is an awkwardly long message that must be secured in a meaningful way";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            var result = Sha256Hash.Hash(plainBytes, 1);

            Assert.AreNotEqual(plainBytes, result);
        }
    }
}
