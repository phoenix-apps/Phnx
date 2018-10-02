namespace Phnx.Random.Generator.Interfaces
{
    /// <summary>
    /// Provides a way to generate random data of type <typeparamref name="T"/> where <typeparamref name="T"/> is numeric (int, decimal etc)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRandomNumberGenerator<T> : IRandomGenerator<T> where T : struct
    {
        /// <summary>
        /// Get a new random instance of <typeparamref name="T"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <typeparamref name="T"/> with the specified range</returns>
        T Get(T inclusiveMinValue, T inclusiveMaxValue);
    }
}