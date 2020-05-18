using Northwind.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Northwind.Web.Models;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

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
            //if (!orders.Any())
            //{
            //    return;
            //}

            IWorkbook book;
            try
            {
                book = new XSSFWorkbook();
            }
            catch
            {
                book = null;
            }

            if (book == null)
            {
                book = new HSSFWorkbook();
            }

            var sheet = book.CreateSheet("Orders sheet");
            ICellStyle styleMiddle = book.CreateCellStyle();
            styleMiddle.Alignment = HorizontalAlignment.Center;
            styleMiddle.VerticalAlignment = VerticalAlignment.Center;
            styleMiddle.WrapText = true;

            var row = sheet.CreateRow(0);
            var cell = row.CreateCell(0);
            cell.SetCellValue(nameof(Order.OrderId));
            row.GetCell(0).CellStyle = styleMiddle;

            cell = row.CreateCell(1);
            cell.SetCellValue(nameof(Order.CustomerId));
            row.GetCell(1).CellStyle = styleMiddle;

            cell = row.CreateCell(2);
            cell.SetCellValue(nameof(Order.EmployeeId));
            row.GetCell(2).CellStyle = styleMiddle;

            cell = row.CreateCell(3);
            cell.SetCellValue(nameof(Order.OrderDate));
            row.GetCell(3).CellStyle = styleMiddle;

            var ordersList = orders.ToList();
            for (int i = 0, j = 1; i < ordersList.Count(); i++, j++)
            {
                row = sheet.CreateRow(j);
                row.CreateCell(0).SetCellValue(ordersList[i].OrderId);
                row.GetCell(0).CellStyle = styleMiddle;

                row.CreateCell(1).SetCellValue(ordersList[i].CustomerId);
                row.GetCell(1).CellStyle = styleMiddle;

                row.CreateCell(2).SetCellValue((double)ordersList[i]?.EmployeeId);
                row.GetCell(2).CellStyle = styleMiddle;

                row.CreateCell(3).SetCellValue(ordersList[i]?.OrderDate.ToString());
                row.GetCell(3).CellStyle = styleMiddle;
            }

            book.Write(stream);
        }
    }
}