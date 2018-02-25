namespace MarkSFrancis.Security.Interfaces
{
    /// <summary>
    /// One way hashing. This should be paired with either <see cref="IHashWithSalt"/>, or <see cref="IHashWithoutSalt"/>, depending on whether your hashing algorithm uses a salt. Implement both if your hashing algorithm supports both
    /// </summary>
    public interface IHash
    {
        /// <summary>
        /// The number of times this hashing algorithm is ran on data
        /// </summary>
        int IterationCount { get; }
    }
}
