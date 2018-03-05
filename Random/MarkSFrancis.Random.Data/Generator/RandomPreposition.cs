using MarkSFrancis.Random.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Data.Generator
{
    /// <summary>
    /// Provides methods for generating a random preposition. Source: <see cref="WordTypes.Prepositions"/>
    /// </summary>
    public class RandomPreposition : IRandomGenerator<string>
    {
        /// <summary>
        /// Get a random preposition
        /// </summary>
        /// <returns>A random preposition</returns>
        public string Get()
        {
            return RandomHelper.OneOf(WordTypes.Prepositions);
        }
    }
}
