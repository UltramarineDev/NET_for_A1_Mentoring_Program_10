using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Serialization
{
    [XmlRoot("catalog", Namespace = _namespace)]
    public class Catalog
    {
        [XmlIgnore]
        public DateTime Date { get; set; }

        [XmlAttribute(AttributeName = "date")]
        public string DateString
        {
            get { return this.Date.ToString("yyyy-MM-dd"); }
            set { this.Date = DateTime.Parse(value); }
        }

        [XmlElement("book")]
        public List<Book> Books { get; set; }

        public string Namespace
        {
            get { return _namespace; }
        }

        private const string _namespace = "http://library.by/catalog";
    }
}
