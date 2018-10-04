using System;
using System.Threading.Tasks;

namespace Phnx.Data.Seeds
{
    /// <summary>
    /// A asyncronous seed defined by an <see cref="Action"/>
    /// </summary>
    public class SeedAsync : ISeedAsync
    {
        private readonly Action seedAsync;

        /// <summary>
        /// Create a new seed with a method to call to seed
        /// </summary>
        /// <param name="seedAsync">The method to seed with</param>
        /// <exception cref="ArgumentNullException"><paramref name="seedAsync"/> is <see langword="null"/></exception>
        public SeedAsync(Action seedAsync)
        {
            this.seedAsync = seedAsync ?? throw new ArgumentNullException(nameof(seedAsync));
        }

        /// <summary>
        /// Run this seed asyncronously
        /// </summary>
        public Task RunAsync()
        {
            return Task.Run(seedAsync);
        }
    }
}
