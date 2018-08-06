using Microsoft.Extensions.Configuration;

namespace MarkSFrancis.Configuration
{
    /// <summary>
    /// Loads the application configuration, including default values and converting the configuration to the needed types
    /// </summary>
    public class Config : IConfig
    {
        /// <summary>
        /// Configuration to read from
        /// </summary>
        protected IConfiguration Configuration { get; }

        /// <summary>
        /// Create a new <see cref="Config"/> using <paramref name="configuration"/> as a configuration source
        /// </summary>
        /// <param name="configuration">The configuration source</param>
        public Config(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Get a configuration by key as a string
        /// </summary>
        /// <param name="key">The key to the configuration to load</param>
        /// <returns>The value associated with the given configuration key</returns>
        public string this[string key] => Configuration[key];

        /// <summary>
        /// Get a configuration by key as <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of the value for the key to load</typeparam>
        /// <param name="key">The key to the configuration to load</param>
        /// <returns>The value associated with the given configuration key, converted to <typeparamref name="T"/></returns>
        public T Get<T>(string key)
        {
            var defaultConverter = ConverterHelpers.GetDefaultConverter<string, T>();
            return defaultConverter(this[key]);
        }

        /// <summary>
        /// Get a configuration by key as <typeparamref name="T"/>. If the configuration is missing, or the conversion fails, it will return <paramref name="defaultValue"/>
        /// </summary>
        /// <typeparam name="T">The type of the value for the key to load</typeparam>
        /// <param name="key">The key to the configuration to load</param>
        /// <param name="defaultValue">The default value to return if the value was not found, or could not be parsed</param>
        /// <returns>The value associated with the given configuration key, converted to <typeparamref name="T"/></returns>
        public T Get<T>(string key, T defaultValue)
        {
            if (TryGet<T>(key, out T value))
            {
                return value;
            }
            return defaultValue;
        }

        /// <summary>
        /// Try to get a configuration by key as <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of the value for the key to load</typeparam>
        /// <param name="key">The key to the configuration to load</param>
        /// <param name="value">The value associated with the given configuration key, converted to <typeparamref name="T"/></param>
        /// <returns><see langword="true"/> if the value was successfully loaded and converted, otherwise <see langword="false"/></returns>
        public bool TryGet<T>(string key, out T value)
        {
            var keyValue = this[key];

            if (keyValue == null)
            {
                value = default(T);
                return false;
            }

            try
            {
                var defaultConverter = ConverterHelpers.GetDefaultConverter<string, T>();
                value = defaultConverter(this[key]);
                return true;
            }
            catch
            {
                value = default(T);
                return false;
            }
        }

        /// <summary>
        /// Get a connection string from the configuration
        /// </summary>
        /// <param name="key">The key to the connection string to load</param>
        /// <returns>The connection string associated with the given key</returns>
        public string GetConnectionString(string key)
        {
            return Configuration.GetConnectionString(key);
        }
    }
}
