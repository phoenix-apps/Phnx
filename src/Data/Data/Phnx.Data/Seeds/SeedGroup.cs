using System;
using System.Collections.Generic;

namespace Phnx.Data.Seeds
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
        /// <exception cref="ArgumentNullException"><paramref name="seeds"/> is <see langword="null"/></exception>
        public SeedGroup(params ISeed[] seeds)
        {
            _seeds = new List<ISeed>(seeds) ?? throw new ArgumentNullException(nameof(seeds));
        }

        /// <summary>
        /// Create a new seed group from a range of seeds
        /// </summary>
        /// <param name="seeds">The seed group to initalise from</param>
        /// <exception cref="ArgumentNullException"><paramref name="seeds"/> is <see langword="null"/></exception>
        public SeedGroup(IEnumerable<ISeed> seeds)
        {
            _seeds = new List<ISeed>(seeds) ?? throw new ArgumentNullException(nameof(seeds));
        }

        private readonly List<ISeed> _seeds;

        /// <summary>
        /// Add a single seed
        /// </summary>
        /// <param name="seed">The seed to add</param>
        /// <returns>This seed group</returns>
        /// <exception cref="ArgumentNullException"><paramref name="seed"/> is <see langword="null"/></exception>
        public SeedGroup Add(ISeed seed)
        {
            if (seed is null)
            {
                throw new ArgumentNullException(nameof(seed));
            }

            _seeds.Add(seed);
            return this;
        }

        /// <summary>
        /// Add a range of seeds
        /// </summary>
        /// <param name="seeds">The range of seeds to add</param>
        /// <returns>This seed group</returns>
        /// <exception cref="ArgumentNullException"><paramref name="seeds"/> is <see langword="null"/></exception>
        public SeedGroup Add(params ISeed[] seeds)
        {
            if (seeds is null)
            {
                throw new ArgumentNullException(nameof(seeds));
            }

            _seeds.AddRange(seeds);
            return this;
        }

        /// <summary>
        /// Add a range of seeds
        /// </summary>
        /// <param name="seeds">The range of seeds to add</param>
        /// <returns>This seed group</returns>
        /// <exception cref="ArgumentNullException"><paramref name="seeds"/> is <see langword="null"/></exception>
        public SeedGroup Add(IEnumerable<ISeed> seeds)
        {
            if (seeds is null)
            {
                throw new ArgumentNullException(nameof(seeds));
            }

            _seeds.AddRange(seeds);
            return this;
        }

        /// <summary>
        /// Run all the <see cref="_seeds"/>
        /// </summary>
        public void Run()
        {
            for (int i = 0; i < _seeds.Count; i++)
            {
                if (_seeds[i] is null)
                {
                    throw new NullReferenceException($"One or more seeds were null in this seed group");
                }

                _seeds[i].Run();
            }
        }
    }
}
