using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Generator
{
    public class RandomBoolGenerator : IRandomGenerator<bool>
    {
        public bool Get()
        {
            return RandomHelper.Random.Next(0, 2) == 0;
        }
    }
}
