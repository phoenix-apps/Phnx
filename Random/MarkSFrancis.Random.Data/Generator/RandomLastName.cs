using MarkSFrancis.Random.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Data.Generator
{
    /// <summary>
    /// Provides methods for generating a random last name. Source: <see cref="Names.Surnames"/>
    /// </summary>
    public class RandomLastName : IRandomGenerator<string>
    {
        /// <summary>
        /// Get a random last name
        /// </summary>
        /// <returns>A random last name</returns>
        public string Get()
        {
            return RandomHelper.OneOf(Names.Surnames);
        }
    }
}
