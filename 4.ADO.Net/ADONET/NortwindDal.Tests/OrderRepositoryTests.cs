using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NorthwindDAL;

namespace NortwindDal.Tests
{
    public class OrderRepositoryTests
    {
        OrderRepositorySql repository;

        [SetUp]
        public void SetUp()
        {
            string connectionString = @"Data Source=LAPTOP-P8MIHKFJ\MSSQLEXPRESSDB;Initial Catalog=Northwind;Integrated Security=True";
            string provider = "System.Data.SqlClient";
            repository = new OrderRepositorySql(connectionString, provider);
        }

        [Test]
        public void GetOrders_OK()
        {
            const int rowsCount = 830;

            var orders = repository.GetOrders();

            Assert.AreEqual(rowsCount, orders.Count());
        }

        [Test]
        public void GetOrderById_OK()
        {
            const int id = 10248;

            var expected = new Order
            {
                OrderId = 10248,
                OrderDate = new DateTime(1996, 07, 04),
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail { UnitPrice = 14.00M, Quantity = 12, ProductId = 11,
                        Product = new Product
                        {
                            ProductName = "Queso Cabrales",
                            ProductId = 11,
                            Discontinued = false
                        } },
                    new OrderDetail { UnitPrice = 9.8M, Quantity = 10, ProductId = 42,
                        Product = new Product
                        {
                            ProductName = "Singaporean Hokkien Fried Mee",
                            ProductId = 42,
                            Discontinued = true
                        } },
                    new OrderDetail { UnitPrice = 34.8M, Quantity = 5, ProductId = 72,
                        Product = new Product
                        {
                            ProductName = "Mozzarella di Giovanni",
                            ProductId = 72,
                            Discontinued = false
                        } }
                }
            };

            var actual = repository.GetOrderById(id);

            Assert.AreEqual(expected.OrderDetails.Count(), actual.OrderDetails.Count());
            Assert.AreEqual(expected.OrderDate, actual.OrderDate);
        }

        [Test]
        public void AddNew_OK()
        {
            const int expectedAffectedRowsCount = 1;
            const int insertedOrderId = 11079;

            var order = new Order
            {
                CustomerId = "RATTC",
                EmployeeId = 1,
                OrderDate = null,
                RequiredDate = new DateTime(1998, 06, 04),
                ShipCity = "Graz",
                ShipCountry = "Austria",
            };

            var actualAffectedRowsCount = repository.AddNew(order);
            var newOrder = repository.GetOrderById(insertedOrderId);

            Assert.AreEqual(expectedAffectedRowsCount, actualAffectedRowsCount);
            Assert.IsNotNull(newOrder);
            Assert.AreEqual(order.OrderDate, newOrder.OrderDate);
        }

        [Test]
        public void Update_StatusIsNew_OrderUpdated()
        {
            const int expectedAffectedRowsCount = 1;
            const int orderId = 11077;

            var order = new Order
            {
                OrderId = orderId,
                CustomerId = "LILAS",
                EmployeeId = 2,
                Status = OrderStatuses.New
            };

            var actualAffectedRowsCount = repository.UpdateOrder(order);
            var updatedOrder = repository.GetOrderById(orderId);

            Assert.AreEqual(expectedAffectedRowsCount, actualAffectedRowsCount);
            Assert.AreEqual(order.EmployeeId, updatedOrder.EmployeeId);
            Assert.AreEqual(order.CustomerId, updatedOrder.CustomerId);
        }

        [TestCase(OrderStatuses.InProgress)]
        [TestCase(OrderStatuses.Done)]
        public void Update_InvalidStatus_OrderDoesNotUpdated(OrderStatuses status)
        {
            const int expectedAffectedRowsCount = 0;
            const int orderId = 11077;

            var order = new Order
            {
                OrderId = orderId,
                CustomerId = "BONAP",
                EmployeeId = 2,
                Status = status
            };

            var actualAffectedRowsCount = repository.UpdateOrder(order);

            Assert.AreEqual(expectedAffectedRowsCount, actualAffectedRowsCount);
        }

        [Test]
        public void Delete_StatusIsDone_OrderDoesNotDeleted()
        {
            const int expectedAffectedRowsCount = 0;
            const int orderId = 11069;

            var actualAffectedRowsCount = repository.DeleteOrder(orderId);
            Assert.AreEqual(expectedAffectedRowsCount, actualAffectedRowsCount);
        }

        [Test]
        public void Delete_StatusIsNew_OrderDeleted()
        {
            const int expectedAffectedRowsCount = 1;
            const int orderId = 11079;

            var actualAffectedRowsCount = repository.DeleteOrder(orderId);
            Assert.AreEqual(expectedAffectedRowsCount, actualAffectedRowsCount);
        }

        [Test]
        public void SetOrderDate_OrderDateNotNull_OrderDateDoesNotUpdated()
        {
            const int orderId = 11078;
            const int expectedAffectedRowsCount = 0;
            DateTime orderdate = DateTime.UtcNow;

            var actualAffectedRowsCount = repository.SetOrderDate(orderId, orderdate);
            Assert.AreEqual(expectedAffectedRowsCount, actualAffectedRowsCount);
        }

        [Test]
        public void SetOrderDate_OrderDateIsNull_OrderDateUpdated()
        {
            const int orderId = 11083;
            const int expectedAffectedRowsCount = 1;
            DateTime orderdate = DateTime.UtcNow;

            var actualAffectedRowsCount = repository.SetOrderDate(orderId, orderdate);
            Assert.AreEqual(expectedAffectedRowsCount, actualAffectedRowsCount);
        }

        [Test]
        public void SetShippedDate_ShippedDateNotNull_ShippedDateDoesNotUpdated()
        {
            const int orderId = 11069;
            const int expectedAffectedRowsCount = 0;
            DateTime shippedDate = DateTime.UtcNow;

            var actualAffectedRowsCount = repository.SetShippedDate(orderId, shippedDate);
            Assert.AreEqual(expectedAffectedRowsCount, actualAffectedRowsCount);
        }

        [Test]
        public void SetShippedDate_ShippedDateIsNull_ShippedDateUpdated()
        {
            const int orderId = 11082;
            const int expectedAffectedRowsCount = 1;
            DateTime shippedDate = DateTime.UtcNow;

            var actualAffectedRowsCount = repository.SetShippedDate(orderId, shippedDate);
            Assert.AreEqual(expectedAffectedRowsCount, actualAffectedRowsCount);
        }

        [Test]
        public void GetCustomerOrdersHistory_OK()
        {
            const string customerId = "LILAS";
            const int rowsCount = 40;

            var result = repository.GetCustomerOrdersHistory(customerId);
            Assert.AreEqual(rowsCount, result.Count());
        }

        [Test]
        public void GetOrderDetailsHistory_OK()
        {
            const int orderId = 10248;
            const int rowsCount = 3;

            var result = repository.GetOrderDetailsHistory(orderId);
            Assert.AreEqual(rowsCount, result.Count());
        }
    }
}
