using System;
using Phnx.Random.Generator.Interfaces;

namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="DateTime"/>
    /// </summary>
    public class RandomDateTimeGenerator : IRandomNumberGenerator<DateTime>
    {
        /// <summary>
        /// Create a new instance of <see cref="RandomDateTimeGenerator"/>
        /// </summary>
        public RandomDateTimeGenerator()
        {
            RandomLongGenerator = new RandomLongGenerator();
        }

        private RandomLongGenerator RandomLongGenerator { get; }

        /// <summary>
        /// Get a random <see cref="DateTime"/>
        /// </summary>
        /// <returns>A random <see cref="DateTime"/></returns>
        public DateTime Get()
        {
            return Get(DateTime.MinValue, DateTime.MaxValue);
        }

        /// <summary>
        /// Get a new random instance of <see cref="DateTime"/> within a limited range
        /// </summary>
        /// <param name="from">The inclusive minimum value to generate</param>
        /// <param name="to">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="DateTime"/> with the specified range</returns>
        public DateTime Get(DateTime from, DateTime to)
        {
            var newDateTimeTicks = RandomLongGenerator.Get(from.Ticks, to.Ticks);
            return new DateTime(newDateTimeTicks);
        }
    }
}
