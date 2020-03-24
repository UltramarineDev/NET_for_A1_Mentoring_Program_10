using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCL
{
    [ConfigurationCollection(typeof(FileElement))]

    public class FileCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FileElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FileElement)(element)).Name;
        }

        public FileElement this[int idx]
        {
            get { return (FileElement)BaseGet(idx); }
        }
    }
}
