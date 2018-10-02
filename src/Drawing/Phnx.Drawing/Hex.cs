using Phnx.Collections.Extensions;
using System;
using System.Drawing;

namespace Phnx.Drawing
{
    /// <summary>
    /// A representation of a hexidecimal data structure
    /// </summary>
    public struct Hex
    {
        /// <summary>
        /// Create a hex structure from a <see cref="T:char[]"/>
        /// </summary>
        /// <param name="data">The data to build the hex structure from</param>
        /// <exception cref="FormatException"><paramref name="data"/> contains one or more characters that are not a valid digit in hex</exception>
        public Hex(char[] data) : this(data is null ? null : new string(data))
        // Passing null to new string() gives string.Empty, not null
        {
        }

        /// <summary>
        /// Create a hex structure from a <see cref="byte"/>
        /// </summary>
        /// <param name="data">The data to build the hex structure from</param>
        public Hex(byte data)
        {
            HexCode = new[] { data };
        }

        /// <summary>
        /// Create a hex structure from a <see cref="T:byte[]"/>
        /// </summary>
        /// <param name="data">The data to build the hex structure from</param>
        public Hex(byte[] data)
        {
            HexCode = data;
        }

        /// <summary>
        /// Create a hex structure from a <see cref="string"/>
        /// </summary>
        /// <param name="data">The data to build the hex structure from</param>
        /// <exception cref="FormatException"><paramref name="data"/> contains one or more characters that are not a valid digit in hex</exception>
        public Hex(string data)
        {
            if (data is null)
            {
                HexCode = null;
            }
            else if (data.Length == 0)
            {
                HexCode = new byte[0];
            }
            else
            {
                HexCode = HexStringToBytes(data);
            }
        }

        /// <summary>
        /// Create a hex structure from a <see cref="Color"/>
        /// </summary>
        /// <param name="color">The color to build the hex structure from</param>
        /// <param name="includeAlpha">Whether the alpha property of the color should be included in the hex structure</param>
        public Hex(Color color, bool includeAlpha = true)
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

        /// <exception cref="FormatException"><paramref name="hexCode"/> contains a character that is not a valid digit in hex</exception>
        private static byte[] HexStringToBytes(string hexCode)
        {
            if (hexCode is null)
            {
                return null;
            }
            else if (hexCode.Length == 0)
            {
                return new byte[0];
            }

            int NumberChars = hexCode.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexCode.Substring(i, 2), 16);
            }

            return bytes;
        }

        /// <summary>
        /// The hex code
        /// </summary>
        public byte[] HexCode { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="HexCode"/> as a <see cref="string"/>
        /// </summary>
        /// <exception cref="FormatException">value contains a character that is not a valid digit in hex</exception>
        public string HexString
        {
            get
            {
                return HexCode?.ToHex();
            }
            set
            {
                HexCode = HexStringToBytes(value);
            }
        }

        /// <summary>
        /// Whether this hex structure represents a <see cref="Color"/>
        /// </summary>
        public bool IsValidColor => HexCode != null && (HexCode.Length == 3 || HexCode.Length == 4);

        /// <summary>
        /// Whether <see cref="HexCode"/> represents a <see cref="Color"/> and has an alpha channel
        /// </summary>
        /// <exception cref="InvalidCastException">This hex does not represent a color</exception>
        public bool IsColorWithAlpha
        {
            get
            {
                if (!IsValidColor)
                {
                    return false;
                }

                return HexCode.Length == 4;
            }
        }

        /// <summary>
        /// Converts to a <see cref="T:byte[]"/> by copying the contents of <see cref="HexCode"/>
        /// </summary>
        /// <returns>A <see cref="T:byte[]"/> representation of the hex code</returns>
        public byte[] ToBytes()
        {
            if (HexCode == null)
            {
                return null;
            }

            return HexCode.ShallowCopy();
        }

        /// <summary>
        /// Convert <see cref="HexCode"/> to a color. The first byte is taken as the alpha channel if it is 4 bytes long, or opaque if it is 3 bytes long
        /// </summary>
        /// <returns>A <see cref="Color"/> built from the components of the hex code</returns>
        /// <exception cref="InvalidCastException"><see cref="HexCode"/> does not represent a valid color</exception>
        public Color ToColor()
        {
            if (!IsValidColor)
            {
                string errorMessage = ErrorMessage.Factory.InvalidCast(nameof(HexCode), typeof(byte[]), typeof(Color));
                throw new InvalidCastException(errorMessage);
            }

            if (IsColorWithAlpha)
            {
                return Color.FromArgb(HexCode[0], HexCode[1], HexCode[2], HexCode[3]);
            }
            else
            {
                return Color.FromArgb(HexCode[0], HexCode[1], HexCode[2]);
            }
        }

        /// <summary>
        /// Convert <see cref="HexCode"/> to a color. The first byte is taken as the alpha channel if it is 4 bytes long, or opaque if it is 3 bytes long
        /// </summary>
        /// <param name="color">A <see cref="Color"/> built from the components of the hex code</param>
        /// <returns>Whether the conversion was successful</returns>
        public bool TryToColor(out Color color)
        {
            if (!IsValidColor)
            {
                color = default(Color);
                return false;
            }

            if (IsColorWithAlpha)
            {
                color = Color.FromArgb(HexCode[0], HexCode[1], HexCode[2], HexCode[3]);
                return true;
            }
            else
            {
                color = Color.FromArgb(HexCode[0], HexCode[1], HexCode[2]);
                return true;
            }
        }

        /// <summary>
        /// Convert to a hexidecimal string
        /// </summary>
        /// <returns>A <see cref="string"/> representation of the hex code</returns>
        public override string ToString()
        {
            return HexString;
        }
    }
}
