using System;
using System.Threading.Tasks;

namespace Phnx.Data.Seed
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
        public SeedAsync(Action seedAsync)
        {
            this.seedAsync = seedAsync;
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
