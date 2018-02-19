using System;

namespace MarkSFrancis.DotNetExtensions.Numeric
{
    public static class ByteExtensions
    {
        public static byte Max(this byte a, byte b)
        {
            return Math.Max(a, b);
        }

        public static byte Min(this byte a, byte b)
        {
            return Math.Min(a, b);
        }

        public static byte RoundToNearest(this byte valueToRound, byte toNearest)
        {
            return (byte)(Math.Round((decimal)valueToRound / toNearest, MidpointRounding.AwayFromZero) * toNearest);
        }
    }
}