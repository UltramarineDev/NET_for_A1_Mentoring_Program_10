using Northwind.Data;
using System;
using System.Data.Entity;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new NorthwindDataContext();
            var orderDetailsRepository = new OrderDetailsRepository(context);
            var resultForTask = orderDetailsRepository.GetMany(i => i.Product.CategoryId == 1)
                .Include(i => i.Product)
                .Include(i => i.Order).ToList();
            foreach (var res in resultForTask)
            {
                Console.WriteLine($"{res.OrderId.ToString()}, {res.Order.Customer.ContactName}, {res.Product.ProductName}");
            }

            var repository = new OrderRepository(context);
            var result = repository.GetMany(i => i.ShipVia == 1).ToArray();
            foreach (var item in result)
            {
                Console.WriteLine(item.OrderId.ToString(), item.OrderDate);
            }
        }
    }
}
