using System.Security.Cryptography;

namespace MarkSFrancis.Security
{
    /// <summary>
    /// Provides a way to generate secure random bytes
    /// </summary>
    public static class SecureRandomBytes
    {
        /// <summary>
        /// Generate a number of secure random bytes
        /// </summary>
        /// <param name="numberOfBytesToGenerate">The number of bytes to generate</param>
        /// <returns></returns>
        public static byte[] Generate(int numberOfBytesToGenerate)
        {
            var rnd = RandomNumberGenerator.Create();

            byte[] rndBytes = new byte[numberOfBytesToGenerate];
            rnd.GetBytes(rndBytes);

            return rndBytes;
        }
    }
}
