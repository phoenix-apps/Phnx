using System.Security.Cryptography;
using MarkSFrancis.Security.Hash.Interface;

namespace MarkSFrancis.Security.Tests.Hash
{
    internal class HashGeneratorMock : IHashGeneratorVersion
    {
        public int Version => 1;
        public int HashBytesLength => 24;
        public int SaltBytesLength => 24;

        private RNGCryptoServiceProvider CryptoServiceProvider { get; }

        public HashGeneratorMock()
        {
            CryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public byte[] GenerateHash(byte[] password, byte[] salt)
        {
            return new byte[HashBytesLength];
        }

        public byte[] GenerateSalt()
        {
            return new byte[SaltBytesLength];
        }
    }
}