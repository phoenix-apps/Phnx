using System.Text;
using MarkSFrancis.Security.Interfaces;

namespace MarkSFrancis.Security.Extensions
{
    public static class IHashExtensions
    {
        public static byte[] Encrypt(this IHashWithSalt encryption, string data, byte[] salt, Encoding encoding)
        {
            var bytes = encoding.GetBytes(data);

            return encryption.Hash(bytes, salt);
        }
        
        public static byte[] Encrypt(this IHashWithoutSalt encryption, string data, Encoding encoding)
        {
            var bytes = encoding.GetBytes(data);

            return encryption.Hash(bytes);
        }
    }
}
