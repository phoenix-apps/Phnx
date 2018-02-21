namespace MarkSFrancis.Data
{
    /// <summary>
    /// Generate escaped, or unescaped data such as escaping commas or speech marks in csv files
    /// </summary>
    public static class Escaped
    {
        /// <summary>
        /// Escapes some given characters with a given escape character
        /// </summary>
        /// <param name="textToEscape">The text to escape</param>
        /// <param name="escapeChar">The character to use when escaping</param>
        /// <param name="escapeTheseChars">The characters that will be escaped</param>
        /// <returns></returns>
        public static string Escape(string textToEscape, char escapeChar, params char[] escapeTheseChars)
        {
            textToEscape = textToEscape.Replace(new string(escapeChar, 1), new string(escapeChar, 2));

            foreach(var charToEscape in escapeTheseChars)
            {
                textToEscape = textToEscape.Replace(new string(charToEscape, 1), escapeChar.ToString() + charToEscape);
            }

            return textToEscape;
        }

        /// <summary>
        /// Escapes some given characters with a given escape character
        /// </summary>
        /// <param name="escapedText">The text to unescape</param>
        /// <param name="escapeChar">The character that's been used when escaping</param>
        /// <param name="escapedTheseChars">The characters that have be escaped</param>
        /// <returns></returns>
        public static string Unescape(string escapedText, char escapeChar, params char[] escapedTheseChars)
        {
            foreach (var charEscaped in escapedTheseChars)
            {
                escapedText = escapedText.Replace(escapeChar.ToString() + charEscaped, new string(charEscaped, 1));
            }

            escapedText = escapedText.Replace(new string(escapeChar, 2), new string(escapeChar, 1));

            return escapedText;
        }
    }
}