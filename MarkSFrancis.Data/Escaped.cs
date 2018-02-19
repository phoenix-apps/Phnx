namespace MarkSFrancis.Data
{
    /// <summary>
    /// Generate escaped data
    /// </summary>
    public static class Escaped
    {
        public static string Escape(string textToEscape, char escapeChar, params char[] escapeTheseChars)
        {
            textToEscape = textToEscape.Replace(new string(escapeChar, 1), new string(escapeChar, 2));

            foreach(var charToEscape in escapeTheseChars)
            {
                textToEscape = textToEscape.Replace(new string(charToEscape, 1), escapeChar.ToString() + charToEscape);
            }

            return textToEscape;
        }

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