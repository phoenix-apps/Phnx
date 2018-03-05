using MarkSFrancis.Random.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Data.Generator
{
    /// <summary>
    /// Provides methods for generating a random pronoun. Source: <see cref="WordTypes.Pronouns"/>
    /// </summary>
    public class RandomPronoun : IRandomGenerator<string>
    {
        /// <summary>
        /// Get a random pronoun
        /// </summary>
        /// <returns>A random pronoun</returns>
        public string Get()
        {
            return RandomHelper.OneOf(WordTypes.Pronouns);
        }
    }
}
