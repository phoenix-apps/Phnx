using System;

namespace MarkSFrancis.DotNetExtensions
{
    public static class TypeExtensions
    {
        public static object CreateInstance(this Type t, params object[] args)
        {
            return Activator.CreateInstance(t, args);
        }
    }
}