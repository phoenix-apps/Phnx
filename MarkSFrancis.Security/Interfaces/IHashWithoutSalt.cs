namespace MarkSFrancis.Security.Interfaces
{
    public interface IHashWithoutSalt : IHash
    {
        byte[] Hash(byte[] data);
    }
}