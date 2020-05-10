using System.Data.Entity.Infrastructure;
using System.Runtime.Serialization;
using Task.DB;

namespace Task.Surrogates
{
    public class OrderDetailSerializationSurrogate : ISerializationSurrogate
    {
        private readonly Northwind _context;

        public OrderDetailSerializationSurrogate(Northwind context)
        {
            _context = context;
        }

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var orderDetail = (Order_Detail)obj;

            var objectContext = (_context as IObjectContextAdapter).ObjectContext;
            objectContext.LoadProperty(orderDetail, od => od.Order);
            objectContext.LoadProperty(orderDetail, od => od.Product);

            info.AddValue(nameof(orderDetail.OrderID), orderDetail.OrderID);
            info.AddValue(nameof(orderDetail.ProductID), orderDetail.ProductID);
            info.AddValue(nameof(orderDetail.UnitPrice), orderDetail.UnitPrice);
            info.AddValue(nameof(orderDetail.Quantity), orderDetail.Discount);
            info.AddValue(nameof(orderDetail.Order), orderDetail.Order);
            info.AddValue(nameof(orderDetail.Product), orderDetail.Product);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var orderDetail = (Order_Detail)obj;
            orderDetail.OrderID = (int)info.GetValue(nameof(orderDetail.OrderID), typeof(int));
            orderDetail.ProductID = (int)info.GetValue(nameof(orderDetail.ProductID), typeof(int));
            orderDetail.UnitPrice = (decimal)info.GetValue(nameof(orderDetail.UnitPrice), typeof(decimal));
            orderDetail.Quantity = (short)info.GetValue(nameof(orderDetail.Quantity), typeof(short));
            orderDetail.Order = (Order)info.GetValue(nameof(orderDetail.Order), typeof(Order));
            orderDetail.Product = (Product)info.GetValue(nameof(orderDetail.Product), typeof(Product));

            return orderDetail;
        }
    }
}
