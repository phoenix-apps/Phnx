using MarkSFrancis.Random.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Data.Generator
{
    public class RandomAdjective : IRandomGenerator<string>
    {
        public string Get()
        {
            return RandomHelper.OneOf(WordTypes.Adjectives);
        }
    }
}
