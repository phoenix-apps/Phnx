using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkSFrancis.Reflection
{
    /// <summary>
    /// Adds new errors onto the <see cref="ErrorFactory"/>
    /// </summary>
    public static class ErrorFactoryExtensions
    {
        /// <summary>
        /// An error to describe that the <see cref="Expression"/> does not point to a field or property
        /// </summary>
        /// <param name="factory">The error factory that this method extends</param>
        /// <param name="paramName">The name of the <see cref="Expression"/> parameter</param>
        /// <returns></returns>
        public static ArgumentException InvalidExpression(this ErrorFactory factory, string paramName) =>
            new ArgumentException("Expression is not a field or property access", paramName);

        /// <summary>
        /// An error to describe that the <see cref="MemberInfo"/> does not point to a field or property
        /// </summary>
        /// <param name="factory">The error factory that this method extends</param>
        /// <param name="paramName">The name of the <see cref="MemberInfo"/> parameter</param>
        /// <returns></returns>
        public static ArgumentException InvalidMember(this ErrorFactory factory, string paramName) =>
            new ArgumentException("Member is not a field or property access", paramName);
    }
}
