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
            return type;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            return obj;
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
                    return GetOrderSurrogated(obj);
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

        private OrderSurrogated GetOrderSurrogated(object obj)
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
                Orders = ((Customer)obj).Orders.ToList(),
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
                Employees1 = ((Employee)obj).Employees1.ToList(),
                Employee1 = ((Employee)obj).Employee1,
                Orders = ((Employee)obj).Orders.ToList(),
                Territories = ((Employee)obj).Territories.ToList()
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
                Order = ((Order_Detail)obj).Order,
                Product = ((Order_Detail)obj).Product
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
                Orders = ((Shipper)obj).Orders.ToList()
            };

            return shipperSurrogated;
        }
    }
}
