using System.Collections.Specialized;
using System.Configuration;

namespace MarkSFrancis
{
    /// <summary>
    /// Access this application's configuration. Use the constructor for a custom settings source. Inherit from this class to provide your own custom key reading properties (like <see cref="DefaultConnectionString"/>)
    /// </summary>
    public class ApplicationConfiguration
    {
        private NameValueCollection AppSettings { get; }
        private ConnectionStringSettingsCollection ConnectionStrings { get; }

        /// <summary>
        /// Create a new ApplicationConfiguration reader from the app.config or web.config
        /// </summary>
        public ApplicationConfiguration() : this(ConfigurationManager.AppSettings, ConfigurationManager.ConnectionStrings)
        {
        }

        /// <summary>
        /// Create a new application configuration from a given <see cref="NameValueCollection"/>
        /// </summary>
        /// <param name="appSettings">The <see cref="NameValueCollection"/> to use as the settings source</param>
        /// <param name="connectionStrings">The <see cref="ConnectionStringSettingsCollection"/> to use as the connection strings source</param>
        public ApplicationConfiguration(NameValueCollection appSettings, ConnectionStringSettingsCollection connectionStrings)
        {
            AppSettings = appSettings;
            ConnectionStrings = connectionStrings;
        }

        /// <summary>
        /// Get a given key from the application settings
        /// </summary>
        /// <param name="key">The key for the setting to retrieve</param>
        /// <returns></returns>
        public string Get(string key)
        {
            return AppSettings[key];
        }

        /// <summary>
        /// Get a given key from the connection strings
        /// </summary>
        /// <param name="key">The key for the connection string to retrieve</param>
        /// <returns></returns>
        public string GetConnectionString(string key)
        {
            return ConnectionStrings[key].ConnectionString;
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
