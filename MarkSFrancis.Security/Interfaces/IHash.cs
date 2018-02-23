namespace MarkSFrancis.Security.Interfaces
{
    public interface IHash
    {
        byte[] Hash(byte[] data);
    }
}