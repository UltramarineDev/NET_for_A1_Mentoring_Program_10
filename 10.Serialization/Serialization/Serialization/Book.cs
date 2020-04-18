using System;
using System.Xml.Serialization;

namespace Serialization
{
    public class Book
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("isbn")]
        public string ISBN { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("genre")]
        public Genre Genre { get; set; }

        [XmlElement("publisher")]
        public string Publisher { get; set; }

        [XmlIgnore]
        public DateTime PublishDate { get; set; }

        [XmlElement("publish_date")]
        public string PublishDateString
        {
            get { return this.PublishDate.ToString("yyyy-MM-dd"); }
            set { this.PublishDate = DateTime.Parse(value); }
        }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlIgnore]
        public DateTime RegistationDate { get; set; }

        [XmlElement("registration_date")]
        public string RegistationDateString
        {
            get { return this.RegistationDate.ToString("yyyy-MM-dd"); }
            set { this.RegistationDate = DateTime.Parse(value); }
        }
    }
}
