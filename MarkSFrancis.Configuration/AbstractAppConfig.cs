using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace MarkSFrancis.Configuration
{
    /// <summary>
    /// An <see cref="AppConfig"/> designed to be inherited from, with getters for each key within the <see cref="AppConfig"/>
    /// </summary>
    public abstract class AbstractAppConfig
    {
        /// <summary>
        /// The base application configuration reader
        /// </summary>
        protected AppConfig AppConfig { get; }

        /// <summary>
        /// Create a new application configuration reader from the app.config or web.config
        /// </summary>
        protected AbstractAppConfig()
        {
            AppConfig = new AppConfig();
        }

        /// <summary>
        /// Create a new application configuration from a <see cref="NameValueCollection"/>
        /// </summary>
        /// <param name="appSettings">The <see cref="NameValueCollection"/> to use as the settings source</param>
        /// <param name="connectionStrings">The <see cref="ConnectionStringSettingsCollection"/> to use as the connection strings source</param>
        protected AbstractAppConfig(NameValueCollection appSettings, ConnectionStringSettingsCollection connectionStrings)
        {
            AppConfig = new AppConfig(appSettings, connectionStrings);
        }

        /// <summary>
        /// Gets the element that has the specified key in Application Settings (excluding Connection Strings)
        /// </summary>
        /// <param name="key">The key to locate</param>
        /// <returns>The element that has the specified key in the read-only dictionary</returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key" /> is <see langword="null" /></exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> is not found</exception>
        protected string GetAppSetting(string key)
        {
            return AppConfig[key];
        }

        /// <summary>
        /// Gets the element that has the specified key in Application Settings, using the default converter to <typeparamref name="T"/>
        /// </summary>
        /// <param name="key">The key to locate</param>
        /// <returns>The element that has the specified key in the read-only dictionary</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key" /> is <see langword="null" /></exception>
        /// <exception cref="KeyNotFoundException">The property is retrieved and <paramref name="key" /> is not found</exception>
        /// <exception cref="InvalidCastException">This conversion is not supported</exception>
        /// <exception cref="FormatException">The value found with <paramref name="key"/> is not in a format recognized by the default converter</exception>
        /// <exception cref="OverflowException">The value found with <paramref name="key"/> represents a number that is out of the range of the default converter</exception>
        protected T GetAppSetting<T>(string key)
        {
            var converter = ConverterHelpers.GetDefaultConverter<string, T>();

            return GetAppSetting(key, converter);
        }

        /// <summary>
        /// Gets the element that has the specified key in Application Settings, using the default converter to <typeparamref name="T"/>
        /// </summary>
        /// <param name="key">The key to locate</param>
        /// <param name="converter">The converter to use from the <see cref="string"/> value found with <paramref name="key"/> to <typeparamref name="T"/></param>
        /// <returns>The element that has the specified key in the read-only dictionary</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key" /> is <see langword="null" />, or <paramref name="converter"/> is <see langword="null"/></exception>
        /// <exception cref="KeyNotFoundException">The property is retrieved and <paramref name="key" /> is not found</exception>
        protected T GetAppSetting<T>(string key, Func<string, T> converter)
        {
            if (key == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(key));
            }
            if (converter == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(converter));
            }

            string valueAsString = GetAppSetting(key);

            return converter(valueAsString);
        }

        /// <summary>
        /// Get a key's value from the connection strings
        /// </summary>
        /// <param name="key">The key for the connection string to retrieve</param>
        /// <returns></returns>
        protected string GetConnectionString(string key)
        {
            return AppConfig.GetConnectionString(key);
        }
    }
}

