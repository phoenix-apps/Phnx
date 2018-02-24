using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Generator
{
    public class RandomLetterGenerator : IRandomGenerator<char>
    {
        public char[] UpperCaseAlphabet = new[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V', 'W', 'X', 'Y', 'Z'
        };

        public char[] LowerCaseAlphabet = new[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
            'v', 'w', 'x', 'y', 'z'
        };

        public char Get()
        {
            if (RandomHelper.GetBool())
            {
                // Get upper case
                return GetUpperCase();
            }
            else
            {
                // Get lower case
                return GetLowerCase();
            }
        }

        public char GetUpperCase()
        {
            return RandomHelper.OneOf(UpperCaseAlphabet);
        }

        public char GetLowerCase()
        {
            return RandomHelper.OneOf(LowerCaseAlphabet);
        }
    }
}
