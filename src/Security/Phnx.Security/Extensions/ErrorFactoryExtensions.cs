using MarkSFrancis;
using Phnx.Security.Passwords.Interface;
using System;

namespace Phnx.Security.Extensions
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

        /// <summary>
        /// An error to describe that the salt length is invalid
        /// </summary>
        /// <param name="factory">The factory to extend</param>
        /// <param name="hashLength">The length that the hash actually was</param>
        /// <param name="hashLengthShouldBe">The length that the hash should've been</param>
        /// <param name="generatorVersion">The hash generator version for the <see cref="IPasswordHashVersion"/> used</param>
        /// <returns>A <see cref="TypeLoadException"/> asking the user to check the <see cref="IPasswordHashVersion"/> and the hash</returns>
        public static TypeLoadException InvalidHashConfiguration(this ErrorFactory factory, int hashLength, int hashLengthShouldBe, int generatorVersion)
        {
            return new TypeLoadException($"The configuration for hash generator version {generatorVersion} does not match with the hash. The hash length should be {hashLengthShouldBe} but was {hashLength}. Please check that this hash belongs to the assigned {nameof(IPasswordHashVersion)}, and that the {nameof(IPasswordHashVersion.SaltBytesLength)} and {nameof(IPasswordHashVersion.HashBytesLength)} are correct");
        }
    }
}
