using System;

namespace MarkSFrancis.Reflection
{
    internal static class ErrorFactory
    {
        public static ArgumentException InvalidExpression(string paramName) =>
            new ArgumentException("Expression is not a field or property access", paramName);
        
        public static ArgumentException InvalidMember(string paramName) =>
            new ArgumentException("Member is not a field or property access", paramName);
    }
}
