namespace Phnx.Security.Interfaces
{
    /// <summary>
    /// Two way encryption for encrypting and decrypting data using a key
    /// </summary>
    public interface IEncryption
    {
        /// <summary>
        /// Encrypt data using a key
        /// </summary>
        /// <param name="data">The data to encrypt</param>
        /// <param name="key">The key to use</param>
        /// <returns></returns>
        byte[] Encrypt(byte[] data, byte[] key);

        /// <summary>
        /// Decrypt data using a key
        /// </summary>
        /// <param name="encryptedData">The data to decrypt</param>
        /// <param name="key">The key to use</param>
        /// <returns></returns>
        byte[] Decrypt(byte[] encryptedData, byte[] key);
    }
}