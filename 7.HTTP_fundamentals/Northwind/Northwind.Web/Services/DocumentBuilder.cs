using Northwind.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Northwind.Web.Models;
using System.Xml.Serialization;
using System.IO;

namespace Northwind.Web.Services
{
    public class DocumentBuilder
    {
        private readonly OutputFormats _format;
        public DocumentBuilder(OutputFormats format)
        {
            _format = format;
        }

        public void Build(IEnumerable<Order> orders, Stream stream)
        {
            var xmlDocument = new XmlDocument();
            Array.Copy(orders.ToArray(), xmlDocument.Orders.ToArray(), orders.Count());

            using (StreamReader reader = new StreamReader(stream))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XmlDocument));
                serializer.Serialize(stream, xmlDocument);
            }
        }
    }
}