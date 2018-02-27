using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using MarkSFrancis.Reflection.Extensions;

namespace MarkSFrancis.Windows.Extensions.Reflection
{
    /// <summary>
    /// Extensions for <see cref="MemberInfo"/>
    /// </summary>
    public static class MemberInfoExtensions
    {
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