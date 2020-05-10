using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Task.DB;

namespace Task
{
    public class DataContractSurrogate : IDataContractSurrogate
    {
        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public Type GetDataContractType(Type type)
        {
            if (typeof(Order).IsAssignableFrom(type))
            {
                return typeof(OrderSurrogated);
            }

            if (typeof(Customer).IsAssignableFrom(type))
            {
                return typeof(CustomerSurrogated);
            }

            if (typeof(Employee).IsAssignableFrom(type))
            {
                return typeof(EmployeeSurrogated);
            }

            if (typeof(Order_Detail).IsAssignableFrom(type))
            {
                return typeof(OrderDetailSurrogated);
            }

            if (typeof(Shipper).IsAssignableFrom(type))
            {
                return typeof(ShipperSurrogated);
            }

            return type;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            switch (obj)
            {
                case OrderSurrogated _:
                    return GetOrderFromSurrogated(obj);
                case CustomerSurrogated _:
                    return GetCustomerFromSurrogated(obj);
                case EmployeeSurrogated _:
                    return GetEmployeeFromSurrogated(obj);
                case OrderDetailSurrogated _:
                    return GetOrderDetailFromSurrogated(obj);
                case ShipperSurrogated _:
                    return GetShipperFromSurrogated(obj);
                default: return obj;
            }
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            switch (obj)
            {
                case Order _:
                    return GetOrderSurrogated(obj, targetType);
                case Customer _:
                    return GetCustomerSurrogated(obj);
                case Employee _:
                    return GetEmployeeSurrogated(obj);
                case Order_Detail _:
                    return GetOrderDetailSurrogated(obj);
                case Shipper _:
                    return GetShipperSurrogated(obj);
                default: return obj;
            }
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }

        private OrderSurrogated GetOrderSurrogated(object obj, Type targetType)
        {
            var orderSurrogated = new OrderSurrogated();
            orderSurrogated.OrderID = ((Order)obj).OrderID;
            orderSurrogated.CustomerID = ((Order)obj).CustomerID;
            orderSurrogated.EmployeeID = ((Order)obj).EmployeeID;
            orderSurrogated.OrderDate = ((Order)obj).OrderDate;
            orderSurrogated.RequiredDate = ((Order)obj).RequiredDate;
            orderSurrogated.ShippedDate = ((Order)obj).ShippedDate;
            orderSurrogated.ShipVia = ((Order)obj).ShipVia;
            orderSurrogated.Freight = ((Order)obj).Freight;
            orderSurrogated.ShipVia = ((Order)obj).ShipVia;
            orderSurrogated.ShipAddress = ((Order)obj).ShipAddress;
            orderSurrogated.ShipCity = ((Order)obj).ShipCity;
            orderSurrogated.ShipRegion = ((Order)obj).ShipRegion;
            orderSurrogated.ShipPostalCode = ((Order)obj).ShipPostalCode;
            orderSurrogated.ShipCountry = ((Order)obj).ShipCountry;
            orderSurrogated.Customer = ((Order)obj).Customer;
            orderSurrogated.Employee = ((Order)obj).Employee;
            orderSurrogated.Order_Details = ((Order)obj).Order_Details.ToList();

            return orderSurrogated;
        }

        private CustomerSurrogated GetCustomerSurrogated(object obj)
        {
            var customerSurrogated = new CustomerSurrogated
            {
                CustomerID = ((Customer)obj).CustomerID,
                CompanyName = ((Customer)obj).CompanyName,
                ContactName = ((Customer)obj).ContactName,
                ContactTitle = ((Customer)obj).ContactTitle,
                Address = ((Customer)obj).Address,
                City = ((Customer)obj).City,
                Region = ((Customer)obj).Region,
                PostalCode = ((Customer)obj).PostalCode,
                Country = ((Customer)obj).Country,
                Phone = ((Customer)obj).Phone,
                Fax = ((Customer)obj).Fax,
                Orders = null,
                CustomerDemographics = ((Customer)obj).CustomerDemographics.ToList()
            };

            return customerSurrogated;
        }

        private EmployeeSurrogated GetEmployeeSurrogated(object obj)
        {
            EmployeeSurrogated employeeSurrogated = new EmployeeSurrogated
            {
                EmployeeID = ((Employee)obj).EmployeeID,
                LastName = ((Employee)obj).LastName,
                FirstName = ((Employee)obj).FirstName,
                Title = ((Employee)obj).Title,
                TitleOfCourtesy = ((Employee)obj).TitleOfCourtesy,
                BirthDate = ((Employee)obj).BirthDate,
                HireDate = ((Employee)obj).HireDate,
                Address = ((Employee)obj).Address,
                City = ((Employee)obj).City,
                Region = ((Employee)obj).Region,
                PostalCode = ((Employee)obj).PostalCode,
                Country = ((Employee)obj).Country,
                HomePhone = ((Employee)obj).HomePhone,
                Extension = ((Employee)obj).Extension,
                Photo = ((Employee)obj).Photo,
                Notes = ((Employee)obj).Notes,
                ReportsTo = ((Employee)obj).ReportsTo,
                PhotoPath = ((Employee)obj).PhotoPath,
                Employees1 = null,
                Employee1 = null,
                Orders = null,
                Territories = null
            };

            return employeeSurrogated;
        }

        private OrderDetailSurrogated GetOrderDetailSurrogated(object obj)
        {
            var orderDetailSurrogated = new OrderDetailSurrogated
            {
                OrderID = ((Order_Detail)obj).OrderID,
                ProductID = ((Order_Detail)obj).ProductID,
                UnitPrice = ((Order_Detail)obj).UnitPrice,
                Quantity = ((Order_Detail)obj).Quantity,
                Discount = ((Order_Detail)obj).Discount,
                Order = null,
                Product = null
            };

            return orderDetailSurrogated;
        }

        private ShipperSurrogated GetShipperSurrogated(object obj)
        {
            var shipperSurrogated = new ShipperSurrogated
            {
                ShipperID = ((Shipper)obj).ShipperID,
                CompanyName = ((Shipper)obj).CompanyName,
                Phone = ((Shipper)obj).Phone,
                Orders = null
            };

            return shipperSurrogated;
        }

        private Order GetOrderFromSurrogated(object obj)
        {
            var order = new Order
            {
                OrderID = ((OrderSurrogated)obj).OrderID,
                CustomerID = ((OrderSurrogated)obj).CustomerID,
                EmployeeID = ((OrderSurrogated)obj).EmployeeID,
                OrderDate = ((OrderSurrogated)obj).OrderDate,
                RequiredDate = ((OrderSurrogated)obj).RequiredDate,
                ShippedDate = ((OrderSurrogated)obj).ShippedDate,
                ShipVia = ((OrderSurrogated)obj).ShipVia,
                Freight = ((OrderSurrogated)obj).Freight,
                ShipName = ((OrderSurrogated)obj).ShipName,
                ShipAddress = ((OrderSurrogated)obj).ShipAddress,
                ShipCity = ((OrderSurrogated)obj).ShipCity,
                ShipRegion = ((OrderSurrogated)obj).ShipRegion,
                ShipPostalCode = ((OrderSurrogated)obj).ShipPostalCode,
                ShipCountry = ((OrderSurrogated)obj).ShipCountry,
                Customer = ((OrderSurrogated)obj).Customer,
                Employee = ((OrderSurrogated)obj).Employee,
                Order_Details = ((OrderSurrogated)obj).Order_Details,
                Shipper = ((OrderSurrogated)obj).Shipper
            };

            return order;
        }

        private Customer GetCustomerFromSurrogated(object obj)
        {
            var customer = new Customer
            {
                CustomerID = ((CustomerSurrogated)obj).CustomerID,
                CompanyName = ((CustomerSurrogated)obj).CompanyName,
                ContactName = ((CustomerSurrogated)obj).ContactName,
                ContactTitle = ((CustomerSurrogated)obj).ContactTitle,
                Address = ((CustomerSurrogated)obj).Address,
                City = ((CustomerSurrogated)obj).City,
                Region = ((CustomerSurrogated)obj).Region,
                PostalCode = ((CustomerSurrogated)obj).PostalCode,
                Country = ((CustomerSurrogated)obj).Country,
                Phone = ((CustomerSurrogated)obj).Phone,
                Fax = ((CustomerSurrogated)obj).Fax,
                Orders = null,
                CustomerDemographics = ((CustomerSurrogated)obj).CustomerDemographics.ToList()
            };

            return customer;
        }

        private Employee GetEmployeeFromSurrogated(object obj)
        {
            var employee = new Employee
            {
                EmployeeID = ((EmployeeSurrogated)obj).EmployeeID,
                LastName = ((EmployeeSurrogated)obj).LastName,
                FirstName = ((EmployeeSurrogated)obj).FirstName,
                Title = ((EmployeeSurrogated)obj).Title,
                TitleOfCourtesy = ((EmployeeSurrogated)obj).TitleOfCourtesy,
                BirthDate = ((EmployeeSurrogated)obj).BirthDate,
                HireDate = ((EmployeeSurrogated)obj).HireDate,
                Address = ((EmployeeSurrogated)obj).Address,
                City = ((EmployeeSurrogated)obj).City,
                Region = ((EmployeeSurrogated)obj).Region,
                PostalCode = ((EmployeeSurrogated)obj).PostalCode,
                Country = ((EmployeeSurrogated)obj).Country,
                HomePhone = ((EmployeeSurrogated)obj).HomePhone,
                Extension = ((EmployeeSurrogated)obj).Extension,
                Photo = ((EmployeeSurrogated)obj).Photo,
                Notes = ((EmployeeSurrogated)obj).Notes,
                ReportsTo = ((EmployeeSurrogated)obj).ReportsTo,
                PhotoPath = ((EmployeeSurrogated)obj).PhotoPath,
                Employees1 = null,
                Employee1 = null,
                Orders = null,
                Territories = null
            };

            return employee;
        }

        private Order_Detail GetOrderDetailFromSurrogated(object obj)
        {
            var orderDetail = new Order_Detail
            {
                OrderID = ((OrderDetailSurrogated)obj).OrderID,
                ProductID = ((OrderDetailSurrogated)obj).ProductID,
                UnitPrice = ((OrderDetailSurrogated)obj).UnitPrice,
                Quantity = ((OrderDetailSurrogated)obj).Quantity,
                Discount = ((OrderDetailSurrogated)obj).Discount,
                Order = null,
                Product = null
            };

            return orderDetail;
        }

        private Shipper GetShipperFromSurrogated(object obj)
        {
            var shipper = new Shipper
            {
                ShipperID = ((ShipperSurrogated)obj).ShipperID,
                CompanyName = ((ShipperSurrogated)obj).CompanyName,
                Phone = ((ShipperSurrogated)obj).Phone,
                Orders = null
            };

            return shipper;
        }
    }
}
