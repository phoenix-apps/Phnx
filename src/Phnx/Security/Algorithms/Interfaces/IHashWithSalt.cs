using System;

namespace Phnx.Security.Algorithms
{
    /// <summary>
    /// One way hashing using a salt. Useful for storing passwords
    /// </summary>
    public interface IHashWithSalt
    {
        /// <summary>
        /// The length of the salt used by this hashing algorithm
        /// </summary>
        int SaltBytesLength { get; }

        /// <summary>
        /// Generate a random salt with a length appropriate for this hashing algorithm
        /// </summary>
        /// 
        byte[] GenerateSalt();

        /// <summary>
        /// Hash data using a salt
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <param name="salt">The salt to use. This must have the same length as <see cref="SaltBytesLength"/></param>
        /// <param name="numberOfIterations">The number of times the algorithm is ran on <paramref name="data"/></param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="salt"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="numberOfIterations"/> is less than zero</exception>
        byte[] Hash(byte[] data, byte[] salt, int numberOfIterations = 1);
    }
}