using Phnx.Security.Passwords;

namespace Phnx
{
    /// <summary>
    /// Extensions for <see cref="ErrorMessage"/>
    /// </summary>
    public static class SecurityErrorMessageExtensions
    {
        /// <summary>
        /// An error to describe that the salt length is invalid
        /// </summary>
        /// <param name="factory">The factory to extend</param>
        /// <param name="saltLengthShouldBe">The length that the salt should've been</param>
        /// <param name="saltLengthWas">The length that the salt actually was</param>
        /// 
        public static string InvalidSaltSize(this ErrorMessage factory, int saltLengthShouldBe,
            int saltLengthWas)
        {
            return $"The salt was an invalid length. The salt length should be {saltLengthShouldBe} but was {saltLengthWas}";
        }

        /// <summary>
        /// An error to describe that the salt length is invalid
        /// </summary>
        /// <param name="factory">The factory to extend</param>
        /// <param name="hashLength">The length that the hash actually was</param>
        /// <param name="hashLengthShouldBe">The length that the hash should've been</param>
        /// <returns>A message asking the developer to check the <see cref="IPasswordHash"/> and the hash</returns>
        public static string InvalidHashConfiguration(this ErrorMessage factory, int hashLength, int hashLengthShouldBe)
        {
            return $"The configuration for this hash generator does not match with the hash. The hash length should be {hashLengthShouldBe} but was {hashLength}. Please check that this hash belongs to the assigned {nameof(IPasswordHash)}, and that the {nameof(IPasswordHash.SaltBytesLength)} and {nameof(IPasswordHash.HashBytesLength)} are correct";
        }
    }
}
