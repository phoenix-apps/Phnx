using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Collections.Extensions;
using MarkSFrancis.Security.Passwords.Extensions;
using MarkSFrancis.Security.Passwords.Interface;

namespace MarkSFrancis.Security.Passwords
{
    public sealed class HashManagerService
    {
        private List<IHashGeneratorVersion> HashGeneratorServices { get; }

        private IHashGeneratorVersion LatestGenerateHashService => HashGeneratorServices.MaxBy(g => g.Version);

        public HashManagerService(params IHashGeneratorVersion[] hashGenerators)
        {
            HashGeneratorServices = new List<IHashGeneratorVersion>(hashGenerators);
        }

        public HashManagerService(IEnumerable<IHashGeneratorVersion> hashGenerators)
        {
            HashGeneratorServices = new List<IHashGeneratorVersion>(hashGenerators);
        }

        private IHashGeneratorVersion GetHashGeneratorFromHash(byte[] hash)
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

        public void AddHashGenerator(IHashGeneratorVersion hashGenerator)
        {
            if (HashGeneratorServices.Any(knownHashGenerator => knownHashGenerator.Version == hashGenerator.Version))
            {
                throw ErrorFactory.Default.DuplicateHashVersion(hashGenerator.Version);
            }

            HashGeneratorServices.Add(hashGenerator);
        }
    }
}