using System;

namespace MarkSFrancis.Extensions.Numeric
{
    public static class IntExtensions
    {
        public static string ToString(this int number, int minimumNumberOfDigitsToShow)
        {
            return number.ToString("d" + minimumNumberOfDigitsToShow);
        }

        public static int RoundToNearest(this int valueToRound, int toNearest)
        {
            return (int)(Math.Round((decimal)valueToRound / toNearest, MidpointRounding.AwayFromZero) * toNearest);
        }
    }
}