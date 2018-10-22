namespace Phnx.Security
{
    /// <summary>
    /// One way hashing using a salt. Useful for storing passwords
    /// </summary>
    public interface IHashWithSalt : IHash
    {
        /// <summary>
        /// The length of the salt used by this hashing algorithm
        /// </summary>
        int SaltBytesLength { get; }

        /// <summary>
        /// Generate a random salt with a length appropriate for this hashing algorithm
        /// </summary>
        /// <returns></returns>
        byte[] GenerateSalt();

        /// <summary>
        /// Hash data using a salt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        byte[] Hash(byte[] data, byte[] salt);
    }
}