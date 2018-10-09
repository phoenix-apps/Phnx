using System.Diagnostics.Contracts;
using System.Drawing;

namespace Phnx.Drawing
{
    /// <summary>
    /// Provides methods for converting to and from <see cref="Color"/> and hex
    /// </summary>
    public static class HexColorConverter
    {
        /// <summary>
        /// Get the hex code for the color. Does not include a leading "#"
        /// </summary>
        /// <param name="color">The color to convert</param>
        /// <param name="includeAlpha">Whether the alpha property of the color should be included in the hex</param>
        /// <returns>The hex code for the given color (e.g "FF0000" for red)</returns>
        [Pure]
        public static string GetHexCode(Color color, bool includeAlpha)
        {
            var hex = new Hex(color, includeAlpha);

            return hex.ToString();
        }

        /// <summary>
        /// Get the color for a hex code
        /// </summary>
        /// <param name="hexCode">The hex code to convert</param>
        /// <returns>The color for the given hex code (e.g red for "FF0000")</returns>
        [Pure]
        public static Color GetColor(string hexCode)
        {
            var hex = new Hex(hexCode);

            return hex.ToColor();
        }
    }
}
