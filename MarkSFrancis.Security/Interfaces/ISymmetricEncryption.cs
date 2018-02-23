namespace MarkSFrancis.Security.Interfaces
{
    public interface ISymmetricEncryption
    {
        byte[] Encrypt(byte[] data, byte[] key);

        byte[] Decrypt(byte[] encryptedData, byte[] key);
    }
}