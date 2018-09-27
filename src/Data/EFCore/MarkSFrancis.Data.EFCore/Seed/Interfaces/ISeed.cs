namespace MarkSFrancis.Data.EFCore.Seed.Interfaces
{
    /// <summary>
    /// Provides an interface for seeding databases
    /// </summary>
    public interface ISeed
    {
        /// <summary>
        /// Run this seed
        /// </summary>
        void Run();
    }
}
