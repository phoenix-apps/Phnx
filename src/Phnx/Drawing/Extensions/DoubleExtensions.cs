using System;

namespace Phnx.Drawing
{
    /// <summary>
    /// Extensions for <see cref="double"/>
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Converts from degrees to radians
        /// </summary>
        /// <param name="degrees">The degrees to convert</param>
        /// <returns><paramref name="degrees"/> in radians</returns>
        public static double ToRadians(this double degrees)
        {
            return degrees / (180 / Math.PI);
        }

        /// <summary>
        /// Converts from radians to degrees
        /// </summary>
        /// <param name="radians">The radians to convert</param>
        /// <returns><paramref name="radians"/> in degrees</returns>
        public static double ToDegrees(this double radians)
        {
            return radians * (180 / Math.PI);
        }
    }
}
