namespace MarkSFrancis.Security.Interfaces
{
    /// <summary>
    /// A symmetric encryption algorithm. Suitable for password protecting documents, but not suited for storing passwords or sending data to 3rd parties. Consider using <see cref="IHashWithSalt"/> for storing passwords, or <see cref="IAsymmetricEncryption"/> for sending data to 3rd parties. If you're using user created password keys instead of random keys, you'll need to hash the passwords to the algorithm's required key length first. Consider using <see cref="IHashWithoutSalt"/> for this
    /// </summary>
    public interface ISymmetricEncryption : IEncryption
    {
        /// <summary>
        /// Create a random secure key that can be used for this algorithm
        /// </summary>
        /// <returns></returns>
        byte[] CreateRandomKey();
    }
}