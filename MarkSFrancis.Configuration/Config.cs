using System;
using System.Collections.Specialized;
using System.Configuration;
using MarkSFrancis.Collections.Extensions;

namespace MarkSFrancis.Configuration
{
    /// <summary>
    /// Access this application's configuration
    /// </summary>
    public class Config
    {
        /// <summary>
        /// The Application Settings collection
        /// </summary>
        protected NameValueCollection AppSettings { get; }

        /// <summary>
        /// The Connection Strings collection
        /// </summary>
        protected ConnectionStringSettingsCollection ConnectionStrings { get; }
        
        /// <summary>
        /// Create a new application configuration reader from the app.config or web.config
        /// </summary>
        public static Config Default()
        {
            return new Config(ConfigurationManager.AppSettings, ConfigurationManager.ConnectionStrings);
        }

        /// <summary>
        /// Create a new application configuration from a pair of collection readers (one for app settings, the other for connection strings)
        /// </summary>
        /// <param name="appSettings">The <see cref="NameValueCollection"/> to use as the settings source</param>
        /// <param name="connectionStrings">The <see cref="ConnectionStringSettingsCollection"/> to use as the connection strings source</param>
        public Config(NameValueCollection appSettings, ConnectionStringSettingsCollection connectionStrings)
        {
            AppSettings = appSettings;
            ConnectionStrings = connectionStrings;
        }

        /// <summary>
        /// Get a key's value from the connection strings
        /// </summary>
        /// <param name="key">The key for the connection string to retrieve</param>
        /// <returns></returns>
        public virtual string GetConnectionString(string key)
        {
            return ConnectionStrings[key].ConnectionString;
        }

        /// <summary>
        /// Get a key's value from the application settings
        /// </summary>
        /// <param name="key">The key for the connection string to retrieve</param>
        /// <returns></returns>
        public virtual string GetAppSetting(string key)
        {
            return AppSettings[key];
        }

        /// <summary>
        /// Gets the value that is associated with the specified key from the application settings
        /// </summary>
        /// <param name="key">The key to locate</param>
        /// <param name="value">When this method returns <see langword="true"/>, the value associated with the specified key; otherwise <see langword="null"/></param>
        /// <returns>
        /// <see langword="true"/> if this contains an element that has the specified key; otherwise, <see langword="false"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="key" /> is <see langword="null" /></exception>
        public bool TryGetAppSetting(string key, out string value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!AppSettingsContainsKey(key))
            {
                value = null;
                return false;
            }

            value = AppSettings[key];
            return true;
        }

        /// <summary>
        /// Get whether the Application Settings contains the given key
        /// </summary>
        /// <param name="key">The key to search for</param>
        /// <returns>Whether the Application Settings contains the given key</returns>
        public bool AppSettingsContainsKey(string key)
        {
            return AppSettings.AllKeys.IndexOf(key) >= 0;
        }

        /// <summary>
        /// The key for the <see cref="DefaultConnectionString"/>
        /// </summary>
        public const string DefaultConnectionStringKey = "DefaultConnection";

        /// <summary>
        /// Gets the default from the connection strings
        /// </summary>
        public string DefaultConnectionString => GetConnectionString(DefaultConnectionStringKey);
    }
}
