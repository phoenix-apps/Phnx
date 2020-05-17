using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace Phnx.Reflection
{
    /// <summary>
    /// Extensions for <see cref="PropertyInfo"/>
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Get whether this property is an auto property ({ get; set; })
        /// </summary>
        /// <param name="property">The property to check</param>
        /// <returns>Whether this property is an auto property</returns>
        /// <exception cref="ArgumentNullException"><paramref name="property"/> is <see langword="null"/></exception>
        /// <exception cref="SecurityException">The requested method is non-public and the caller does not have the <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect on this non-public method</exception>
        public static bool IsAuto(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            var getMethod = property.GetGetMethod(true);

            if (getMethod is null)
            {
                return false;
            }

            var compilerGeneratedAttributes = getMethod.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true);

            return compilerGeneratedAttributes.Length > 0;
        }
    }
}
