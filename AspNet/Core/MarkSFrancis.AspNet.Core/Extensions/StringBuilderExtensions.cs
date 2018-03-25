using System.Text;
using Microsoft.AspNetCore.Html;

namespace MarkSFrancis.AspNet.Core.Extensions
{
    public static class StringBuilderExtensions
    {
        public static HtmlString ToHtmlString(this StringBuilder stringBuilder)
        {
            return stringBuilder.ToString().ToHtmlString();
        }
    }
}
