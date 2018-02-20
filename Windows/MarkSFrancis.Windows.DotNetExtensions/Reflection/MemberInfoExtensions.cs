using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using MarkSFrancis.Extensions.Reflection;

namespace MarkSFrancis.Windows.Extensions.Reflection
{
    public static class MemberInfoExtensions
    {
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