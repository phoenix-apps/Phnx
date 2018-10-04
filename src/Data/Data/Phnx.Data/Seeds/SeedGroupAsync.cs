using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phnx.Data.Seeds
{
    /// <summary>
    /// A group of seeds, used to help setup and organise seed operations for a database
    /// </summary>
    public class SeedGroupAsync : ISeedAsync
    {
        /// <summary>
        /// Create a new seed group from a range of seeds
        /// </summary>
        /// <param name="seeds">The seed group to initalise from</param>
        /// <exception cref="ArgumentNullException"><paramref name="seeds"/> is <see langword="null"/></exception>
        public SeedGroupAsync(params ISeedAsync[] seeds)
        {
            _seeds = new List<ISeedAsync>(seeds) ?? throw new ArgumentNullException(nameof(seeds));
        }

        /// <summary>
        /// Create a new seed group from a range of seeds
        /// </summary>
        /// <param name="seeds">The seed group to initalise from</param>
        /// <exception cref="ArgumentNullException"><paramref name="seeds"/> is <see langword="null"/></exception>
        public SeedGroupAsync(IEnumerable<ISeedAsync> seeds)
        {
            _seeds = new List<ISeedAsync>(seeds) ?? throw new ArgumentNullException(nameof(seeds));
        }

        private readonly List<ISeedAsync> _seeds;

        /// <summary>
        /// Add a single seed
        /// </summary>
        /// <param name="seed">The seed to add</param>
        /// <returns>This seed group</returns>
        /// <exception cref="ArgumentNullException"><paramref name="seed"/> is <see langword="null"/></exception>
        public SeedGroupAsync Add(ISeedAsync seed)
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
        public SeedGroupAsync Add(params ISeedAsync[] seeds)
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
        public SeedGroupAsync Add(IEnumerable<ISeedAsync> seeds)
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
        /// <param name="runParallel">Whether to run the seeds in parallel (<see langword="true"/>) or series (<see langword="false"/>)</param>
        public void RunSync(bool runParallel)
        {
            var task = RunAsync(runParallel);

            task.Wait();
        }

        /// <summary>
        /// Run all the <see cref="_seeds"/>
        /// </summary>
        /// <param name="runParallel">Whether to run the seeds in parallel (<see langword="true"/>) or series (<see langword="false"/>)</param>
        public Task RunAsync(bool runParallel)
        {
            if (runParallel)
            {
                return Task.Run(() => Parallel.ForEach(_seeds, seed => seed.RunAsync()));
            }
            else
            {
                return Task.Run(() =>
                {
                    foreach (var seed in _seeds)
                    {
                        if (seed is null)
                        {
                            throw new NullReferenceException($"One or more seeds were null in this seed group");
                        }

                        var task = seed.RunAsync();
                        task.Wait();
                    }
                });
            }
        }

        /// <summary>
        /// Run all the <see cref="_seeds"/> in series
        /// </summary>
        public Task RunAsync()
        {
            return RunAsync(false);
        }
    }
}
