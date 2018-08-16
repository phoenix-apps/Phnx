using System.Linq;
using System.Text;

namespace MarkSFrancis.Data
{
    /// <summary>
    /// Generate escaped, or unescaped data such as escaping commas or speech marks in csv files
    /// </summary>
    public static class Escaped
    {
        /// <summary>
        /// Escapes certain characters within text using an escape character
        /// </summary>
        /// <param name="textToEscape">The text to escape</param>
        /// <param name="escapeChar">The character to use when escaping</param>
        /// <param name="escapeTheseChars">The characters that will be escaped</param>
        /// <returns>The escaped string</returns>
        public static string Escape(string textToEscape, char escapeChar, params char[] escapeTheseChars)
        {
            StringBuilder escaped = new StringBuilder();

            foreach(var @char in textToEscape)
            {
                if (@char == escapeChar || escapeTheseChars.Contains(@char))
                {
                    escaped.Append(escapeChar);
                }

                escaped.Append(@char);
            }

            return escaped.ToString();
        }

        /// <summary>
        /// Unescapes certain characters within text, removing an escape character where appropriate
        /// </summary>
        /// <param name="escapedText">The text to unescape</param>
        /// <param name="escapeChar">The character that's been used when escaping</param>
        /// <param name="escapedTheseChars">The characters that have be escaped</param>
        /// <returns>The restored (unescaped) string</returns>
        public static string Unescape(string escapedText, char escapeChar, params char[] escapedTheseChars)
        {
            StringBuilder unescaped = new StringBuilder();

            bool lastCharWasEscapeChar = false;
            foreach (var @char in escapedText)
            {
                if (@char == escapeChar)
                {
                    if (lastCharWasEscapeChar)
                    {
                        unescaped.Append(@char);
                    }

                    lastCharWasEscapeChar = !lastCharWasEscapeChar;

                    continue;
                }

                unescaped.Append(@char);

                lastCharWasEscapeChar = false;
            }

            return unescaped.ToString();
        }
    }
}