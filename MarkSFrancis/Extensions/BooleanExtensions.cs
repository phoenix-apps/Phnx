namespace MarkSFrancis.Extensions
{
    /// <summary>
    /// Extensions for <see cref="bool"/>
    /// </summary>
    public static class BooleanExtensions
    {
        /// <summary>
        /// Convert a <see cref="bool"/> to a string, with set values for what to display in each case
        /// </summary>
        /// <param name="condition">Whether to display <paramref name="trueText"/> or <paramref name="falseText"/></param>
        /// <param name="trueText">The text to return if <paramref name="condition"/> is <see langword="true"/></param>
        /// <param name="falseText">The text to return if <paramref name="condition"/> is <see langword="false"></see></param>
        /// <returns></returns>
        public static string ToString(this bool condition, string trueText, string falseText)
        {
            return condition ? trueText : falseText;
        }
    }
}