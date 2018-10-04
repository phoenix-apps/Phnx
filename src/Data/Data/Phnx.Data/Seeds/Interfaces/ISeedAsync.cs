using System.Threading.Tasks;

namespace Phnx.Data.Seeds
{
    /// <summary>
    /// Provides an interface for asyncronously seeding databases
    /// </summary>
    public interface ISeedAsync
    {
        /// <summary>
        /// The data to seed
        /// </summary>
        Task RunAsync();
    }
}
