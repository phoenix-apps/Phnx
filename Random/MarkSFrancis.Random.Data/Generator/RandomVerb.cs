using MarkSFrancis.Random.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Data.Generator
{
    /// <summary>
    /// Provides methods for generating a random verb. Source: <see cref="WordTypes.Verbs"/>
    /// </summary>
    public class RandomVerb : IRandomGenerator<string>
    {
        /// <summary>
        /// Get a random verb
        /// </summary>
        /// <returns>A random verb</returns>
        public string Get()
        {
            return RandomHelper.OneOf(WordTypes.Verbs);
        }
    }
}
