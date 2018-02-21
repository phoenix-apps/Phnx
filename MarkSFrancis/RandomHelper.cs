using System;

namespace MarkSFrancis
{
    /// <summary>
    /// Contains helpers for generating random things (such as data)
    /// </summary>
    public static class RandomHelper
    {
        /// <summary>
        /// The static <see cref="Random"/> object. As this is created when the app opens, <see cref="Random"/> is better to use than recreating a new <see cref="Random"/> object each time you generate a value
        /// </summary>
        public static Random Random { get; } = new Random();
    }
}