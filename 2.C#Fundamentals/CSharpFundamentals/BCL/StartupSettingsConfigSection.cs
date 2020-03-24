using System.Configuration;

namespace BCL
{
    public class StartupSettingsConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("ListeningDirectories")]
        public DirectoryCollection DirectoriesItems
        {
            get { return ((DirectoryCollection)(base["ListeningDirectories"])); }
        }

        [ConfigurationProperty("FileNameTemplate")]
        public string FileNameTemplate
        {
            get { return (base["FileNameTemplate"]).ToString(); }
        }

        [ConfigurationProperty("DefaultCulture")]
        public string DefaultCulture
        {
            get { return (base["DefaultCulture"]).ToString(); }
        }

        [ConfigurationProperty("DefaultDestinationFolder")]
        public string DefaultDestinationFolder
        {
            get { return (base["DefaultDestinationFolder"]).ToString(); }
        }

        [ConfigurationProperty("Files")]
        public FileCollection FilesItems
        {
            get { return ((FileCollection)(base["Files"])); }
        }
    }
}
