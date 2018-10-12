using System;

namespace Phnx
{
    /// <summary>
    /// Provides a way of translating between cardinal numbers (1, 2, 353 etc) and ordinal numbers (1st, 2nd, 353rd)
    /// </summary>
    public static class Ordinals
    {
        /// <summary>
        /// Convert a number to its ordinal representation (e.g from 1 to 1st)
        /// </summary>
        /// <param name="number">The number to convert</param>
        /// <returns>The ordinal representation of <paramref name="number"/></returns>
        public static string ToOrdinal(int number)
        {
            if (number < 0)
            {
                // There is no ordinal version
                return number.ToString();
            }

            if (number % 100 > 10 && number % 100 < 20)
            {
                return $"{number}th";
            }

            string suffix;
            switch (number % 10)
            {
                case 1:
                    suffix = "st";
                    break;
                case 2:
                    suffix = "nd";
                    break;
                case 3:
                    suffix = "rd";
                    break;
                default:
                    suffix = "th";
                    break;
            }

            return number + suffix;
        }

        /// <summary>
        /// Convert from an ordinal to its original number (e.g from 354th to 354)
        /// </summary>
        /// <param name="ordinal">The ordinal to convert</param>
        /// <returns><paramref name="ordinal"/> converted to a number</returns>
        public static int FromOrdinal(string ordinal)
        {
            if (ordinal is null)
            {
                throw new ArgumentNullException(nameof(ordinal));
            }

            if (ordinal.StartsWith("-"))
            {
                return int.Parse(ordinal);
            }

            if (ordinal.Length < 2)
            {
                throw new FormatException(nameof(ordinal) + " is not a valid ordinal");
            }

            return int.Parse(ordinal.Substring(0, ordinal.Length - 2));
        }
    }
}
