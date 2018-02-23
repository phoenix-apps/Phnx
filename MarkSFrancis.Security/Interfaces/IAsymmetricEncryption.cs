namespace MarkSFrancis.Security.Interfaces
{
    public interface IAsymmetricEncryption
    {
        byte[] Encrypt(byte[] data, byte[] key);

        byte[] Decrypt(byte[] encryptedData, byte[] key);
    }
}
