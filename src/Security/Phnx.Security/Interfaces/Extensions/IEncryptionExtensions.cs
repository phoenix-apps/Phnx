using System.Text;

namespace Phnx.Security
{
    /// <summary>
    /// Extension for <see cref="IEncryption"/>
    /// </summary>
    public static class IEncryptionExtensions
    {
        /// <summary>
        /// Encrypt text using encoding to transfer between the text and bytes
        /// </summary>
        /// <param name="encryption">The encryption algorithm to use</param>
        /// <param name="data">The data to encrypt</param>
        /// <param name="key">The encryption key</param>
        /// <param name="encoding">The encoding to convert the text to bytes with</param>
        /// <returns></returns>
        public static byte[] Encrypt(this IEncryption encryption, string data, byte[] key, Encoding encoding)
        {
            var bytes = encoding.GetBytes(data);

            return encryption.Encrypt(bytes, key);
        }

        /// <summary>
        /// Decrypt text using encoding to transfer between the text and bytes
        /// </summary>
        /// <param name="encryption">The encryption algorithm to use</param>
        /// <param name="data">The data to decrypt</param>
        /// <param name="key">The encryption key</param>
        /// <param name="encoding">The encoding to convert the bytes to text with</param>
        /// <returns></returns>
        public static string Decrypt(this IEncryption encryption, byte[] data, byte[] key, Encoding encoding)
        {
            var bytes = encryption.Decrypt(data, key);

            return encoding.GetString(bytes);
        }
    }
}
