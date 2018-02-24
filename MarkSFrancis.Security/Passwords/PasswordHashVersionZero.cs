using System.Security.Cryptography;
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

        private RNGCryptoServiceProvider CryptoServiceProvider { get; }

        public PasswordHashVersionZero()
        {
            CryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public byte[] GenerateHash(byte[] password, byte[] salt)
        {
            byte[] hashValue;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, IterationCount))
            {
                hashValue = pbkdf2.GetBytes(HashBytesLength);
            }
            return hashValue;
        }

        public byte[] GenerateSalt()
        {
            byte[] newSalt = new byte[SaltBytesLength];
            CryptoServiceProvider.GetBytes(newSalt);

            return newSalt;
        }
    }
}