using System;
using System.Text;

namespace Phnx.Security.Passwords
{
    internal class VersionedHash
    {
        private static readonly Encoding DefaultPasswordEncoding = Encoding.Unicode;

        public const int BytesUsedByVersionTag = 4;

        public VersionedHash(string password, int version, IPasswordHash hashGenerator)
        {
            Version = version;
            Salt = hashGenerator.GenerateSalt();

            var passBytes = DefaultPasswordEncoding.GetBytes(password);
            PasswordHash = hashGenerator.GenerateHash(passBytes, Salt);

            VerifyGenerator(hashGenerator);
        }

        public VersionedHash(string password, byte[] salt, int version, IPasswordHash hashGenerator)
        {
            Version = version;
            Salt = salt;

            var passBytes = DefaultPasswordEncoding.GetBytes(password);
            PasswordHash = hashGenerator.GenerateHash(passBytes, salt);

            VerifyGenerator(hashGenerator);
        }

        public VersionedHash(byte[] bytes, IPasswordHash hashGenerator)
        {
            VerifyGenerator(bytes.Length, hashGenerator);

            Version = GetVersionFromBytes(bytes);

            PasswordHash = new byte[hashGenerator.HashBytesLength];
            Array.Copy(bytes, BytesUsedByVersionTag, PasswordHash, 0, PasswordHash.Length);

            Salt = new byte[hashGenerator.SaltBytesLength];
            Array.Copy(bytes, BytesUsedByVersionTag + PasswordHash.Length, Salt, 0, Salt.Length);
        }

        private void VerifyGenerator(IPasswordHash generator)
        {
            var hashLength = BytesUsedByVersionTag + PasswordHash.Length + Salt.Length;
            var hashLengthShouldBe = BytesUsedByVersionTag + generator.HashBytesLength + generator.SaltBytesLength;

            if (hashLength != hashLengthShouldBe)
            {
                string msg = ErrorMessage.Factory.InvalidHashConfiguration(hashLength, hashLengthShouldBe);

                throw new TypeLoadException(msg);
            }
        }

        private void VerifyGenerator(int hashLength, IPasswordHash generator)
        {
            var hashLengthShouldBe = BytesUsedByVersionTag + generator.HashBytesLength + generator.SaltBytesLength;
            if (hashLength != hashLengthShouldBe)
            {
                string msg = ErrorMessage.Factory.InvalidHashConfiguration(hashLength, hashLengthShouldBe);

                throw new ArgumentException(msg, nameof(generator));
            }
        }

        public static int GetVersionFromBytes(byte[] bytes)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
            if (bytes.Length < BytesUsedByVersionTag)
            {
                throw new ArgumentException($"{bytes} is an invalid versioned hash", nameof(bytes));
            }

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
