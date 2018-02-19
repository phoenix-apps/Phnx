using System;

namespace MarkSFrancis.DotNetExtensions.Numeric
{
    public static class DecimalExtensions
    {
        public static string ToMoney(this decimal d, string currencySymbol, bool symbolBeforeValue = true)
        {
            return symbolBeforeValue ? currencySymbol + Math.Round(d, 2) : 
                Math.Round(d, 2) + currencySymbol;
        }

        public static decimal RoundToNearest(this decimal valueToRound, decimal toNearest)
        {
            return Math.Round(valueToRound / toNearest, MidpointRounding.AwayFromZero) * toNearest;
        }
    }
}