using System.Reflection;

namespace Phnx.Reflection
{
    /// <summary>
    /// Extensions for <see cref="PropertyFieldInfo{T,U}"/>
    /// </summary>
    public static class PropertyFieldInfoExtensions
    {
        /// <summary>
        /// Get whether this property field is an auto property (e.g { get; set; })
        /// </summary>
        /// <param name="propertyField">The property to check</param>
        /// <returns>Whether this property is an auto property</returns>
        public static bool IsAutoProperty<T, U>(this PropertyFieldInfo<T, U> propertyField)
        {
            if (!propertyField.IsProperty)
            {
                return false;
            }

            var property = (PropertyInfo)propertyField.Member;

            return property.IsAuto();
        }
    }
}