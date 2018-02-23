using System;
using System.Text;
using MarkSFrancis.Security.Hash.Interface;

namespace MarkSFrancis.Security.Hash
{
    internal class VersionedHash
    {
        private static readonly Encoding DefaultPasswordEncoding = Encoding.Unicode;
        
        public VersionedHash(string password, IHashGeneratorVersion hashGenerator)
        {
            Version = hashGenerator.Version;
            Salt = hashGenerator.GenerateSalt();

            var passBytes = DefaultPasswordEncoding.GetBytes(password);
            PasswordHash = hashGenerator.GenerateHash(passBytes, Salt);
        }

        public VersionedHash(string password, byte[] salt, IHashGeneratorVersion hashGenerator)
        {
            Version = hashGenerator.Version;
            Salt = salt;

            var passBytes = DefaultPasswordEncoding.GetBytes(password);
            PasswordHash = hashGenerator.GenerateHash(passBytes, salt);
        }

        public VersionedHash(byte[] bytes, IHashGeneratorVersion hashGenerator)
        {
            Version = BitConverter.ToInt32(bytes, 0);

            PasswordHash = new byte[hashGenerator.HashBytesLength];
            Array.Copy(bytes, 4, PasswordHash, 0, PasswordHash.Length);

            Salt = new byte[hashGenerator.SaltBytesLength];
            Array.Copy(bytes, 4 + PasswordHash.Length, Salt, 0, Salt.Length);
        }

        public static int GetVersionFromBytes(byte[] bytes)
        {
            return BitConverter.ToInt32(bytes, 0);
        }

        public byte[] PasswordHash { get; }

        public byte[] Salt { get; }

        public int Version { get; }

        public byte[] ToBytes()
        {
            byte[] returnBytes = new byte[4 + PasswordHash.Length + Salt.Length];

            byte[] versionBytes = BitConverter.GetBytes(Version);

            Array.Copy(versionBytes, returnBytes, 4);

            Array.Copy(PasswordHash, 0, returnBytes, 4, PasswordHash.Length);

            Array.Copy(Salt, 0, returnBytes, 4 + PasswordHash.Length, Salt.Length);

            return returnBytes;
        }
    }
}