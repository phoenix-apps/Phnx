using System;

namespace MarkSFrancis.Extensions.Numeric
{
    public static class DoubleExtensions
    {
        public static string ToMoney(this double d, string currencySymbol, bool symbolBeforeValue = true)
        {
            return symbolBeforeValue ? currencySymbol + Math.Round(d, 2) :
                Math.Round(d, 2) + currencySymbol;
        }

        public static double RoundToNearest(this double valueToRound, double toNearest)
        {
            return Math.Round(valueToRound / toNearest, MidpointRounding.AwayFromZero) * toNearest;
        }
    }
}