using System;

namespace Phnx.Data.Seeds
{
    /// <summary>
    /// A seed defined by an <see cref="Action"/>
    /// </summary>
    public class Seed : ISeed
    {
        private readonly Action seed;

        /// <summary>
        /// Create a new seed with a method to call to seed
        /// </summary>
        /// <param name="seed">The method to seed with</param>
        /// <exception cref="ArgumentNullException"><paramref name="seed"/> is <see langword="null"/></exception>
        public Seed(Action seed)
        {
            this.seed = seed ?? throw new ArgumentNullException(nameof(seed));
        }

        /// <summary>
        /// Run this seed
        /// </summary>
        public void Run()
        {
            seed();
        }
    }
}
