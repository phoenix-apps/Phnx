using System.Linq.Expressions;
using System.Reflection;

namespace Phnx
{
    /// <summary>
    /// Extensions for <see cref="ErrorMessage"/>
    /// </summary>
    internal static class ReflectionErrorMessageExtensions
    {
        /// <summary>
        /// An error to describe that the <see cref="Expression"/> does not point to a field or property
        /// </summary>
        /// <param name="factory">The error factory that this method extends</param>
        /// <returns></returns>
        public static string ExpressionIsNotPropertyOrFieldAccess(this ErrorMessage factory) =>
            "Expression is not a field or property access";

        /// <summary>
        /// An error to describe that the <see cref="MemberInfo"/> does not point to a field or property
        /// </summary>
        /// <param name="factory">The error factory that this method extends</param>
        /// <returns></returns>
        public static string MemberIsNotPropertyOrField(this ErrorMessage factory) =>
            "Member is not a field or property access";
    }
}
