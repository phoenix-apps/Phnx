using System.Security.Cryptography;
using MarkSFrancis.Security.Extensions;
using MarkSFrancis.Security.Interfaces;

namespace MarkSFrancis.Security
{
    public class Pbkdf2Hash : IHash
    {
        public const int HashBytesLength = 24;
        public const int SaltBytesLength = 24;
        public int IterationCount { get; }

        public Pbkdf2Hash(int iterationCount = 1024)
        {
            IterationCount = iterationCount;
        }

        public virtual byte[] GenerateSalt()
        {
            return SecureRandomBytes.Generate(SaltBytesLength);
        }

        public virtual byte[] Hash(byte[] data, byte[] salt)
        {
            if (salt.Length != SaltBytesLength)
            {
                throw ErrorFactory.Default.InvalidSaltSize(SaltBytesLength, salt.Length);
            }
            
            using (var pbkdf2 = new Rfc2898DeriveBytes(data, salt, IterationCount))
            {
                return pbkdf2.GetBytes(HashBytesLength);
            }
        }
    }
}
