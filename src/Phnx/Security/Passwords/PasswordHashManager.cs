using Phnx.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Phnx.Security.Passwords
{
    /// <summary>
    /// A central place for managing all your password hashes, including whether they should be updated to the latest algorithm
    /// </summary>
    public class PasswordHashManager : IDictionary<int, IPasswordHash>, IReadOnlyDictionary<int, IPasswordHash>
    {
        /// <summary>
        /// A collection of all the hash generators
        /// </summary>
        protected IDictionary<int, IPasswordHash> Generators { get; }

        /// <summary>
        /// The highest version number in the <see cref="Generators"/>
        /// </summary>
        public int LatestGeneratorVersion => Generators.MaxBy(g => g.Key).Key;

        /// <summary>
        /// Gets an <see cref="ICollection{T}"/> containing the known versions
        /// </summary>
        public ICollection<int> Keys => Generators.Keys;

        /// <summary>
        /// Gets an <see cref="ICollection{T}"/> containing the known <see cref="IPasswordHash"/>es
        /// </summary>
        public ICollection<IPasswordHash> Values => Generators.Values;

        /// <summary>
        /// Gets the number of known <see cref="IPasswordHash"/>es
        /// </summary>
        public int Count => Generators.Count;

        bool ICollection<KeyValuePair<int, IPasswordHash>>.IsReadOnly => Generators.IsReadOnly;

        IEnumerable<int> IReadOnlyDictionary<int, IPasswordHash>.Keys => Keys;

        IEnumerable<IPasswordHash> IReadOnlyDictionary<int, IPasswordHash>.Values => Values;

        /// <summary>
        /// Gets or sets the element with the specified version
        /// </summary>
        /// <param name="version">The version of <see cref="IPasswordHash"/> to get or set</param>
        /// <returns>The <see cref="IPasswordHash"/> with the specified version</returns>
        public IPasswordHash this[int version]
        {
            get => Generators[version];
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                Generators[version] = value;
            }
        }

        /// <summary>
        /// Create a new empty <see cref="PasswordHashManager"/>
        /// </summary>
        public PasswordHashManager()
        {
            Generators = new Dictionary<int, IPasswordHash>();
        }

        /// <summary>
        /// Add an <see cref="IPasswordHash"/> with a version number
        /// </summary>
        /// <param name="version">The version number for <paramref name="generator"/></param>
        /// <param name="generator">The <see cref="IPasswordHash"/> to use for this version of passwords</param>
        public void Add(int version, IPasswordHash generator)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            Generators.Add(version, generator);
        }

        /// <summary>
        /// Add a version and <see cref="IPasswordHash"/>
        /// </summary>
        /// <param name="item">The version and <see cref="IPasswordHash"/> to add</param>
        public void Add(KeyValuePair<int, IPasswordHash> item)
        {
            if (item.Value is null)
            {
                throw new ArgumentException($"{nameof(item)}.{nameof(item.Value)} cannot be null", nameof(item));
            }

            Generators.Add(item);
        }

        /// <summary>
        /// Gets whether a password matches a hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// 
        public bool PasswordMatchesHash(string password, byte[] hash)
        {
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (hash is null)
            {
                throw new ArgumentNullException(nameof(hash));
            }

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
        /// <returns><see langword="true"/> if a hash is using an old hashing algorithm, and should therefore be updated to the latest algorithm, otherwise <see langword="false"/></returns>
        public bool ShouldUpdateHash(byte[] hash)
        {
            if (hash is null)
            {
                throw new ArgumentNullException(nameof(hash));
            }

            int hashVersion = VersionedHash.GetVersionFromBytes(hash);

            return hashVersion < LatestGeneratorVersion;
        }

        /// <summary>
        /// Hash a password using the latest hash generator
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <returns><paramref name="password"/> hashed with the generator associated with the <see cref="LatestGeneratorVersion"/></returns>
        public byte[] HashWithLatest(string password)
        {
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var latestVersion = LatestGeneratorVersion;
            VersionedHash pass = new VersionedHash(password, latestVersion, Generators[latestVersion]);

            return pass.ToBytes();
        }

        /// <summary>
        /// Checks if the two byte arrays are exactly equal
        /// </summary>
        /// <param name="hash1">The first hash to compare</param>
        /// <param name="hash2">The second hash to compare</param>
        /// <returns><see langword="true"/> if <paramref name="hash1"/> is the same as <paramref name="hash2"/>, otherwise <see langword="false"/></returns>
        private bool HashesMatch(byte[] hash1, byte[] hash2)
        {
            return hash1.SequenceEqual(hash2);
        }

        /// <summary>
        /// Get whether a version is known
        /// </summary>
        /// <param name="key">The version to check for</param>
        /// 
        public bool ContainsKey(int key)
        {
            return Generators.ContainsKey(key);
        }

        /// <summary>
        /// Remove a version and its <see cref="IPasswordHash"/>
        /// </summary>
        /// <param name="key">The version to remove</param>
        /// <returns>Whether the version was removed</returns>
        public bool Remove(int key)
        {
            return Generators.Remove(key);
        }

        /// <summary>
        /// Gets the <see cref="IPasswordHash"/> associated with the specified version
        /// </summary>
        /// <param name="key">The version to get</param>
        /// <param name="value">The found <see cref="IPasswordHash"/>. If the version was not found, this is <see langword="null"/></param>
        /// <returns><see langword="true"/> if the version is found, otherwise <see langword="false"/></returns>
        public bool TryGetValue(int key, out IPasswordHash value)
        {
            return Generators.TryGetValue(key, out value);
        }

        /// <summary>
        /// Clear all versions and <see cref="IPasswordHash"/>es
        /// </summary>
        public void Clear()
        {
            Generators.Clear();
        }

        /// <summary>
        /// Get whether this contains a specific version and <see cref="IPasswordHash"/> pair
        /// </summary>
        /// <param name="item">The pair to check for</param>
        /// <returns>Whether this contains a specific version and <see cref="IPasswordHash"/> pair</returns>
        public bool Contains(KeyValuePair<int, IPasswordHash> item)
        {
            return Generators.Contains(item);
        }

        /// <summary>
        /// Copies the version and <see cref="IPasswordHash"/>es to a <see cref="T:[]"/>, starting at a particular index
        /// </summary>
        /// <param name="array">The destination array to copy to</param>
        /// <param name="arrayIndex">The destination array's index at which copying should begin</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than zero</exception>
        /// <exception cref="ArgumentException">The number of known password hashes is greater than the available space from <paramref name="arrayIndex"/> to the end of the <paramref name="array"/></exception>
        public void CopyTo(KeyValuePair<int, IPasswordHash>[] array, int arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }
            if (array.Length - arrayIndex < Generators.Count)
            {
                throw new ArgumentException($"The number of known password hashes is greater than the available space from {nameof(arrayIndex)} to the end of {nameof(array)}");
            }

            Generators.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Remove a specific version and <see cref="IPasswordHash"/> pair
        /// </summary>
        /// <param name="item">The pair to remove</param>
        /// <returns><see langword="true"/> if the specific version and <see cref="IPasswordHash"/> was removed successfully, otherwise <see langword="false"/></returns>
        public bool Remove(KeyValuePair<int, IPasswordHash> item)
        {
            return Generators.Remove(item);
        }

        /// <summary>
        /// Get an enumerator for all the version and <see cref="IPasswordHash"/>es
        /// </summary>
        /// <returns>An enumerator for all the version and <see cref="IPasswordHash"/>es</returns>
        public IEnumerator<KeyValuePair<int, IPasswordHash>> GetEnumerator()
        {
            return Generators.GetEnumerator();
        }

        /// <summary>
        /// Get an enumerator for all the version and <see cref="IPasswordHash"/>es
        /// </summary>
        /// <returns>An enumerator for all the version and <see cref="IPasswordHash"/>es</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
