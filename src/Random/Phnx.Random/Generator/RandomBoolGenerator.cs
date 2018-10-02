using Phnx.Random.Generator.Interfaces;

namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="bool"/>
    /// </summary>
    public class RandomBoolGenerator : IRandomGenerator<bool>
    {
        /// <summary>
        /// Get a random <see cref="bool"/>
        /// </summary>
        /// <returns>A random <see cref="bool"/></returns>
        public bool Get()
        {
            return RandomHelper.Random.Next(0, 2) == 0;
        }
    }
}
