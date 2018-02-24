namespace MarkSFrancis.Security.Interfaces
{
    public interface IHashWithSalt : IHash
    {
        byte[] GenerateSalt();

        byte[] Hash(byte[] data, byte[] salt);
    }
}