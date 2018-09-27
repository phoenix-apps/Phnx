using MarkSFrancis.Data.EFCore.Seed.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkSFrancis.Data.EFCore.Seed
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
        public SeedGroupAsync(params ISeedAsync[] seeds) : this((IEnumerable<ISeedAsync>)seeds)
        {
        }

        /// <summary>
        /// Create a new seed group from a range of seeds
        /// </summary>
        /// <param name="seeds">The seed group to initalise from</param>
        public SeedGroupAsync(IEnumerable<ISeedAsync> seeds)
        {
            Seeds = new List<ISeedAsync>(seeds);
        }

        /// <summary>
        /// The collection of seeds in this seed group
        /// </summary>
        public List<ISeedAsync> Seeds { get; set; }

        /// <summary>
        /// Add a single seed
        /// </summary>
        /// <param name="seed">The seed to add</param>
        /// <returns>This seed group</returns>
        public SeedGroupAsync Add(ISeedAsync seed)
        {
            Seeds.Add(seed);
            return this;
        }

        /// <summary>
        /// Add a range of seeds
        /// </summary>
        /// <param name="seeds">The range of seeds to add</param>
        /// <returns>This seed group</returns>
        public SeedGroupAsync Add(params ISeedAsync[] seeds)
        {
            return Add((IEnumerable<ISeedAsync>)seeds);
        }

        /// <summary>
        /// Add a range of seeds
        /// </summary>
        /// <param name="seeds">The range of seeds to add</param>
        /// <returns>This seed group</returns>
        public SeedGroupAsync Add(IEnumerable<ISeedAsync> seeds)
        {
            Seeds.AddRange(seeds);
            return this;
        }

        /// <summary>
        /// Run all the <see cref="Seeds"/>
        /// </summary>
        /// <param name="runParallel"></param>
        public void SeedSync(bool runParallel)
        {
            var task = Seed(runParallel);

            task.Wait();
        }

        /// <summary>
        /// Run all the <see cref="Seeds"/>
        /// </summary>
        /// <param name="runParallel">Whether to run the seeds in parallel</param>
        public Task Seed(bool runParallel)
        {
            if (runParallel)
            {
                return Task.Run(() => Parallel.ForEach(Seeds, seed => seed.RunAsync()));
            }
            else
            {
                return Task.Run(() =>
                {
                    foreach (var seed in Seeds)
                    {
                        var task = seed.RunAsync();
                        task.Wait();
                    }
                });
            }
        }

        /// <summary>
        /// Run all the <see cref="Seeds"/> in series
        /// </summary>
        public Task RunAsync()
        {
            return Seed(false);
        }
    }
}
