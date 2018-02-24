namespace MarkSFrancis.Security.Passwords.Extensions
{
    public static class ErrorFactoryExtensions
    {
        public static DuplicateHashVersionException DuplicateHashVersion(this ErrorFactory factory, int hashVersion)
        {
            return new DuplicateHashVersionException($"{hashVersion} is already configured and cannot be re-added");
        }
    }
}
