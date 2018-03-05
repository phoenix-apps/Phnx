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
    }
}