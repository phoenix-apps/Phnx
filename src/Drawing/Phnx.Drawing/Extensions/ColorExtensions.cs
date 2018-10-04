using System.Diagnostics.Contracts;
using System.Drawing;

namespace Phnx.Drawing
{
    /// <summary>
    /// Extensions for <see cref="Color"/>
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Get the hex code for the color. Does not include a leading "#"
        /// </summary>
        /// <param name="color">The color to convert</param>
        /// <param name="includeAlpha">Whether the alpha property of the color should be included in the hex structure</param>
        /// <returns>The hex code for the given color (e.g "FF0000" for red)</returns>
        [Pure]
        public static Hex ToHex(this Color color, bool includeAlpha)
        {
            return new Hex(color, includeAlpha);
        }
    }
}
