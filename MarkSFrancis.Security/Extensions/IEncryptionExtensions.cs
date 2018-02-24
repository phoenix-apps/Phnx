using System.Text;
using MarkSFrancis.Security.Interfaces;

namespace MarkSFrancis.Security.Extensions
{
    public static class IEncryptionExtensions
    {
        public static byte[] Encrypt(this IEncryption encryption, string data, byte[] key, Encoding encoding)
        {
            var bytes = encoding.GetBytes(data);

            return encryption.Encrypt(bytes, key);
        }

        public static string Decrypt(this IEncryption encryption, byte[] data, byte[] key, Encoding encoding)
        {
            var bytes = encryption.Decrypt(data, key);

            return encoding.GetString(bytes);
        }
    }
}
