using System.Collections.Specialized;
using System.Configuration;

namespace MarkSFrancis
{
    public class ApplicationConfiguration
    {
        private NameValueCollection AppSettings { get; }

        public ApplicationConfiguration(NameValueCollection appSettings)
        {
            AppSettings = appSettings;
        }

        public static ApplicationConfiguration FromConfig() => 
            new ApplicationConfiguration(ConfigurationManager.AppSettings);

        public string Get(string key)
        {
            return AppSettings[key];
        }
    }
}
