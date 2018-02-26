using System;

namespace MarkSFrancis.Security.Extensions
{
    /// <summary>
    /// Extensions for <see cref="ErrorFactory"/>
    /// </summary>
    public static class ErrorFactoryExtensions
    {
        /// <summary>
        /// An error to describe that the salt length is invalid
        /// </summary>
        /// <param name="factory">The factory to extend</param>
        /// <param name="saltLengthShouldBe">The length that the salt should've been</param>
        /// <param name="saltLengthWas">The length that the salt actually was</param>
        /// <returns></returns>
        public static ArgumentException InvalidSaltSize(this ErrorFactory factory, int saltLengthShouldBe,
            int saltLengthWas)
        {
            return new ArgumentException($"The salt was an invalid length. The salt length should be {saltLengthShouldBe} but was {saltLengthWas}");
        }
    }
}
