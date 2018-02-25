using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using MarkSFrancis.Collections;

namespace MarkSFrancis.Configuration
{
    /// <summary>
    /// Access this application's configuration. Use the constructor for a custom settings source. Inherit from this class to provide your own custom key reading properties (like <see cref="DefaultConnectionString"/>)
    /// </summary>
    public class AppConfig : IReadOnlyDictionary<string, string>
    {
        protected NameValueCollection AppSettings { get; }
        protected ConnectionStringSettingsCollection ConnectionStrings { get; }

        /// <summary>
        /// Create a new ApplicationConfiguration reader from the app.config or web.config
        /// </summary>
        public AppConfig() : this(ConfigurationManager.AppSettings, ConfigurationManager.ConnectionStrings)
        {
        }

        /// <summary>
        /// Create a new application configuration from a <see cref="NameValueCollection"/>
        /// </summary>
        /// <param name="appSettings">The <see cref="NameValueCollection"/> to use as the settings source</param>
        /// <param name="connectionStrings">The <see cref="ConnectionStringSettingsCollection"/> to use as the connection strings source</param>
        public AppConfig(NameValueCollection appSettings, ConnectionStringSettingsCollection connectionStrings)
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
        /// Determines whether this contains an element that has the specified key
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        /// <see langword="true" /> if the read-only dictionary contains an element that has the specified key; otherwise, <see langword="false" />.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// 
        /// <paramref name="key" /> is <see langword="null" />.</exception>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return TryGetValue(key, out _);
        }

        /// <summary>
        /// Gets the value that is associated with the specified key
        /// </summary>
        /// <param name="key">The key to locate</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized</param>
        /// <returns>
        /// <see langword="true" /> if the object that implements the <see cref="T:System.Collections.Generic.IReadOnlyDictionary`2" /> interface contains an element that has the specified key; otherwise, <see langword="false" /></returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// 
        /// <paramref name="key" /> is <see langword="null" />.</exception>
        public bool TryGetValue(string key, out string value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            try
            {
                value = this[key];
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection</returns>
        public virtual IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            var allKeys = AppSettings.AllKeys;
            foreach (var key in allKeys)
            {
                yield return new KeyValuePair<string, string>(key, this[key]);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// The key for the <see cref="DefaultConnectionString"/>
        /// </summary>
        public const string DefaultConnectionStringKey = "DefaultConnection";

        /// <summary>
        /// Gets the default from the connection strings
        /// </summary>
        public virtual string DefaultConnectionString => GetConnectionString(DefaultConnectionStringKey);

        /// <summary>
        /// Gets an enumerable collection that contains the keys in Application Settings (excluding Connection Strings)
        /// </summary>
        /// <returns>An enumerable collection that contains the keys in Application Settings (excluding Connection Strings)</returns>
        public virtual IEnumerable<string> Keys => AppSettings.AllKeys;

        /// <summary>
        /// Gets an enumerable collection that contains the values in the read-only dictionary
        /// </summary>
        /// <returns>An enumerable collection that contains the values in the read-only dictionary</returns>
        public IEnumerable<string> Values
        {
            get
            {
                foreach(var item in new EnumerableEnumerator<KeyValuePair<string, string>>(GetEnumerator()))
                {
                    yield return item.Value;
                }
            }
        }

        /// <summary>
        /// Gets the number of elements in the Application Settings (excluding Connection Strings)
        /// </summary>
        /// <returns>The number of elements in the collection</returns>
        public int Count => GetEnumerator().ToEnumerable().Count();
        
        /// <summary>
        /// Gets the element that has the specified key in Application Settings (excluding Connection Strings)
        /// </summary>
        /// <param name="key">The key to locate</param>
        /// <returns>The element that has the specified key in the read-only dictionary</returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key" /> is <see langword="null" /></exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> is not found</exception>
        public string this[string key] => AppSettings[key];
    }
}
