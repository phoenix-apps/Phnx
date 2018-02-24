using MarkSFrancis.Random.Generator.Data.Source;
using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Generator.Data
{
    public class RandomLastName : IRandomGenerator<string>
    {
        public string Get()
        {
            return RandomHelper.OneOf(Names.Surnames);
        }
    }
}
