using System.Security.Cryptography;
using MarkSFrancis.Security.Interfaces;

namespace MarkSFrancis.Security
{
    public class RsaEncryption : IEncryption
    {
        public void CreateRandomBlobs(out byte[] publicBlob, out byte[] privateBlob)
        {
            var provider = new RSACryptoServiceProvider(2048);

            publicBlob = provider.ExportCspBlob(false);

            privateBlob = provider.ExportCspBlob(true);
        }

        public byte[] Encrypt(byte[] data, byte[] publicRsaBlob)
        {
            var rsaServiceProvider = new RSACryptoServiceProvider();
            rsaServiceProvider.ImportCspBlob(publicRsaBlob);

            return rsaServiceProvider.Encrypt(data, true);
        }

        public byte[] Decrypt(byte[] encryptedData, byte[] privateRsaBlob)
        {
            var rsaServiceProvider = new RSACryptoServiceProvider();
            rsaServiceProvider.ImportCspBlob(privateRsaBlob);

            return rsaServiceProvider.Decrypt(encryptedData, true);
        }
    }
}
