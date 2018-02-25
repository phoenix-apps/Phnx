using MarkSFrancis.Random.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Data.Generator
{
    public class RandomJob : IRandomGenerator<string>
    {
        public string Get()
        {
            return 
                RandomHelper.OneOf(Jobs.ExperiencePrefix) + 
                RandomHelper.OneOf(Jobs.Titles);
        }
    }
}
