namespace MarkSFrancis.Security.Interfaces
{
    /// <summary>
    /// One way hashing without a salt. Useful for generating checksums for data at a fixed length
    /// </summary>
    public interface IHashWithoutSalt : IHash
    {
        /// <summary>
        /// Hash data
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <returns></returns>
        byte[] Hash(byte[] data);
    }
}