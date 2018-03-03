using MarkSFrancis.Random.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Data.Generator
{
    /// <summary>
    /// Provides methods for generating a random job. Source: <see cref="Jobs"/>
    /// </summary>
    public class RandomJob : IRandomGenerator<string>
    {
        /// <summary>
        /// Get a random job name and optional experience level (such as Apprentice Automotive engineer, Senior Midwife or Insurance risk surveyor)
        /// </summary>
        /// <returns>A random job name and optional experience level</returns>
        public string Get()
        {
            var randomExperienceLevel = RandomHelper.GetInt(0, Jobs.ExperiencePrefix.Length);
            string randomExperience;

            if (randomExperienceLevel == Jobs.ExperiencePrefix.Length)
            {
                // Don't write experience level, exclude prefix
                randomExperience = "";
            }
            else
            {
                randomExperience = Jobs.ExperiencePrefix[randomExperienceLevel] + " ";
            }

            return 
                randomExperience + 
                RandomHelper.OneOf(Jobs.Titles);
        }
    }
}
