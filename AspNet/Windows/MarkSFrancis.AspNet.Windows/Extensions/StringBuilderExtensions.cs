using System.Text;
using System.Web;

namespace MarkSFrancis.AspNet.Windows.Extensions
{
    public static class StringBuilderExtensions
    {
        public static IHtmlString ToHtmlString(this StringBuilder stringBuilder)
        {
            return stringBuilder.ToString().ToHtmlString();
        }
    }
}
