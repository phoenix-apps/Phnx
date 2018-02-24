using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Generator
{
    public class RandomByteArray : IRandomGenerator<byte[]>
    {
        byte[] IRandomGenerator<byte[]>.Get()
        {
            return Get();
        }

        public byte[] Get(int length = 4)
        {
            byte[] randomBytes = new byte[length];

            RandomHelper.Random.NextBytes(randomBytes);

            return randomBytes;
        }
    }
}
