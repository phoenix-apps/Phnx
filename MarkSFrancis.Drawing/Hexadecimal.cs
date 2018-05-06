using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using MarkSFrancis.Collections.Extensions;
using MarkSFrancis.Extensions.Numeric;
using MarkSFrancis.Drawing.Extensions;

namespace MarkSFrancis.Drawing
{
    /// <summary>
    /// A representation of a hexidecimal data structure
    /// </summary>
    public struct Hexadecimal
    {
        /// <summary>
        /// Create a hex structre from a <see cref="T:char[]"/>
        /// </summary>
        /// <param name="data">The data to build the hex structure from</param>
        /// <exception cref="FormatException"><paramref name="data"/> contains one or more characters that are not a valid digit in hex</exception>
        public Hexadecimal(char[] data)
        {
            HexCode = HexStringToBytes(new string(data));
        }

        /// <summary>
        /// Create a hex structre from a <see cref="byte"/>
        /// </summary>
        /// <param name="data">The data to build the hex structure from</param>
        public Hexadecimal(byte data)
        {
            HexCode = new[] { data };
        }

        /// <summary>
        /// Create a hex structre from a <see cref="T:byte[]"/>
        /// </summary>
        /// <param name="data">The data to build the hex structure from</param>
        public Hexadecimal(byte[] data)
        {
            HexCode = data;
        }

        /// <summary>
        /// Create a hex structre from a <see cref="string"/>
        /// </summary>
        /// <param name="data">The data to build the hex structure from</param>
        /// <exception cref="FormatException"><paramref name="data"/> contains one or more characters that are not a valid digit in hex</exception>
        public Hexadecimal(string data)
        {
            HexCode = HexStringToBytes(data);
        }

        /// <summary>
        /// Create a hex structre from a <see cref="Color"/>
        /// </summary>
        /// <param name="color">The color to build the hex structure from</param>
        /// <param name="includeAlpha">Whether the alpha property of the color should be included in the hex structure</param>
        public Hexadecimal(Color color, bool includeAlpha = false)
        {
            if (includeAlpha)
            {
                HexCode = new[] { color.A, color.R, color.G, color.B };
            }
            else
            {
                HexCode = new[] { color.R, color.G, color.B };
            }
        }

        private static byte[] HexStringToBytes(string hexCode)
        {
            int NumberChars = hexCode.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexCode.Substring(i, 2), 16);
            }

            return bytes;
        }

        private byte[] HexCode { get; }

        /// <summary>
        /// Whether this hex structure represents a valid hex color
        /// </summary>
        public bool IsValidColor => HexCode.Length == 3 || HexCode.Length == 4;

        /// <summary>
        /// Whether the color represented by this hex structure has a data segment that represents alpha
        /// </summary>
        /// <exception cref="InvalidCastException">This hexidecimal value does not represent a color</exception>
        public bool ColorHasAlpha {get{
            if(!IsValidColor){
                throw ErrorFactory.Default.HexIsNotAColor(this);
            }

            return HexCode.Length == 4;
        }}

        /// <summary>
        /// Converts to a <see cref="T:byte[]"/>
        /// </summary>
        /// <returns>A <see cref="T:byte[]"/> representation of the hex code</returns>
        [Pure]
        public byte[] ToBytes()
        {
            return HexCode.ShallowCopy();
        }

        /// <summary>
        /// Convert to a color. The first byte is taken as the alpha if it is 4 bytes long. If it is 3, the alpha is presumed as opaque
        /// </summary>
        /// <returns>A <see cref="Color"/> built from the components of the hex code</returns>
        /// <exception cref="InvalidCastException">The byte array is not 3 (no alpha) or 4 (with alpha) bytes long</exception>
        [Pure]
        public Color ToColor()
        {
            switch (HexCode.Length)
            {
                case 3:
                    return Color.FromArgb(HexCode[0], HexCode[1], HexCode[2]);
                case 4:
                    return Color.FromArgb(HexCode[0], HexCode[1], HexCode[2], HexCode[3]);
                default:
                    throw ErrorFactory.Default.InvalidCast(nameof(HexCode), typeof(byte[]), typeof(Color));
            }
        }

        /// <summary>
        /// Convert to a hexidecimal char array
        /// </summary>
        /// <returns>A <see cref="T:char[]"/> representation of the hex code</returns>
        [Pure]
        public char[] ToChars()
        {
            return ToString().ToCharArray();
        }

        /// <summary>
        /// Convert to a hexidecimal string
        /// </summary>
        /// <returns>A <see cref="string"/> representation of the hex code</returns>
        [Pure]
        public override string ToString()
        {
            return HexCode.ToHex();
        }
    }
}
