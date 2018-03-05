using MarkSFrancis.Random.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Data.Generator
{
    /// <summary>
    /// Provides methods for generating a random adverb. Source: <see cref="WordTypes.Adverbs"/>
    /// </summary>
    public class RandomAdverb : IRandomGenerator<string>
    {
        /// <summary>
        /// Get a random adverb
        /// </summary>
        /// <returns>A random adverb</returns>
        public string Get()
        {
            return RandomHelper.OneOf(WordTypes.Adverbs);
        }
    }
}
