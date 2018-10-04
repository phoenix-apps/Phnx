using System;

namespace Phnx.Data.Seed
{
    /// <summary>
    /// Extension methods for <see cref="ISeed"/>
    /// </summary>
    public static class ISeedExtensions
    {
        /// <summary>
        /// Convert this <see cref="ISeed"/> to an <see cref="ISeedAsync"/>
        /// </summary>
        /// <param name="seed">The seed to convert</param>
        /// <returns>An <see cref="ISeed"/> as an <see cref="ISeedAsync"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="seed"/> was <see langword="null"/></exception>
        public static ISeedAsync ToAsync(this ISeed seed)
        {
            if (seed is null)
            {
                throw new ArgumentNullException(nameof(seed));
            }

            return new SeedAsync(seed.Run);
        }
    }
}
