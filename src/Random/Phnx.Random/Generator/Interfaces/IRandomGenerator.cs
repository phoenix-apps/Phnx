namespace Phnx.Random.Generator.Interfaces
{
    /// <summary>
    /// Provides a way to generate random data of type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRandomGenerator<out T>
    {
        /// <summary>
        /// Get a new random instance of <typeparamref name="T"/>
        /// </summary>
        /// <returns>A random instance of <typeparamref name="T"/></returns>
        T Get();
    }
}