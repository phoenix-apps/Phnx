using System;

namespace Phnx
{
    /// <summary>
    /// Extensions for <see cref="long"/>
    /// </summary>
    public static class LongExtensions
    {
        /// <summary>
        /// Convert the <paramref name="number"/> to a <see cref="string"/> with a minimum number of digits that should be shown (e.g on a digital clock, time displays like 03:15, so the minimum number of digits for hours and minutes is 2)
        /// </summary>
        /// <param name="number">The number to convert to a <see cref="string"/></param>
        /// <param name="minimumNumberOfDigitsToShow"></param>
        /// 
        public static string ToString(this long number, int minimumNumberOfDigitsToShow)
        {
            return number.ToString("d" + minimumNumberOfDigitsToShow);
        }

        /// <summary>
        /// Round to the nearest value
        /// </summary>
        /// <param name="valueToRound">The value to be rounded</param>
        /// <param name="toNearest">The value to round to</param>
        /// 
        public static long RoundToNearest(this long valueToRound, long toNearest)
        {
            return (long)(Math.Round((decimal)valueToRound / toNearest, MidpointRounding.AwayFromZero) * toNearest);
        }
    }
}