using System;
using System.Security.Cryptography;
using MarkSFrancis.Security.Extensions;
using MarkSFrancis.Security.Interfaces;

namespace MarkSFrancis.Security
{
    public class Sha256Hash : IHashWithoutSalt, IHashWithSalt
    {
        public const int HashBytesLength = 32;
        public const int SaltBytesLength = 32;
        public int IterationCount { get; }

        public Sha256Hash(int iterationCount = 1)
        {
            IterationCount = iterationCount;
        }

        public byte[] Hash(byte[] data)
        {
            if (IterationCount <= 0)
            {
                return data;
            }

            SHA256Managed sha = new SHA256Managed();

            var hashedData = sha.ComputeHash(data);

            for (int iterationsSoFar = 1; iterationsSoFar < IterationCount; ++iterationsSoFar)
            {
                hashedData = sha.ComputeHash(hashedData);
            }

            return hashedData;
        }

        public byte[] GenerateSalt()
        {
            return SecureRandomBytes.Generate(SaltBytesLength);
        }

        public byte[] Hash(byte[] data, byte[] salt)
        {
            if (salt.Length != SaltBytesLength)
            {
                throw ErrorFactory.Default.InvalidSaltSize(SaltBytesLength, salt.Length);
            }

            byte[] copiedData = new byte[data.Length + salt.Length];
            Array.Copy(data, copiedData, data.Length);
            Array.Copy(salt, 0, copiedData, data.Length, salt.Length);

            return Hash(copiedData);
        }
    }
}
