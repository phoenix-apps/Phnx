using System;

namespace Phnx.AspNet
{
    /// <summary>
    /// Extensions for <see cref="ErrorMessage"/>
    /// </summary>
    public static class ErrorMessageExtensions
    {
        /// <summary>
        /// Create a <see cref="NullReferenceException"/> describing that the <see cref="System.Web.HttpContext"/> cannot be <see langword="null"/>
        /// </summary>
        /// <param name="factory">The <see cref="ErrorMessage"/> to extend</param>
        /// <returns>A message describing that the <see cref="System.Web.HttpContext"/> cannot be <see langword="null"/></returns>
        public static string HttpContextRequired(this ErrorMessage factory)
        {
            return "A http context is required";
        }
    }
}
