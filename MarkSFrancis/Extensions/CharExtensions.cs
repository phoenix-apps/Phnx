namespace MarkSFrancis.Extensions
{
    /// <summary>
    /// Extension methods for the base type <see cref="char"/>
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// Convert a given character to a <see cref="string"/>, with a number of times this character should be repeated in the new <see cref="string"/>
        /// </summary>
        /// <param name="character">The character to create the new string from</param>
        /// <param name="repeat">The number of times to put the <paramref name="character"/> in the new string</param>
        /// <returns></returns>
        public static string ToString(this char character, int repeat = 1)
        {
            return new string(character, repeat);
        }
    }
}