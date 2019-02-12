using System;
using System.Collections;
using System.Collections.Generic;

namespace Phnx.Data.Seeds
{
    /// <summary>
    /// A group of seeds, used to help setup and organise seed operations for a database
    /// </summary>
    public class SeedGroup : ISeed, ICollection<ISeed>
    {
        private readonly List<ISeed> _seeds;

        /// <summary>
        /// Create a new seed group with an empty range of seeds
        /// </summary>
        public SeedGroup()
        {
            _seeds = new List<ISeed>();
        }

        /// <summary>
        /// Create a new seed group from a range of seeds
        /// </summary>
        /// <param name="seeds">The seed group to initalise from</param>
        /// <exception cref="ArgumentNullException"><paramref name="seeds"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">One or more of the enties in <paramref name="seeds"/> is  <see langword="null"/></exception>
        public SeedGroup(params ISeed[] seeds) : this()
        {
            Add(seeds);
        }

        /// <summary>
        /// Create a new seed group from a range of seeds
        /// </summary>
        /// <param name="seeds">The seed group to initalise from</param>
        /// <exception cref="ArgumentNullException"><paramref name="seeds"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">One or more of the enties in <paramref name="seeds"/> is  <see langword="null"/></exception>
        public SeedGroup(IEnumerable<ISeed> seeds) : this()
        {
            Add(seeds);
        }

        /// <summary>
        /// The number of entries in this group
        /// </summary>
        public int Count => _seeds.Count;

        bool ICollection<ISeed>.IsReadOnly => false;

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

        void ICollection<ISeed>.Add(ISeed item)
        {
            Add(item);
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
        public SeedGroup Add(IEnumerable<ISeed> seeds)
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
        /// Run all the <see cref="_seeds"/>
        /// </summary>
        public void Run()
        {
            for (int i = 0; i < _seeds.Count; i++)
            {
                _seeds[i].Run();
            }
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
        public bool Contains(ISeed item)
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
        public void CopyTo(ISeed[] array, int arrayIndex)
        {
            _seeds.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific seed from the group
        /// </summary>
        /// <param name="item">The seed to remove from the group</param>
        /// <returns><see langword="true"/> if the seed is successfully removed, otherwise <see langword="false"/>. This method also returns <see langword="false"/> if the seed was not found in the group</returns>
        public bool Remove(ISeed item)
        {
            return _seeds.Remove(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the group
        /// </summary>
        /// <returns>An <see cref="IEnumerator{ISeedAsync}"/> for the group</returns>
        public IEnumerator<ISeed> GetEnumerator()
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
