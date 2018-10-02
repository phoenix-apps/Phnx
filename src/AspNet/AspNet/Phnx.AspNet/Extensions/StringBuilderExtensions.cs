using System.Text;
using System.Web;

namespace Phnx.AspNet.Extensions
{
    /// <summary>
    /// Extensions for <see cref="StringBuilder"/>
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Export the value of this <see cref="StringBuilder"/> as a <see cref="HtmlString"/>
        /// </summary>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> to convert</param>
        /// <returns><paramref name="stringBuilder"/> converted to a <see cref="HtmlString"/></returns>
        public static HtmlString ToHtmlString(this StringBuilder stringBuilder)
        {
            return stringBuilder.ToString().ToHtmlString();
        }
    }
}
