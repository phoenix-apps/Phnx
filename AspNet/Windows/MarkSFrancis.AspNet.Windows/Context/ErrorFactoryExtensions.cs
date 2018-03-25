using System;

namespace MarkSFrancis.AspNet.Windows.Context
{
    /// <summary>
    /// Extensions for <see cref="ErrorFactory"/>
    /// </summary>
    public static class ErrorFactoryExtensions
    {
        public static ArgumentException InvalidCookieKey(this ErrorFactory factory, string cookieKey, string cookieName)
        {
            return new ArgumentException($"The cookie key must match the name of the cookie. The key was \"{cookieKey}\" and the cookie name was \"{cookieName}\"");
        }
    }
}
