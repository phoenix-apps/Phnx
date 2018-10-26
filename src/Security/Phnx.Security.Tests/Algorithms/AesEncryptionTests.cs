using NUnit.Framework;
using Phnx.Security.Algorithms;
using System;
using System.Text;

namespace Phnx.Security.Tests.Algorithms
{
    public class AesEncryptionTests
    {
        public AesEncryptionTests()
        {
            AesEncryption = new AesEncryption();
        }

        public AesEncryption AesEncryption { get; }

        [Test]
        public void CreateRandomKey_GetsKeyOfSizeKeySizeDividedBy8()
        {
            var key = AesEncryption.CreateRandomKey();

            Assert.AreEqual(AesEncryption.KeyBits / 8, key.Length);
        }

        [Test]
        public void CreateRandomIv_GetsKeyOfSizeIVSizeDividedBy8()
        {
            var key = AesEncryption.CreateRandomIv();

            Assert.AreEqual(AesEncryption.IvBits / 8, key.Length);
        }
    }
}
