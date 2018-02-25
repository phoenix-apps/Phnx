using System;
using MarkSFrancis.Extensions;

namespace MarkSFrancis
{
    /// <summary>
    /// A set of methods to help with converting to and from objects
    /// </summary>
    public static class ConverterHelpers
    {
        /// <summary>
        /// If <typeparamref name="TConvertFrom"/> implements <typeparamref name="TConvertTo"/>, it returns a method that simply casts between the two. If not, this gets the default <see cref="IConvertible"/> implementation for converting to <typeparamref name="TConvertTo"/>
        /// </summary>
        /// <typeparam name="TConvertFrom">The type to convert from</typeparam>
        /// <typeparam name="TConvertTo">The type to convert to</typeparam>
        /// <returns></returns>
        public static Func<TConvertFrom, TConvertTo> GetDefaultConverter<TConvertFrom, TConvertTo>()
        {
            var TGetAsType = typeof(TConvertTo);
            if (typeof(TConvertFrom).Is(TGetAsType))
            {
                return convertFrom => (TConvertTo)(object)convertFrom;
            }

            return convertFrom => (TConvertTo)Convert.ChangeType(convertFrom, TGetAsType);
        }
    }
}
