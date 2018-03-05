using MarkSFrancis.Random.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Data.Generator
{
    /// <summary>
    /// Provides methods for generating a random conjunction. Source: <see cref="WordTypes.Conjunctions"/>
    /// </summary>
    public class RandomConjunction : IRandomGenerator<string>
    {
        /// <summary>
        /// Get a random conjunction
        /// </summary>
        /// <returns>A random conjunction</returns>
        public string Get()
        {
            return RandomHelper.OneOf(WordTypes.Conjunctions);
        }
    }
}
