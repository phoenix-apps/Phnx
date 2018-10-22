using System.Text;

namespace Phnx.Security
{
    /// <summary>
    /// Extension for <see cref="IHashWithSalt"/> and <see cref="IHashWithoutSalt"/>
    /// </summary>
    public static class IHashExtensions
    {
        /// <summary>
        /// Hash text using encoding to transfer between the text and bytes
        /// </summary>
        /// <param name="encryption">The hashing algorithm to use</param>
        /// <param name="data">The data to hash</param>
        /// <param name="salt">The salt to use</param>
        /// <param name="encoding">The encoding to convert the text to bytes with</param>
        /// <returns></returns>
        public static byte[] Hash(this IHashWithSalt encryption, string data, byte[] salt, Encoding encoding)
        {
            var bytes = encoding.GetBytes(data);

            return encryption.Hash(bytes, salt);
        }

        /// <summary>
        /// Hash text using encoding to transfer between the text and bytes
        /// </summary>
        /// <param name="encryption">The hashing algorithm to use</param>
        /// <param name="data">The data to hash</param>
        /// <param name="encoding">The encoding to convert the text to bytes with</param>
        /// <returns></returns>
        public static byte[] Hash(this IHashWithoutSalt encryption, string data, Encoding encoding)
        {
            var bytes = encoding.GetBytes(data);

            return encryption.Hash(bytes);
        }
    }
}
