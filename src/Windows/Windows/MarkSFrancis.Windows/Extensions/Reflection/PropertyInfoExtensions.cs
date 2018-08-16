using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MarkSFrancis.Windows.Extensions.Reflection
{
    /// <summary>
    /// Extensions for <see cref="PropertyInfo"/>
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Get whether this property is an auto property (e.g { get; set; })
        /// </summary>
        /// <param name="property">The property to check</param>
        /// <returns>Whether this property is an auto property</returns>
        public static bool IsAuto(this PropertyInfo property)
        {
            var getMethod = property.GetGetMethod(true);

            if (getMethod == null)
            {
                return false;
            }

            return getMethod.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any();
        }
    }
}
