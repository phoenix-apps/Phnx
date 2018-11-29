using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Phnx.Console.Playground
{
    public static class Test
    {
        /// <summary>
        /// Get whether this property is an auto property (e.g { get; set; })
        /// </summary>
        /// <param name="property">The property to check</param>
        /// <returns>Whether this property is an auto property</returns>
        /// <exception cref="ArgumentNullException"><paramref name="property"/> is <see langword="null"/></exception>
        /// <exception cref="SecurityException">The requested method is non-public and the caller does not have <see cref="ReflectionPermission" /> to reflect on this non-public method</exception>
        public static bool IsAuto(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            var getMethod = property.GetGetMethod(true);

            if (getMethod == null)
            {
                return false;
            }

            return getMethod.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Length > 0;
        }
    }
}
