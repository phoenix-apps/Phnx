using System;
using System.Threading.Tasks;

namespace Phnx.Data.Seeds
{
    /// <summary>
    /// A asyncronous seed defined by an <see cref="Action"/>
    /// </summary>
    public class SeedAsync : ISeedAsync
    {
        private readonly Action _seedAsync;

        /// <summary>
        /// Create a new seed with a method to call to seed
        /// </summary>
        /// <param name="seedAsync">The method to seed with</param>
        /// <exception cref="ArgumentNullException"><paramref name="seedAsync"/> is <see langword="null"/></exception>
        public SeedAsync(Action seedAsync)
        {
            _seedAsync = seedAsync ?? throw new ArgumentNullException(nameof(seedAsync));
        }

        /// <summary>
        /// Convert a <see cref="SeedAsync"/> to an <see cref="Action"/>
        /// </summary>
        /// <param name="seed">The seed to convert</param>
        public static implicit operator Action(SeedAsync seed)
        {
            if (seed is null)
            {
                return null;
            }

            return seed._seedAsync;
        }

        /// <summary>
        /// Convert an <see cref="Action"/> to a <see cref="SeedAsync"/>
        /// </summary>
        /// <param name="seed">The action to convert</param>
        public static implicit operator SeedAsync(Action seed)
        {
            if (seed is null)
            {
                return null;
            }

            return new SeedAsync(seed);
        }

        /// <summary>
        /// Run this seed asyncronously
        /// </summary>
        public Task RunAsync()
        {
            return Task.Run(_seedAsync);
        }
    }
}
