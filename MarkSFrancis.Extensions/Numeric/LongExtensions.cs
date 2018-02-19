using System;

namespace MarkSFrancis.DotNetExtensions.Numeric
{
    public static class LongExtensions
    {
        public static string ToString(this long number, int minimumNumberOfDigitsToShow)
        {
            return number.ToString("d" + minimumNumberOfDigitsToShow);
        }

        public static long RoundToNearest(this long valueToRound, long toNearest)
        {
            return (long)(Math.Round((decimal)valueToRound / toNearest, MidpointRounding.AwayFromZero) * toNearest);
        }

        public static byte[] ToBytes(this long l)
        {
            return BitConverter.GetBytes(l);
        }
    }
}