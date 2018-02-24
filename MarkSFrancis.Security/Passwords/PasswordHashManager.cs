using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Collections.Extensions;
using MarkSFrancis.Security.Passwords.Extensions;
using MarkSFrancis.Security.Passwords.Interface;

namespace MarkSFrancis.Security.Passwords
{
    public sealed class PasswordHashManager
    {
        private List<IPasswordHashVersion> HashGeneratorServices { get; }

        private IPasswordHashVersion LatestGenerateHashService => HashGeneratorServices.MaxBy(g => g.Version);

        public PasswordHashManager(params IPasswordHashVersion[] hashGenerators)
        {
            HashGeneratorServices = new List<IPasswordHashVersion>(hashGenerators);
        }

        public PasswordHashManager(IEnumerable<IPasswordHashVersion> hashGenerators)
        {
            HashGeneratorServices = new List<IPasswordHashVersion>(hashGenerators);
        }

        private IPasswordHashVersion GetHashGeneratorFromHash(byte[] hash)
        {
            int hashVersion = VersionedHash.GetVersionFromBytes(hash);
            return HashGeneratorServices.First(h => h.Version == hashVersion);
        }

        public bool ShouldUpdateHash(byte[] hash)
        {
            int hashVersion = VersionedHash.GetVersionFromBytes(hash);

            return hashVersion < LatestGenerateHashService.Version;
        }

        public bool PasswordMatch(string password, byte[] hash)
        {
            var hashGenerator = GetHashGeneratorFromHash(hash);

            var hashed = new VersionedHash(hash, hashGenerator);

            var hashedPassword = new VersionedHash(password, hashed.Salt, hashGenerator);

            return HashesMatch(hashed.PasswordHash, hashedPassword.PasswordHash);
        }

        public byte[] HashPasswordWithLatestHashGenerator(string password)
        {
            VersionedHash pass = new VersionedHash(password, LatestGenerateHashService);

            return pass.ToBytes();
        }

        private bool HashesMatch(byte[] hash1, byte[] hash2)
        {
            if (hash1 == hash2)
            {
                return true;
            }

            if (hash1.Length != hash2.Length)
            {
                return false;
            }

            for (int index = 0; index < hash1.Length; index++)
            {
                if (hash1[index] != hash2[index])
                {
                    return false;
                }
            }

            return true;
        }

        public void AddHashGenerator(IPasswordHashVersion hashGenerator)
        {
            if (HashGeneratorServices.Any(knownHashGenerator => knownHashGenerator.Version == hashGenerator.Version))
            {
                throw ErrorFactory.Default.DuplicateHashVersion(hashGenerator.Version);
            }

            HashGeneratorServices.Add(hashGenerator);
        }
    }
}