using System;

namespace MarkSFrancis.Extensions.Numeric
{
    /// <summary>
    /// Extensions for <see cref="double"/>
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Display the <paramref name="value"/> as money
        /// </summary>
        /// <param name="value">The value to display as money</param>
        /// <param name="currencySymbol">The currency symbol of the currency format to display</param>
        /// <param name="symbolBeforeValue">Whether the symbol should be placed before the value (such as $1.23 in the USA), or after (such as 1.23 € in France)</param>
        /// <returns></returns>
        public static string ToMoney(this double value, string currencySymbol, bool symbolBeforeValue = true)
        {
            if (currencySymbol == null)
            {
                throw new ArgumentNullException(nameof(currencySymbol));
            }

            var roundedValue = Math.Round(value, 2).ToString();

            int decimalsToPad;
            {
                var dotIndex = roundedValue.IndexOf('.');
                if (dotIndex < 0)
                {
                    decimalsToPad = 2;
                }
                else if (dotIndex == roundedValue.Length - 2)
                {
                    decimalsToPad = 1;
                }
                else
                {
                    decimalsToPad = 0;
                }
            }

            if (decimalsToPad == 2)
            {
                roundedValue += ".00";
            }
            else if (decimalsToPad == 1)
            {
                roundedValue += "0";
            }

            return symbolBeforeValue ? currencySymbol + roundedValue :
                roundedValue + currencySymbol;
        }

        /// <summary>
        /// Round to the nearest value
        /// </summary>
        /// <param name="valueToRound">The value to be rounded</param>
        /// <param name="toNearest">The value to round to</param>
        /// <returns></returns>
        public static double RoundToNearest(this double valueToRound, double toNearest)
        {
            return Math.Round(valueToRound / toNearest, MidpointRounding.AwayFromZero) * toNearest;
        }
    }
}