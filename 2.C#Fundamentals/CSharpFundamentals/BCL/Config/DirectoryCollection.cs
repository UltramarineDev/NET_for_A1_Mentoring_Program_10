using System.Configuration;

namespace BCL
{
    [ConfigurationCollection(typeof(DirectoryElement))]
    public class DirectoryCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DirectoryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DirectoryElement)(element)).Path;
        }

        public DirectoryElement this[int idx]
        {
            get { return (DirectoryElement)BaseGet(idx); }
        }
    }
}
