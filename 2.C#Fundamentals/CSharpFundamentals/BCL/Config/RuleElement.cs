using System.Configuration;

namespace BCL
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("fileName", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string FileName
        {
            get { return ((string)(base["fileName"])); }
            set { base["fileName"] = value; }
        }

        [ConfigurationProperty("destination", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Path
        {
            get { return ((string)(base["destination"])); }
            set { base["destination"] = value; }
        }
    }
}
