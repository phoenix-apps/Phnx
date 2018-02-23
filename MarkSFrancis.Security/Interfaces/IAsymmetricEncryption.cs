namespace MarkSFrancis.Security.Interfaces
{
    public interface IAsymmetricEncryption : IEncryption
    {
        void CreateRandomKeys(out byte[] publicKey, out byte[] privateKey);
    }
}