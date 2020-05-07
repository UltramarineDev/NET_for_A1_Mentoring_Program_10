using System.Configuration;

namespace BCL
{
    public class DirectoryElement : ConfigurationElement
    {
        [ConfigurationProperty("path", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Path
        {
            get { return ((string)(base["path"])); }
            set { base["path"] = value; }
        }
    }
}
