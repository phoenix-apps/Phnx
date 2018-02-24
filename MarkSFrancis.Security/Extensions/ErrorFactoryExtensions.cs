using System;

namespace MarkSFrancis.Security.Extensions
{
    public static class ErrorFactoryExtensions
    {
        public static ArgumentException InvalidSaltSize(this ErrorFactory factor, int saltLengthShouldBe,
            int saltLengthWas)
        {
            return new ArgumentException($"The given salt was an invalid length. The salt length should be {saltLengthShouldBe} but was {saltLengthWas}");
        }
    }
}
