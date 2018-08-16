using System.Security.Cryptography;
using MarkSFrancis.Security.Passwords.Interface;

namespace MarkSFrancis.Security.Tests.Passwords
{
    internal class PasswordHashVersionMock : IPasswordHashVersion
    {
        public int Version => 1;
        public int HashBytesLength => 24;
        public int SaltBytesLength => 24;

        private RNGCryptoServiceProvider CryptoServiceProvider { get; }

        public PasswordHashVersionMock()
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