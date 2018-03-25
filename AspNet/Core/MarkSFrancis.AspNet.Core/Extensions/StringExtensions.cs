using System.Web;
using Microsoft.AspNetCore.Html;

namespace MarkSFrancis.AspNet.Core.Extensions
{
    /// <summary>
    /// Extensions for <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Sanitise a string for HTML
        /// </summary>
        /// <param name="textToSanitise"></param>
        /// <returns></returns>
        public static string HtmlSanitise(this string textToSanitise)
        {
            return HttpUtility.HtmlEncode(textToSanitise);
        }
        
        /// <summary>
        /// Desanitise a string encoded for HTML
        /// </summary>
        /// <param name="textToDesanitise"></param>
        /// <returns></returns>
        public static string HtmlUnsanitise(this string textToDesanitise)
        {
            return HttpUtility.HtmlDecode(textToDesanitise);
        }

        public static HtmlString ToHtmlString(this string text)
        {
            return new HtmlString(text);
        }
    }
}
