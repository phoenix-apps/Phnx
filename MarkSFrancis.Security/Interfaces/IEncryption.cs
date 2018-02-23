namespace MarkSFrancis.Security.Interfaces
{
    public interface IEncryption
    {
        byte[] Encrypt(byte[] data, byte[] key);

        byte[] Decrypt(byte[] encryptedData, byte[] key);
    }
}