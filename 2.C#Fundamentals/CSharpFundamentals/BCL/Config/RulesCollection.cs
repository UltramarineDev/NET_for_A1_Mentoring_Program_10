using System.Configuration;

namespace BCL
{
    [ConfigurationCollection(typeof(RuleElement))]

    public class RulesCollection : ConfigurationElementCollection
    {
        [ConfigurationProperty("defaultDirectory", IsRequired = true)]
        public string DefaultDirectory => (string)this["defaultDirectory"];

        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement)(element)).FileName;
        }

        public RuleElement this[int idx]
        {
            get { return (RuleElement)BaseGet(idx); }
        }
    }
}
