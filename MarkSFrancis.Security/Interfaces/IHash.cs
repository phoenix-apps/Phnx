namespace MarkSFrancis.Security.Interfaces
{
    public interface IHash
    {
        int IterationCount { get; }

        byte[] GenerateSalt();

        byte[] Hash(byte[] data, byte[] salt);
    }
}