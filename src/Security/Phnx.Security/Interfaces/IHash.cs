namespace Phnx.Security.Interfaces
{
    /// <summary>
    /// One way hashing
    /// </summary>
    public interface IHash
    {
        /// <summary>
        /// The number of times this hashing algorithm is ran on data
        /// </summary>
        int IterationCount { get; }
    }
}
