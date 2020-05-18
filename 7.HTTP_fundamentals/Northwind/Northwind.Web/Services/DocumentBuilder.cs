using Northwind.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Northwind.Web.Models;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using NPOI.XSSF.UserModel;

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
            if (_format == OutputFormats.XML)
            {
                BuildXml(orders, stream);
            }
            else
            {
                BuildExcel(orders, stream);
            }
        }

        private void BuildXml(IEnumerable<Order> orders, Stream stream)
        {
            if (orders == null || !orders.Any())
            {
                return;
            }

            var xmlOutputOrders = new List<XmlOutputOrder>();
            foreach (var order in orders)
            {
                xmlOutputOrders.Add(new XmlOutputOrder()
                {
                    OrderId = order.OrderId,
                    CustomerId = order.CustomerId,
                    EmployeeId = order.EmployeeId,
                    OrderDate = order.OrderDate,
                });
            }

            using (var writer = XmlWriter.Create(stream))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<XmlOutputOrder>));
                serializer.Serialize(writer, xmlOutputOrders);
            }
        }

        private void BuildExcel(IEnumerable<Order> orders, Stream stream)
        {
           
        }
    }
}