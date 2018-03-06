using System;
using System.Text;
using MarkSFrancis.Security.Extensions;
using MarkSFrancis.Security.Passwords.Interface;

namespace MarkSFrancis.Security.Passwords
{
    internal class VersionedHash
    {
        private static readonly Encoding DefaultPasswordEncoding = Encoding.Unicode;

        public const int BytesUsedByVersionTag = 4;
        
        public VersionedHash(string password, IPasswordHashVersion hashGenerator)
        {
            Version = hashGenerator.Version;
            Salt = hashGenerator.GenerateSalt();

            var passBytes = DefaultPasswordEncoding.GetBytes(password);
            PasswordHash = hashGenerator.GenerateHash(passBytes, Salt);

            VerifyGenerator(hashGenerator);
        }

        public VersionedHash(string password, byte[] salt, IPasswordHashVersion hashGenerator)
        {
            Version = hashGenerator.Version;
            Salt = salt;

            var passBytes = DefaultPasswordEncoding.GetBytes(password);
            PasswordHash = hashGenerator.GenerateHash(passBytes, salt);

            VerifyGenerator(hashGenerator);
        }

        public VersionedHash(byte[] bytes, IPasswordHashVersion hashGenerator)
        {
            VerifyGenerator(bytes.Length, hashGenerator);

            Version = BitConverter.ToInt32(bytes, 0);

            PasswordHash = new byte[hashGenerator.HashBytesLength];
            Array.Copy(bytes, BytesUsedByVersionTag, PasswordHash, 0, PasswordHash.Length);

            Salt = new byte[hashGenerator.SaltBytesLength];
            Array.Copy(bytes, BytesUsedByVersionTag + PasswordHash.Length, Salt, 0, Salt.Length);
        }

        private void VerifyGenerator(IPasswordHashVersion generator)
        {
            var hashLength = BytesUsedByVersionTag + PasswordHash.Length + Salt.Length;
            var hashLengthShouldBe = BytesUsedByVersionTag + generator.HashBytesLength + generator.SaltBytesLength;


            if (hashLength != BytesUsedByVersionTag + generator.HashBytesLength + generator.SaltBytesLength)
            {
                throw ErrorFactory.Default.InvalidHashConfiguration(hashLength, hashLengthShouldBe, generator.Version);
            }
        }

        private void VerifyGenerator(int hashLength, IPasswordHashVersion generator)
        {
            var hashLengthShouldBe = BytesUsedByVersionTag + generator.HashBytesLength + generator.SaltBytesLength;
            if (hashLength != BytesUsedByVersionTag + generator.HashBytesLength + generator.SaltBytesLength)
            {
                throw ErrorFactory.Default.InvalidHashConfiguration(hashLength, hashLengthShouldBe, generator.Version);
            }
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
            byte[] returnBytes = new byte[BytesUsedByVersionTag + PasswordHash.Length + Salt.Length];

            byte[] versionBytes = BitConverter.GetBytes(Version);

            Array.Copy(versionBytes, returnBytes, BytesUsedByVersionTag);

            Array.Copy(PasswordHash, 0, returnBytes, BytesUsedByVersionTag, PasswordHash.Length);

            Array.Copy(Salt, 0, returnBytes, BytesUsedByVersionTag + PasswordHash.Length, Salt.Length);

            return returnBytes;
        }
    }
}