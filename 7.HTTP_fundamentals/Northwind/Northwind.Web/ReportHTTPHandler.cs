using Newtonsoft.Json;
using System.IO;
using System.Web;
using Northwind.Data;
using Northwind.Web.Services;
using System.Linq;

namespace Northwind.Web
{
    public class ReportHTTPHandler : IHttpHandler
    {
        private const string ExcelType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private const string XmlType = "text/xml";
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

            context.Response.ContentType = GetContentType(format);
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
            using (var reader = new StreamReader(httpRequest.GetBufferedInputStream()))
            {
                content = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(content))
            {
                content = httpRequest.QueryString.ToString();
            }

            var parsedContent = HttpUtility.ParseQueryString(content);
            string json = JsonConvert.SerializeObject(parsedContent.Cast<string>().ToDictionary(k => k, v => parsedContent[v]));
            return JsonConvert.DeserializeObject<OrderRequestContext>(json);
        }

        private string GetContentType(OutputFormats format)
        {
            switch (format)
            {
                case OutputFormats.Excel:
                    return ExcelType;

                case OutputFormats.XML:
                    return XmlType;

                default: return ExcelType;
            }
        }
    }
}