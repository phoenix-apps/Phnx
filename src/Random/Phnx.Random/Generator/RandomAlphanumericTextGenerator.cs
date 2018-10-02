using Phnx.Random.Generator.Interfaces;

namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="string"/> from only alphanumeric characters
    /// </summary>
    public class RandomAlphaNumericTextGenerator : IRandomGenerator<string>
    {
        /// <summary>
        /// All alphanumeric characters
        /// </summary>
        public static readonly char[] AlphaNumericChars =
        {
            '!', '\"', '£', '$', '%', '^', '&', '*', '(', ')', '\'', '-', '_', '=', '`', '¬', '|', '\\', ',', '<', '.', '>', '/', '?', ';', ':', '@', '~', '#', ']', '[', '}', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        private RandomIntGenerator RandomLengthGenerator { get; }

        /// <summary>
        /// Create a new instance of <see cref="RandomAlphaNumericTextGenerator"/>
        /// </summary>
        public RandomAlphaNumericTextGenerator()
        {
            RandomLengthGenerator = new RandomIntGenerator();
        }

        /// <summary>
        /// Get a random <see cref="string"/> of alphanumeric characters between 1 and 20 characters long
        /// </summary>
        /// <returns>A random <see cref="string"/> of alphanumeric characters between 1 and 20 characters long</returns>
        public string Get()
        {
            return Get(RandomLengthGenerator.Get(1, 20));
        }

        /// <summary>
        /// Get a random <see cref="string"/> of alphanumeric characters with a fixed length
        /// </summary>
        /// <param name="length">The length of the random <see cref="string"/> to generate</param>
        /// <returns>A random <see cref="string"/> of alphanumeric characters with a fixed length</returns>
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
