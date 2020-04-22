using System.Data.Entity.Infrastructure;
using System.Runtime.Serialization;
using Task.DB;

namespace Task
{
    public class SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var order_Detail = (Order_Detail)obj;
            info.AddValue("OrderID", order_Detail.OrderID);
            info.AddValue("ProductID", order_Detail.ProductID);
            info.AddValue("UnitPrice", order_Detail.UnitPrice);
            info.AddValue("Quantity", order_Detail.Quantity);
            info.AddValue("Discount", order_Detail.Discount);
            info.AddValue("Order", order_Detail.Order);
            info.AddValue("Product", order_Detail.Product);

            var objectContext = (context.Context as IObjectContextAdapter).ObjectContext;
            objectContext.LoadProperty(order_Detail, f => f.Order);
            objectContext.LoadProperty(order_Detail, f => f.Product);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var order_Detail = (Order_Detail)obj;
            order_Detail.OrderID = info.GetInt32("OrderID");
            order_Detail.ProductID = info.GetInt32("ProductID");
            order_Detail.UnitPrice = info.GetDecimal("UnitPrice");
            order_Detail.Quantity = info.GetInt16("Quantity");
            order_Detail.Discount = info.GetSingle("Discount");
            order_Detail.Order = info.GetValue("Order", typeof(Order)) as Order; 
            order_Detail.Product = info.GetValue("Product", typeof(Product)) as Product;

            return order_Detail;
        }
    }
}
