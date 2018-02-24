namespace MarkSFrancis.Security.Passwords.Interface
{
    public interface IHashGeneratorVersion
    {
        int Version { get; }

        int HashBytesLength { get; }

        int SaltBytesLength { get; }

        byte[] GenerateHash(byte[] password, byte[] salt);

        byte[] GenerateSalt();
    }
}
