using System;
using System.ComponentModel;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace MarkSFrancis.Reflection.Extensions
{
    /// <summary>
    /// Extensions for <see cref="MemberInfo"/>
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>Retrieves a custom attribute of a specified type that is applied to a specified member.</summary>
        /// <param name="member">The member to inspect.</param>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <returns>A custom attribute that matches the type, or null if no such attribute is found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="member" /> is null. </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="member" /> is not a constructor, method, property, event, type, or field. </exception>
        /// <exception cref="AmbiguousMatchException">More than one of the requested attributes was found. </exception>
        /// <exception cref="TypeLoadException">A custom attribute type cannot be loaded. </exception>
        public static T GetAttribute<T>(this MemberInfo member) where T : Attribute
        {
            return member.GetCustomAttribute<T>();
        }


        /// <summary>
        /// Gets the display name for this member by looking for its <see cref="DisplayNameAttribute"/> and <see cref="DisplayAttribute"/>
        /// </summary>
        /// <param name="member">The member to get the display name of</param>
        /// <returns>The display name for this member</returns>
        public static string GetDisplayName(this MemberInfo member)
        {
            var displayName = member.GetAttribute<DisplayNameAttribute>();

            if (displayName != null)
            {
                return displayName.DisplayName;
            }

            var display = member.GetAttribute<DisplayAttribute>();

            if (display != null)
            {
                return display.Name;
            }

            return member.Name;
        }
    }
}