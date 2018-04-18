namespace MarkSFrancis.AspNet.Core.Context.Interfaces
{
    /// <summary>
    /// A service for managing getting headers from a request, and setting headers in the response
    /// </summary>
    public interface IHeadersService
    {
        /// <summary>
        /// Get a header by key from the request
        /// </summary>
        /// <param name="key">The key to the header to get</param>
        /// <returns>The value of the specified header</returns>
        string Get(string key);

        /// <summary>
        /// Set a header in the response
        /// </summary>
        /// <param name="key">The key to the header to set</param>
        /// <param name="value">The value of the header</param>
        void Set(string key, string value);
    }
}