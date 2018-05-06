using System;

namespace MarkSFrancis.Drawing.Extensions
{
    public static class ErrorFactoryExtensions
    {
        /// <summary>
        /// Create an error describing that the given hex code is not a color
        /// </summary>
        /// <param name="errorFactory">The error factory to extend</param>
        /// <param name="hex">The hexidecimal that could not be converted to a color</param>
        public static InvalidCastException HexIsNotAColor(this ErrorFactory errorFactory, Hexadecimal hex)
        {
            return new InvalidCastException($"The given hex code \"{hex}\" is not a valid color");
        }
    }
}