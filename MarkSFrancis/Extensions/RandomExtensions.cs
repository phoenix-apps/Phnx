using System;
using System.Collections.Generic;

namespace MarkSFrancis.Extensions
{
    public static class RandomExtensions
    {
        public static bool CoinToss(this Random rnd)
        {
            return rnd.Next(2) == 0;
        }

        public static T OneOf<T>(this Random rnd, params T[] values)
        {
            return values[rnd.Next(values.Length)];
        }

        public static T OneOf<T>(this Random rnd, IList<T> values)
        {
            return values[rnd.Next(values.Count)];
        }
    }
}