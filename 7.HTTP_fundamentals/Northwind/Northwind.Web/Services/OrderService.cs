using Northwind.Data;
using Northwind.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Web.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        public OrderService(OrderRepository orderRepositoty)
        {
            _orderRepository = orderRepositoty;
        }

        public IEnumerable<Order> GetMany(OrderRequestContext orderRequestContext)
        {
            if (orderRequestContext == null)
            {
                return Enumerable.Empty<Order>();
            }

            var orders = _orderRepository.GetMany();

            if (orderRequestContext.CustomerId != null)
            {
                orders = orders.Where(o => o.CustomerId == orderRequestContext.CustomerId);
            }

            if (orderRequestContext.DateFrom != null)
            {
                orders = orders.Where(o => o.OrderDate != null && o.OrderDate >= orderRequestContext.DateFrom);
            }

            if (orderRequestContext.DateTo != null)
            {
                orders = orders.Where(o => o.OrderDate != null && o.OrderDate <= orderRequestContext.DateTo);
            }

            if (orderRequestContext.Skip != null)
            {
                orders = orders.Skip(orderRequestContext.Skip.Value);
            }

            if (orderRequestContext.Take != null)
            {
                orders = orders.Take(orderRequestContext.Take.Value);
            }

            return orders.OrderBy(o => o.OrderId).ToList();
        }
    }
}