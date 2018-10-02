using Phnx.Data.EFCore.Seed.Interfaces;
using System.Collections.Generic;

namespace Phnx.Data.EFCore.Seed
{
    /// <summary>
    /// A group of seeds, used to help setup and organise seed operations for a database
    /// </summary>
    public class SeedGroup : ISeed
    {
        /// <summary>
        /// Create a new seed group from a range of seeds
        /// </summary>
        /// <param name="seeds">The seed group to initalise from</param>
        public SeedGroup(params ISeed[] seeds) : this((IEnumerable<ISeed>)seeds)
        {
        }

        /// <summary>
        /// Create a new seed group from a range of seeds
        /// </summary>
        /// <param name="seeds">The seed group to initalise from</param>
        public SeedGroup(IEnumerable<ISeed> seeds)
        {
            Seeds = new List<ISeed>(seeds);
        }

        /// <summary>
        /// The collection of seeds in this seed group
        /// </summary>
        public List<ISeed> Seeds { get; set; }

        /// <summary>
        /// Add a single seed
        /// </summary>
        /// <param name="seed">The seed to add</param>
        /// <returns>This seed group</returns>
        public SeedGroup Add(ISeed seed)
        {
            Seeds.Add(seed);
            return this;
        }

        /// <summary>
        /// Add a range of seeds
        /// </summary>
        /// <param name="seeds">The range of seeds to add</param>
        /// <returns>This seed group</returns>
        public SeedGroup Add(params ISeed[] seeds)
        {
            return Add((IEnumerable<ISeed>)seeds);
        }

        /// <summary>
        /// Add a range of seeds
        /// </summary>
        /// <param name="seeds">The range of seeds to add</param>
        /// <returns>This seed group</returns>
        public SeedGroup Add(IEnumerable<ISeed> seeds)
        {
            Seeds.AddRange(seeds);
            return this;
        }

        /// <summary>
        /// Run all the <see cref="Seeds"/>
        /// </summary>
        public void Run()
        {
            for (int i = 0; i < Seeds.Count; i++)
            {
                Seeds[i].Run();
            }
        }
    }
}
