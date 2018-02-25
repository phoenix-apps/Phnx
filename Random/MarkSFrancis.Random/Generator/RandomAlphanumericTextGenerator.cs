using MarkSFrancis.Random.Generator.Interfaces;

namespace MarkSFrancis.Random.Generator
{
    public class RandomAlphaNumericTextGenerator : IRandomGenerator<string>
    {
        public char[] AlphaNumericChars { get; }

        private RandomIntGenerator RandomLengthGenerator { get; }

        public RandomAlphaNumericTextGenerator()
        {
            RandomLengthGenerator = new RandomIntGenerator();

            AlphaNumericChars = new[]
                {
                    '!', '\"', '£', '$', '%', '^', '&', '*', '(', ')', '\'', '-', '_', '=', '`', '¬', '|', '\\', ',', '<', '.', '>', '/', '?', ';', ':', '@', '~', '#', ']', '[', '}', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
                };
        }

        /// <summary>
        /// Generates random string between 1 and 20 characters long
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            return Get(RandomLengthGenerator.Get(1, 20));
        }

        /// <summary>
        /// Get random alphanumeric text of a certain length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string Get(int length)
        {
            string returnValue = "";
            for (int i = 0; i < length; i++)
            {
                returnValue += RandomHelper.OneOf(AlphaNumericChars);
            }
            return returnValue;
        }
    }
}
