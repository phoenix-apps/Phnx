namespace MarkSFrancis.Data.EFCore.Seed.Interfaces
{
    /// <summary>
    /// Provides an interface for asyncronous seeding of data
    /// </summary>
    public interface ISeed
    {
        /// <summary>
        /// The data to seed
        /// </summary>
        void Seed();
    }
}
