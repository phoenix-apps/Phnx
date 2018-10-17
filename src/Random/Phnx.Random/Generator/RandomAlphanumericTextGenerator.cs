using System.Text;

namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="string"/> from only alphanumeric characters
    /// </summary>
    public static class RandomAlphaNumericTextGenerator
    {
        /// <summary>
        /// All alphanumeric characters
        /// </summary>
        public static readonly char[] AlphaNumericChars =
        {
            '!', '\"', '£', '$', '%', '^', '&', '*', '(', ')', '\'', '-', '_', '=', '`', '¬', '|', '\\', ',', '<', '.', '>', '/', '?', ';', ':', '@', '~', '#', ']', '[', '}', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        /// <summary>
        /// Get a random <see cref="string"/> of alphanumeric characters between 1 and 20 characters long
        /// </summary>
        /// <returns>A random <see cref="string"/> of alphanumeric characters between 1 and 20 characters long</returns>
        public static string Get()
        {
            return Get(RandomIntGenerator.Get(1, 20));
        }

        /// <summary>
        /// Get a random <see cref="string"/> of alphanumeric characters with a fixed length
        /// </summary>
        /// <param name="length">The length of the random <see cref="string"/> to generate</param>
        /// <returns>A random <see cref="string"/> of alphanumeric characters with a fixed length</returns>
        public static string Get(int length)
        {
            StringBuilder returnValue = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                returnValue.Append(GetRandom.OneOf(AlphaNumericChars));
            }
            return returnValue.ToString();
        }
    }
}
