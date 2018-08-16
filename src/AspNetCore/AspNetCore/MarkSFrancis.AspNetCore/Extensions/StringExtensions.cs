using Microsoft.AspNetCore.Html;
using System.Web;

namespace MarkSFrancis.AspNetCore.Extensions
{
    /// <summary>
    /// Extensions for <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Sanitise a string for HTML
        /// </summary>
        /// <param name="textToSanitise">The text to sanitise</param>
        /// <returns><paramref name="textToSanitise"/> sanitised for HTML</returns>
        public static string HtmlSanitise(this string textToSanitise)
        {
            return HttpUtility.HtmlEncode(textToSanitise);
        }

        /// <summary>
        /// Desanitise a string encoded for HTML
        /// </summary>
        /// <param name="textToDesanitise">The text to desanitise</param>
        /// <returns>The HTML encoded string <paramref name="textToDesanitise"/> desanitised</returns>
        public static string HtmlUnsanitise(this string textToDesanitise)
        {
            return HttpUtility.HtmlDecode(textToDesanitise);
        }

        /// <summary>
        /// Converts this string to a <see cref="HtmlString"/>, without sanitising its value
        /// </summary>
        /// <param name="text">The text to convert to a <see cref="HtmlString"/></param>
        /// <returns><paramref name="text"/> converted to a <see cref="HtmlString"/>, without sanitisation</returns>
        public static HtmlString ToHtmlString(this string text)
        {
            return new HtmlString(text);
        }
    }
}
