namespace MarkSFrancis.Security.Interfaces
{
    public interface ISymmetricEncryption : IEncryption
    {
        byte[] CreateRandomKey();
    }
}