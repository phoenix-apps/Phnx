namespace MarkSFrancis.Security.Interfaces
{
    /// <summary>
    /// One way hashing using a salt
    /// </summary>
    public interface IHashWithSalt : IHash
    {
        int SaltBytesLength { get; }

        byte[] GenerateSalt();

        /// <summary>
        /// Hash given data using a salt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        byte[] Hash(byte[] data, byte[] salt);
    }
}