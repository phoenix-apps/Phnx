using Phnx.Security.Passwords;

namespace Phnx.Security.Tests.Passwords
{
    public class PasswordHashVersionBroken : IPasswordHash
    {
        public int HashBytesLength => 1;

        public int SaltBytesLength => 1;

        public byte[] GenerateHash(byte[] password, byte[] salt)
        {
            return new byte[5];
        }

        public byte[] GenerateSalt()
        {
            return new byte[5];
        }
    }
}
