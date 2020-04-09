using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;

namespace NorthwindDAL
{
    public abstract class OrderRepository : IOrderRepository
    {
        private readonly DbProviderFactory _dbProviderFactory;
        private readonly string _connectionString;

        public OrderRepository(string connectionString, string provider)
        {
            _dbProviderFactory = DbProviderFactories.GetFactory(provider);
            _connectionString = connectionString;
        }

        public virtual int AddNew(Order order)
        {
            using (var connection = _dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "INSERT INTO dbo.Orders" +
                        "(CustomerID, EmployeeID, RequiredDate, ShipCity, ShipCountry)" +
                        "VALUES(@customerID, @employeeID, @requiredDate, @shipCity, @shipCountry)";
                    command.CommandType = CommandType.Text;

                    var customerID = command.CreateParameter();
                    customerID.ParameterName = "@customerID";
                    customerID.Value = order.CustomerId;
                    command.Parameters.Add(customerID);

                    var employeeID = command.CreateParameter();
                    employeeID.ParameterName = "@employeeID";
                    employeeID.Value = order.EmployeeId;
                    command.Parameters.Add(employeeID);

                    //var orderDate = command.CreateParameter();
                    //orderDate.ParameterName = "@orderDate";
                    //orderDate.Value = order.OrderDate;
                    //command.Parameters.Add(orderDate);

                    var requiredDate = command.CreateParameter();
                    requiredDate.ParameterName = "@requiredDate";
                    requiredDate.Value = order.RequiredDate;
                    command.Parameters.Add(requiredDate);

                    var shipCity = command.CreateParameter();
                    shipCity.ParameterName = "@shipCity";
                    shipCity.Value = order.ShipCity;
                    command.Parameters.Add(shipCity);

                    var shipCountry = command.CreateParameter();
                    shipCountry.ParameterName = "@shipCountry";
                    shipCountry.Value = order.ShipCountry;
                    command.Parameters.Add(shipCountry);

                    int affectedRowsCount = command.ExecuteNonQuery();

                    return affectedRowsCount;
                }
            }
        }

        public virtual int DeleteOrder(int id)
        {
            var order = GetOrderById(id);
            var orderStatus = GetOrderStatus(order.OrderDate, order.ShippedDate);
            if (orderStatus == OrderStatuses.Done)
            {
                return 0;
            }

            using (var connection = _dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "DELETE FROM dbo.Orders WHERE OrderID = @id";
                    command.CommandType = CommandType.Text;

                    var paramId = command.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = id;

                    command.Parameters.Add(paramId);

                    int affectedRowsCount = command.ExecuteNonQuery();

                    return affectedRowsCount;
                }
            }
        }

        public virtual Order GetOrderById(int id)
        {
            using (var connection = _dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "SELECT * FROM dbo.Orders WHERE OrderID = @id; " +
                        "SELECT * FROM dbo.[Order Details] " +
                        "JOIN dbo.Products ON dbo.[Order Details].ProductID = dbo.Products.ProductID WHERE OrderID = @id";

                    command.CommandType = CommandType.Text;

                    var orderId = command.CreateParameter();
                    orderId.ParameterName = "@id";
                    orderId.Value = id;
                    command.Parameters.Add(orderId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }

                        reader.Read();

                        var order = new Order();
                        order.OrderId = reader.GetInt32(0);
                        order.CustomerId = reader.GetString(1);
                        order.EmployeeId = reader.GetInt32(2);
                        _ = reader.IsDBNull(3) ? order.OrderDate = null : order.OrderDate = reader.GetDateTime(3);
                        _ = reader.IsDBNull(5) ? order.ShippedDate = null : order.ShippedDate = reader.GetDateTime(5);

                        reader.NextResult();
                        order.OrderDetails = new List<OrderDetail>();

                        while (reader.Read())
                        {
                            var detail = new OrderDetail();
                            detail.UnitPrice = (decimal)reader["UnitPrice"];
                            detail.Quantity = (short)reader["Quantity"];
                            detail.ProductId = (int)reader["ProductID"];
                            detail.Product = new Product();

                            var product = new Product();
                            product.ProductName = (string)reader["ProductName"];
                            product.ProductId = (int)reader["ProductID"];
                            detail.Product = product;
                            order.OrderDetails.Add(detail);
                        }

                        return order;
                    }
                }
            }
        }

        public virtual IEnumerable<Order> GetOrders()
        {
            var resultOrders = new List<Order>();

            using (var connection = _dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.Orders";
                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var order = new Order();
                            order.OrderId = reader.GetInt32(0);
                            order.CustomerId = reader.GetString(1);
                            order.EmployeeId = reader.GetInt32(2);
                            order.OrderDate = reader.GetDateTime(3);

                            _ = reader.IsDBNull(5) ? order.ShippedDate = null : order.ShippedDate = reader.GetDateTime(5);

                            resultOrders.Add(order);

                            order.Status = GetOrderStatus(order.OrderDate, order.ShippedDate);
                        }
                    }
                }
            }

            return resultOrders;
        }

        public virtual int UpdateOrder(Order order)
        {
            if (order.Status == OrderStatuses.InProgress || order.Status == OrderStatuses.Done)
            {
                return 0;
            }

            using (var connection = _dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "UPDATE dbo.Orders " +
                        "SET CustomerID = @customerID, " +
                        "EmployeeID = @employeeID " +
                        "WHERE OrderID = @id";

                    command.CommandType = CommandType.Text;

                    var customerID = command.CreateParameter();
                    customerID.ParameterName = "@customerID";
                    customerID.Value = order.CustomerId;
                    command.Parameters.Add(customerID);

                    var employeeID = command.CreateParameter();
                    employeeID.ParameterName = "@employeeID";
                    employeeID.Value = order.EmployeeId;
                    command.Parameters.Add(employeeID);

                    var id = command.CreateParameter();
                    id.ParameterName = "@id";
                    id.Value = order.OrderId;
                    command.Parameters.Add(id);

                    int affectedRowsCount = command.ExecuteNonQuery();

                    return affectedRowsCount;
                }
            }
        }

        public virtual int SetOrderDate(int id, DateTime orderDate)
        {
            var order = GetOrderById(id);
            if (order.OrderDate != null)
            {
                return 0;
            }

            order.OrderDate = orderDate;
            return UpdateOrder(order);
        }

        public virtual int SetShippedDate(int id, DateTime shippedDate)
        {
            var order = GetOrderById(id);
            if (order.ShippedDate != null)
            {
                return 0;
            }

            order.ShippedDate = shippedDate;
            return UpdateOrder(order);
        }

        public virtual IEnumerable<ValueTuple<string, int>> GetCustomerOrdersHistory(string customerId)
        {
            var procedureName = "[dbo].[CustOrderHist]";
            var result = new List<ValueTuple<string, int>>();

            using (var connection = _dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = procedureName;

                    var customerIDParam = command.CreateParameter();
                    customerIDParam.ParameterName = "@CustomerID";
                    customerIDParam.Value = customerId;
                    command.Parameters.Add(customerIDParam);

                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var productName = reader.GetString(0);
                            var total = reader.GetInt32(1);
                            var item = new ValueTuple<string, int>();

                            item.Item1 = procedureName;
                            item.Item2 = total;
                            result.Add(item);
                        }
                    }

                    return result;
                }
            }
        }

        public virtual IEnumerable<OrderDetailsHistoryModel> GetOrderDetailsHistory(int orderId)
        {
            var procedureName = "[dbo].[CustOrdersDetail]";
            var result = new List<OrderDetailsHistoryModel>();

            using (var connection = _dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = procedureName;

                    var orderIDParam = command.CreateParameter();
                    orderIDParam.ParameterName = "@OrderID";
                    orderIDParam.Value = orderId;
                    command.Parameters.Add(orderIDParam);

                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var productName = reader.GetString(0);
                            var unitPrice = reader.GetDecimal(1);
                            var quantity = reader.GetInt16(2);
                            var discount = reader.GetInt32(3);
                            var extendedPrice = reader.GetDecimal(4);

                            var item = new OrderDetailsHistoryModel
                            {
                                ProductName = productName,
                                UnitPrice = unitPrice,
                                Quantity = quantity,
                                Discount = discount,
                                ExtendedPrice = extendedPrice
                            };

                            result.Add(item);
                        }
                    }

                    return result;
                }
            }
        }

        private OrderStatuses GetOrderStatus(DateTime? orderDate, DateTime? shippedDate)
        {
            if (orderDate == null)
            {
                return OrderStatuses.New;
            }

            return shippedDate == null ? OrderStatuses.InProgress : OrderStatuses.Done;
        }
    }
}
