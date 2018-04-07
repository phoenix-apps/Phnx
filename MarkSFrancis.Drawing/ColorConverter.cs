using System.Diagnostics.Contracts;
using System.Drawing;

namespace MarkSFrancis.Drawing
{
    /// <summary>
    /// Provides methods for converting to and from <see cref="Color"/>
    /// </summary>
    public static class ColorConverter
    {
        /// <summary>
        /// Get the hex code for the color. Does not include a leading "#"
        /// </summary>
        /// <param name="color">The color to convert</param>
        /// <returns>The hex code for the given color (e.g "FF0000" for red)</returns>
        [Pure]
        public static string GetHexCode(Color color)
        {
            var hex = new Hexadecimal(color);

            return hex.ToString();
        }

        /// <summary>
        /// Get the color for a hex code
        /// </summary>
        /// <param name="hexCode">The hex code to convert</param>
        /// <returns>The color for the given hex code (e.g red for "FF0000")</returns>
        [Pure]
        public static Color FromHexCode(string hexCode)
        {
            var hex = new Hexadecimal(hexCode);

            return hex.ToColor();
        }
    }
}
