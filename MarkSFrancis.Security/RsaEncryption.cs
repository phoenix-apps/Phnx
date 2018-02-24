using System.Security.Cryptography;
using MarkSFrancis.Security.Interfaces;

namespace MarkSFrancis.Security
{
    public class RsaEncryption : IAsymmetricEncryption
    {
        public const int KeySize = 2048;

        public void CreateRandomKeys(out byte[] publicKey, out byte[] privateKey)
        {
            var provider = new RSACryptoServiceProvider(KeySize);

            publicKey = provider.ExportCspBlob(false);

            privateKey = provider.ExportCspBlob(true);
        }

        public byte[] Encrypt(byte[] data, byte[] publicKey)
        {
            var rsaServiceProvider = new RSACryptoServiceProvider();
            rsaServiceProvider.ImportCspBlob(publicKey);

            return rsaServiceProvider.Encrypt(data, true);
        }

        public byte[] Decrypt(byte[] encryptedData, byte[] privateKey)
        {
            var rsaServiceProvider = new RSACryptoServiceProvider();
            rsaServiceProvider.ImportCspBlob(privateKey);

            return rsaServiceProvider.Decrypt(encryptedData, true);
        }
    }
}
