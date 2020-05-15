using System.Collections.Generic;
using System.Xml.Serialization;

namespace Northwind.Web.Models
{
    [XmlRoot("Orders")]
    public class XmlDocument
    {
        public List<XmlOutputOrder> Orders { get; set; }
    }
}