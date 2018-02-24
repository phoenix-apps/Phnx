using System;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Generator
{
    public class RandomDateTimeGenerator : IRandomNumberGenerator<DateTime>
    {
        public RandomDateTimeGenerator()
        {
            RandomLongGenerator = new RandomLongGenerator();
        }

        private RandomLongGenerator RandomLongGenerator { get; }

        public DateTime Get()
        {
            return Get(DateTime.MinValue, DateTime.MaxValue);
        }

        public DateTime Get(DateTime from, DateTime to)
        {
            var newDateTimeTicks = RandomLongGenerator.Get(from.Ticks, to.Ticks);
            return new DateTime(newDateTimeTicks);
        }
    }
}
