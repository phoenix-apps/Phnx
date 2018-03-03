using MarkSFrancis.Random.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Data.Generator
{
    /// <summary>
    /// Provides methods for generating a random first name. Source: <see cref="Names.MaleFirstNames"/> and <see cref="Names.FemaleFirstNames"/> 
    /// </summary>
    public class RandomFirstName : IRandomGenerator<string>
    {
        /// <summary>
        /// Get a random first name
        /// </summary>
        /// <returns>A random first name</returns>
        public string Get()
        {
            return RandomHelper.GetBool() ? GetMale() : GetFemale();
        }

        /// <summary>
        /// Get a random male first name
        /// </summary>
        /// <returns>A random male first name</returns>
        public string GetMale()
        {
            return RandomHelper.OneOf(Names.MaleFirstNames);
        }
        
        /// <summary>
        /// Get a random female first name
        /// </summary>
        /// <returns>A random female first name</returns>
        public string GetFemale()
        {
            return RandomHelper.OneOf(Names.FemaleFirstNames);
        }
    }
}
