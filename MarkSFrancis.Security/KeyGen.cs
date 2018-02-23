using System.Security.Cryptography;

namespace MarkSFrancis.Security
{
    public static class KeyGen
    {
        public static byte[] SecureRandomBytes(int numberOfBytesToGenerate)
        {
            var rnd = RandomNumberGenerator.Create();

            byte[] rndBytes = new byte[numberOfBytesToGenerate];
            rnd.GetBytes(rndBytes);

            return rndBytes;
        }
    }
}
