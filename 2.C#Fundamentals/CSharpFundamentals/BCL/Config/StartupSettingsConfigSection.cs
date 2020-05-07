using System.Configuration;
using System.Globalization;

namespace BCL
{
    public class StartupSettingsConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("ListeningDirectories")]
        public DirectoryCollection DirectoriesItems
        {
            get { return ((DirectoryCollection)(base["ListeningDirectories"])); }
        }

        [ConfigurationProperty("culture")]
        public CultureInfo Culture
        {
            get { return (CultureInfo)this["culture"]; }
        }

        [ConfigurationProperty("rules")]
        public RulesCollection RulesItems
        {
            get { return ((RulesCollection)(base["rules"])); }
        }
    }
}
