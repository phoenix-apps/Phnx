using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phnx.Data.Seeds
{
    /// <summary>
    /// A group of seeds, used to help setup and organise seed operations for a database
    /// </summary>
    public class SeedGroupAsync : ISeedAsync, ICollection<ISeedAsync>
    {
        private readonly List<ISeedAsync> _seeds;

        /// <summary>
        /// Create a new seed group with an empty range of seeds
        /// </summary>
        public SeedGroupAsync()
        {
            _seeds = new List<ISeedAsync>();
        }


        /// <summary>
        /// Create a new seed group from a range of seeds
        /// </summary>
        /// <param name="seeds">The seed group to initalise from</param>
        /// <exception cref="ArgumentNullException"><paramref name="seeds"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">One or more of the enties in <paramref name="seeds"/> is  <see langword="null"/></exception>
        public SeedGroupAsync(params ISeedAsync[] seeds) : this()
        {
            Add(seeds);
        }

        /// <summary>
        /// Create a new seed group from a range of seeds
        /// </summary>
        /// <param name="seeds">The seed group to initalise from</param>
        /// <exception cref="ArgumentNullException"><paramref name="seeds"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">One or more of the enties in <paramref name="seeds"/> is  <see langword="null"/></exception>
        public SeedGroupAsync(IEnumerable<ISeedAsync> seeds) : this()
        {
            Add(seeds);
        }

        /// <summary>
        /// The number of entries in this group
        /// </summary>
        public int Count => _seeds.Count;

        bool ICollection<ISeedAsync>.IsReadOnly => false;

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

        void ICollection<ISeedAsync>.Add(ISeedAsync item)
        {
            Add(item);
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

            // Optimise list
            if (_seeds.Capacity < _seeds.Count + seeds.Length)
            {
                int sizeShouldBe = _seeds.Capacity;
                if (sizeShouldBe <= 0)
                {
                    sizeShouldBe = 4;
                }

                while (sizeShouldBe < _seeds.Count + seeds.Length)
                {
                    sizeShouldBe *= 2;
                }

                _seeds.Capacity = sizeShouldBe;
            }

            foreach (var seed in seeds)
            {
                if (seed is null)
                {
                    throw new ArgumentException($"Entry {_seeds.Count} cannot be null", nameof(seeds));
                }
                _seeds.Add(seed);
            }

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

            foreach (var seed in seeds)
            {
                if (seed is null)
                {
                    throw new ArgumentException($"Entry {_seeds.Count} cannot be null", nameof(seeds));
                }
                _seeds.Add(seed);
            }

            return this;
        }

        /// <summary>
        /// Run all the seeds
        /// </summary>
        /// <param name="runParallel">Whether to run the seeds in parallel (<see langword="true"/>) or series (<see langword="false"/>)</param>
        public async Task RunAsync(bool runParallel)
        {
            if (runParallel)
            {
                await Task.Run(() => Parallel.ForEach(_seeds, async seed => await seed.RunAsync()));
            }
            else
            {
                await Task.Run(async () =>
                {
                    foreach (var seed in _seeds)
                    {
                        await seed.RunAsync();
                    }
                });
            }
        }

        /// <summary>
        /// Run all the seeds in series
        /// </summary>
        public Task RunAsync()
        {
            return RunAsync(false);
        }

        /// <summary>
        /// Removes all seeds
        /// </summary>
        public void Clear()
        {
            _seeds.Clear();
        }

        /// <summary>
        /// Determines whether a seed is in this group
        /// </summary>
        /// <param name="item">The seed to locate</param>
        /// <returns><see langword="true"/> if item is found in the group, otherwise <see langword="false"/></returns>
        public bool Contains(ISeedAsync item)
        {
            return _seeds.Contains(item);
        }

        /// <summary>
        /// Copies the group to a compatible one dimensional array, starting at <paramref name="arrayIndex"/>
        /// </summary>
        /// <param name="array">The array to copy to</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than zero</exception>
        /// <exception cref="ArgumentException">The number of seeds in this group is greater than the available space from <paramref name="arrayIndex"/> to the end of <paramref name="array"/></exception>
        public void CopyTo(ISeedAsync[] array, int arrayIndex)
        {
            _seeds.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific seed from the group
        /// </summary>
        /// <param name="item">The seed to remove from the group</param>
        /// <returns><see langword="true"/> if the seed is successfully removed, otherwise <see langword="false"/>. This method also returns <see langword="false"/> if the seed was not found in the group</returns>
        public bool Remove(ISeedAsync item)
        {
            return _seeds.Remove(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the group
        /// </summary>
        /// <returns>An <see cref="IEnumerator{ISeedAsync}"/> for the group</returns>
        public IEnumerator<ISeedAsync> GetEnumerator()
        {
            return _seeds.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the group
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> for the group</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _seeds.GetEnumerator();
        }
    }
}
