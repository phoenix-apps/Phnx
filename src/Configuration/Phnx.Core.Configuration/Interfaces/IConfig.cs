namespace Phnx.Configuration
{
    /// <summary>
    /// Provides methods for loading from the application configuration, including default values and converting the configuration to the needed types
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Get a configuration by key as a string
        /// </summary>
        /// <param name="key">The key to the configuration to load</param>
        /// <returns>The value associated with the given configuration key</returns>
        string this[string key] { get; }

        /// <summary>
        /// Get a connection string from the configuration
        /// </summary>
        /// <param name="key">The key to the connection string to load</param>
        /// <returns>The connection string associated with the given key</returns>
        string GetConnectionString(string key);

        /// <summary>
        /// Get a configuration by key as <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of the value for the key to load</typeparam>
        /// <param name="key">The key to the configuration to load</param>
        /// <returns>The value associated with the given configuration key, converted to <typeparamref name="T"/></returns>
        T Get<T>(string key);

        /// <summary>
        /// Get a configuration by key as <typeparamref name="T"/>. If the configuration is missing, or the conversion fails, it will return <paramref name="defaultValue"/>
        /// </summary>
        /// <typeparam name="T">The type of the value for the key to load</typeparam>
        /// <param name="key">The key to the configuration to load</param>
        /// <param name="defaultValue">The default value to return if the value was not found, or could not be parsed</param>
        /// <returns>The value associated with the given configuration key, converted to <typeparamref name="T"/></returns>
        T Get<T>(string key, T defaultValue);

        /// <summary>
        /// Try to get a configuration by key as <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of the value for the key to load</typeparam>
        /// <param name="key">The key to the configuration to load</param>
        /// <param name="value">The value associated with the given configuration key, converted to <typeparamref name="T"/></param>
        /// <returns><see langword="true"/> if the value was successfully loaded and converted, otherwise <see langword="false"/></returns>
        bool TryGet<T>(string key, out T value);
    }
}