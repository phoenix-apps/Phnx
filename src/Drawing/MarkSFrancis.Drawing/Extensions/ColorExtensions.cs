using System.Diagnostics.Contracts;
using System.Drawing;

namespace MarkSFrancis.Drawing.Extensions
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
        /// <returns>The hex code for the given color (e.g "FF0000" for red)</returns>
        [Pure]
        public static Hexadecimal GetHex(this Color color)
        {
            return new Hexadecimal(color);
        }
    }
}
