using MarkSFrancis.Random.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Data.Generator
{
    /// <summary>
    /// Provides methods for generating a random adjective. Source: <see cref="WordTypes.Adjectives"/>
    /// </summary>
    public class RandomAdjective : IRandomGenerator<string>
    {
        /// <summary>
        /// Get a random adjective
        /// </summary>
        /// <returns>A random adjective</returns>
        public string Get()
        {
            return RandomHelper.OneOf(WordTypes.Adjectives);
        }
    }
}
