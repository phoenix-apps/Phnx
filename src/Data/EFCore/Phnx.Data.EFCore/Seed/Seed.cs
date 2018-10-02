using Phnx.Data.EFCore.Seed.Interfaces;
using System;

namespace Phnx.Data.EFCore.Seed
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
        public Seed(Action seed)
        {
            this.seed = seed;
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
