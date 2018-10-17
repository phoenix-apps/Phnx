namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="bool"/>
    /// </summary>
    public static class RandomBoolGenerator
    {
        /// <summary>
        /// Get a random <see cref="bool"/>
        /// </summary>
        /// <returns>A random <see cref="bool"/></returns>
        public static bool Get()
        {
            return GetRandom.StaticRandom.Next(0, 2) == 0;
        }
    }
}
