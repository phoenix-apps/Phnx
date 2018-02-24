using MarkSFrancis.Security.Passwords.Interface;

namespace MarkSFrancis.Security.Passwords
{
    /// <summary>
    /// A hash generator with a version of zero
    /// </summary>
    public class PasswordHashVersionZero : IPasswordHashVersion
    {
        public int Version => 0;
        public int HashBytesLength => 24;
        public int SaltBytesLength => 24;
        public int IterationCount => 1024;

        private Pbkdf2Hash HashGenerator { get; }

        public PasswordHashVersionZero()
        {
            HashGenerator = new Pbkdf2Hash(IterationCount);
        }

        public byte[] GenerateHash(byte[] password, byte[] salt)
        {
            return HashGenerator.Hash(password, salt);
        }

        public byte[] GenerateSalt()
        {
            return HashGenerator.GenerateSalt();
        }
    }
}