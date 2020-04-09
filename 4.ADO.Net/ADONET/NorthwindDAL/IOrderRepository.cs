using System;
using System.Collections.Generic;

namespace NorthwindDAL
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        Order GetOrderById(int id);
        int AddNew(Order order);
        int UpdateOrder(Order order);
        int DeleteOrder(int id);
        int SetOrderDate(int id, DateTime orderDate);
        int SetShippedDate(int id, DateTime shippedDate);
        IEnumerable<ValueTuple<string, int>> GetCustomerOrdersHistory(string customerId);
        IEnumerable<OrderDetailsHistoryModel> GetOrderDetailsHistory(int orderId);
    }
}
