using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Collections.Extensions;
using MarkSFrancis.Security.Passwords.Extensions;
using MarkSFrancis.Security.Passwords.Interface;

namespace MarkSFrancis.Security.Passwords
{
    /// <summary>
    /// A central place for managing all your password hashes, including whether they should be updated to the latest algorithm
    /// </summary>
    public class PasswordHashManager
    {
        /// <summary>
        /// A collection of all the hash generator services that have been added through <see cref="AddHashGenerator"/> or the constructor
        /// </summary>
        protected List<IPasswordHashVersion> HashGeneratorServices { get; }

        /// <summary>
        /// The <see cref="HashGeneratorServices"/> with the highest version number
        /// </summary>
        protected IPasswordHashVersion LatestGenerateHashService => HashGeneratorServices.MaxBy(g => g.Version);

        /// <summary>
        /// Create a new instance of the <see cref="PasswordHashManager"/> from a collection of <see cref="IPasswordHashVersion"/>
        /// </summary>
        /// <param name="hashGenerators">The generators to register with the manager</param>
        public PasswordHashManager(params IPasswordHashVersion[] hashGenerators)
        {
            HashGeneratorServices = new List<IPasswordHashVersion>(hashGenerators);
        }
        
        /// <summary>
        /// Create a new instance of the <see cref="PasswordHashManager"/> from a collection of <see cref="IPasswordHashVersion"/>
        /// </summary>
        /// <param name="hashGenerators">The generators to register with the manager</param>
        public PasswordHashManager(IEnumerable<IPasswordHashVersion> hashGenerators)
        {
            HashGeneratorServices = new List<IPasswordHashVersion>(hashGenerators);
        }

        /// <summary>
        /// Gets the hash generator with a version number that matches the one used by the <paramref name="hash"/>
        /// </summary>
        /// <param name="hash">The hash to get the generator that was used</param>
        /// <returns></returns>
        private IPasswordHashVersion GetHashGeneratorFromHash(byte[] hash)
        {
            int hashVersion = VersionedHash.GetVersionFromBytes(hash);
            return HashGeneratorServices.First(h => h.Version == hashVersion);
        }

        /// <summary>
        /// Add a <see cref="IPasswordHashVersion"/> to the service
        /// </summary>
        /// <param name="hashGenerator">The hash generator to add</param>
        public void AddHashGenerator(IPasswordHashVersion hashGenerator)
        {
            if (HashGeneratorServices.Any(knownHashGenerator => knownHashGenerator.Version == hashGenerator.Version))
            {
                throw ErrorFactory.Default.DuplicateHashVersion(hashGenerator.Version);
            }

            HashGeneratorServices.Add(hashGenerator);
        }

        /// <summary>
        /// Gets whether a password matches a hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public bool PasswordMatchesHash(string password, byte[] hash)
        {
            var hashGenerator = GetHashGeneratorFromHash(hash);

            var hashed = new VersionedHash(hash, hashGenerator);

            var hashedPassword = new VersionedHash(password, hashed.Salt, hashGenerator);

            return HashesMatch(hashed.PasswordHash, hashedPassword.PasswordHash);
        }

        /// <summary>
        /// Gets whether a hash is using an old hashing algorithm, and should therefore be updated to the latest algorithm
        /// </summary>
        /// <param name="hash">The hash to check the version number of</param>
        /// <returns></returns>
        public bool ShouldUpdateHash(byte[] hash)
        {
            int hashVersion = VersionedHash.GetVersionFromBytes(hash);

            return hashVersion < LatestGenerateHashService.Version;
        }

        /// <summary>
        /// Hash a password using the latest hash generator
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <returns></returns>
        public byte[] HashPasswordWithLatestHashGenerator(string password)
        {
            VersionedHash pass = new VersionedHash(password, LatestGenerateHashService);

            return pass.ToBytes();
        }

        /// <summary>
        /// Checks if the two byte arrays are exactly equal
        /// </summary>
        /// <param name="hash1">The first hash to compare</param>
        /// <param name="hash2">The second hash to compare</param>
        /// <returns></returns>
        protected bool HashesMatch(byte[] hash1, byte[] hash2)
        {
            return hash1.EqualsRange(hash2);
        }
    }
}