using System;

namespace MarkSFrancis.Extensions.Numeric
{
    /// <summary>
    /// Extensions for <see cref="byte"/>
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Round to the nearest value
        /// </summary>
        /// <param name="valueToRound">The value to be rounded</param>
        /// <param name="toNearest">The value to round to</param>
        /// <returns></returns>
        public static byte RoundToNearest(this byte valueToRound, byte toNearest)
        {
            return (byte)(Math.Round((decimal)valueToRound / toNearest, MidpointRounding.AwayFromZero) * toNearest);
        }

        /// <summary>
        /// Converts the byte to a hex code equivalent
        /// </summary>
        /// <param name="b">The byte to convert to hex</param>
        /// <returns>The equivalent hex code</returns>
        public static string ToHex(this byte b)
        {
            return BitConverter.ToString(new[] { b });
        }

        /// <summary>
        /// Converts the bytes to a hex code equivalent
        /// </summary>
        /// <param name="b">The bytes to convert to hex</param>
        /// <returns>The equivalent hex code</returns>
        public static string ToHex(this byte[] b)
        {
            if (b == null)
            {
                throw ErrorFactory.ArgumentNull(nameof(b)).Create();
            }

            return BitConverter.ToString(b).Replace("-", string.Empty);
        }
    }
}