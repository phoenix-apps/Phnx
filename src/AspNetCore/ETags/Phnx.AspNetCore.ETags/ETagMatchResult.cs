namespace Phnx.AspNetCore.ETags
{
    /// <summary>
    /// The result of an ETag's check for match
    /// </summary>
    public enum ETagMatchResult
    {
        /// <summary>
        /// The ETag was not present in the request
        /// </summary>
        ETagNotInRequest = 0,

        /// <summary>
        /// The ETags matched, either through a <see cref="StrongMatch"/>, or a <see cref="WeakMatch"/>
        /// </summary>
        Match = 1,

        /// <summary>
        /// The ETags did not match, either through a <see cref="StrongDoNotMatch"/>, or a <see cref="WeakDoNotMatch"/>
        /// </summary>
        DoNotMatch = 2,

        /// <summary>
        /// The ETags strongly match
        /// </summary>
        StrongMatch = 3,

        /// <summary>
        /// The ETags weakly match
        /// </summary>
        WeakMatch = 5,

        /// <summary>
        /// The ETags strongly do not match
        /// </summary>
        StrongDoNotMatch = 6,

        /// <summary>
        /// The ETags weakly do not match
        /// </summary>
        WeakDoNotMatch = 10
    }
}
