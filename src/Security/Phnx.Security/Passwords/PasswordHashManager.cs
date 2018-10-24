using Phnx.Collections;
using System.Collections.Generic;

namespace Phnx.Security.Passwords
{
    /// <summary>
    /// A central place for managing all your password hashes, including whether they should be updated to the latest algorithm
    /// </summary>
    public class PasswordHashManager
    {
        /// <summary>
        /// A collection of all the hash generator services that have been added through <see cref="Add"/> or the constructor
        /// </summary>
        protected IDictionary<int, IPasswordHashVersion> Generators { get; }

        /// <summary>
        /// The <see cref="Generators"/> with the highest version number
        /// </summary>
        protected IPasswordHashVersion LatestGenerator => Generators[LatestGeneratorVersion];

        /// <summary>
        /// The highest version number in the <see cref="Generators"/>
        /// </summary>
        public int LatestGeneratorVersion => Generators.MaxBy(g => g.Key).Key;

        /// <summary>
        /// Create a new instance of the <see cref="PasswordHashManager"/> from a collection of <see cref="IPasswordHashVersion"/>
        /// </summary>
        public PasswordHashManager()
        {
            Generators = new Dictionary<int, IPasswordHashVersion>();
        }

        public void Add(int version, IPasswordHashVersion generator)
        {
            Generators.Add(version, generator);
        }

        /// <summary>
        /// Gets whether a password matches a hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public bool PasswordMatchesHash(string password, byte[] hash)
        {
            var version = VersionedHash.GetVersionFromBytes(hash);
            var hashGenerator = Generators[version];

            var hashed = new VersionedHash(hash, hashGenerator);

            var hashedPassword = new VersionedHash(password, hashed.Salt, version, hashGenerator);

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

            return hashVersion < LatestGeneratorVersion;
        }

        /// <summary>
        /// Hash a password using the latest hash generator
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <returns></returns>
        public byte[] HashWithLatest(string password)
        {
            var latestVersion = LatestGeneratorVersion;
            VersionedHash pass = new VersionedHash(password, latestVersion, Generators[latestVersion]);

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