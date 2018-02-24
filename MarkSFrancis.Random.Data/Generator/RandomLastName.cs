using MarkSFrancis.Random.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Data.Generator
{
    public class RandomLastName : IRandomGenerator<string>
    {
        public string Get()
        {
            return RandomHelper.OneOf(Names.Surnames);
        }
    }
}
