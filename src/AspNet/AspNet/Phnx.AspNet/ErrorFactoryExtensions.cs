using MarkSFrancis;
using System;

namespace Phnx.AspNet
{
    /// <summary>
    /// Extensions for <see cref="ErrorFactory"/>
    /// </summary>
    public static class ErrorFactoryExtensions
    {
        /// <summary>
        /// Create a <see cref="NullReferenceException"/> describing that the <see cref="System.Web.HttpContext"/> cannot be <see langword="null"/>
        /// </summary>
        /// <param name="factory">The <see cref="ErrorFactory"/> to extend</param>
        /// <returns>A <see cref="NullReferenceException"/> describing that the <see cref="System.Web.HttpContext"/> cannot be <see langword="null"/></returns>
        public static NullReferenceException HttpContextRequired(this ErrorFactory factory)
        {
            return new NullReferenceException("A http context is required");
        }
    }
}
