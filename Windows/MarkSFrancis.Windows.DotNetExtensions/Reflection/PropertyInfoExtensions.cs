using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MarkSFrancis.Windows.DotNetExtensions.Reflection
{
    public static class PropertyInfoExtensions
    {
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