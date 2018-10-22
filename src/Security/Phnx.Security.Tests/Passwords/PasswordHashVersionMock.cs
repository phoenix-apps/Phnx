using Phnx.Security.Passwords;

namespace Phnx.Security.Tests.Passwords
{
    internal class PasswordHashVersionMock : IPasswordHashVersion
    {
        public int Version => 1;
        public int HashBytesLength => 24;
        public int SaltBytesLength => 24;

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