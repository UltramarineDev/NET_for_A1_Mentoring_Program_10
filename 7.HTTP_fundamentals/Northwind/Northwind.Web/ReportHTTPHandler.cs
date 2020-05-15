using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Northwind.Data.Entities;
using Northwind.Data;
using Northwind.Web.Services;

namespace Northwind.Web
{
    public class ReportHTTPHandler : IHttpHandler
    {
        private const string ExcelType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private NorthwindDataContext dbContext = new NorthwindDataContext();

        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            var format = GetOutputFormat(context.Request);
            var documentBuilder = new DocumentBuilder(format);
            var requestContext = GetRequestContext(context.Request);

            var orderRepository = new OrderRepository(dbContext);
            var service = new OrderService(orderRepository);
            var orders = service.GetMany(requestContext);

            using (var memoryStream = new MemoryStream())
            {
                documentBuilder.Build(orders, memoryStream);
                var buffer = memoryStream.ToArray();
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }

            context.Response.ContentType = "text/xml";
        }

        private OutputFormats GetOutputFormat(HttpRequest httpRequest)
        {
            var acceptTypes = httpRequest.AcceptTypes;

            foreach (var acceptType in acceptTypes)
            {
                switch (acceptType)
                {
                    case ExcelType:
                        return OutputFormats.Excel;
                    default:
                        return OutputFormats.XML;
                }
            }

            return OutputFormats.XML;
        }

        private OrderRequestContext GetRequestContext(HttpRequest httpRequest)
        {
            string content;
            using (var reader = new StreamReader(httpRequest.InputStream))
            {
                content = reader.ReadToEnd();
            }

            var objectDeserialized = JsonConvert.DeserializeObject<OrderRequestContext>(content);
            return objectDeserialized;
        }
    }
}