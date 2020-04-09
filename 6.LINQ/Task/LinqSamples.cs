// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {

        private DataSource dataSource = new DataSource();

        [Category("Restriction Operators")]
        [Title("Where - Task 1")]
        [Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
        public void Linq1()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var lowNums =
                from num in numbers
                where num < 5
                select num;

            Console.WriteLine("Numbers < 5:");
            foreach (var x in lowNums)
            {
                Console.WriteLine(x);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 2")]
        [Description("This sample return return all presented in market products")]

        public void Linq2()
        {
            var products =
                from p in dataSource.Products
                where p.UnitsInStock > 0
                select p;

            foreach (var p in products)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Homework")]
        [Title("Task 1")]
        [Description("Gets a list of all customers whose total orders exceed the given value.")]

        public void Linq01()
        {
            var customers = dataSource.Customers;
            var value = 5;
            Console.WriteLine("the given value: {0}", value);
            var resultCustomers = customers.Where(customer => customer.Orders.Sum(order => order.Total) > value);
            foreach (var x in resultCustomers)
            {
                Console.WriteLine("Customer ID: {0}", x.CustomerID);
            }
        }

        [Category("Homework")]
        [Title("Task 2_1")]
        [Description("Gets a list of supplies for each customer located at the same country and city without GroupBy")]

        public void Linq02_1()
        {
            var customers = dataSource.Customers;
            var suppliers = dataSource.Suppliers;
            foreach (var customer in customers)
            {
                Console.WriteLine("For customer {0}:", customer.CustomerID);
                Console.WriteLine("located in {0}, {1}:", customer.Country, customer.City);

                var resultSuppliers = suppliers.Where(x => x.Country == customer.Country && x.City == customer.City);
                foreach (var item in resultSuppliers)
                {
                    Console.WriteLine("{0}, {1}, {2}", item.SupplierName, item.Country, item.City);
                }
            }
        }

        [Category("Homework")]
        [Title("Task 2_2")]
        [Description("Gets a list of supplies for each customer located at the same country and city with GroupBy")]
        public void Linq02_2()
        {
            var customers = dataSource.Customers;
            var suppliers = dataSource.Suppliers;
            foreach (var customer in customers)
            {
                Console.WriteLine("For customer {0}:", customer.CustomerID);
                Console.WriteLine("located in {0}, {1}:", customer.Country, customer.City);

                var resultSuppliers = suppliers.Where(x => x.Country == customer.Country && x.City == customer.City).GroupBy(x => x.Country);
                foreach (var group in resultSuppliers)
                {
                    foreach (var item in group)
                    {
                        Console.WriteLine("{0}, {1}, {2}", item.SupplierName, item.Country, item.City);
                    }
                }
            }
        }

        [Category("Homework")]
        [Title("Task 3")]
        [Description("Gets a list of all customers whose order exceed the given value")]
        public void Linq03()
        {
            var customers = dataSource.Customers;
            var value = 300;

            Console.WriteLine("The given value {0}", value);
            var resultCustomers = customers.Where(customer => customer.Orders.Any(order => order.Total > value));
            foreach (var customer in resultCustomers)
            {
                Console.WriteLine("Customer id: {0}\n Random cost of order that exceeds value: {1}",
                    customer.CustomerID, customer.Orders.Where(order => order.Total > value).First().Total);
            }
        }

        [Category("Homework")]
        [Title("Task 4")]
        [Description("Gets a list of all customers with the date of becoming a client.")]
        public void Linq04()
        {
            var customers = dataSource.Customers;
            var clients = customers.Where(c => c.Orders.Count() > 0);
            foreach (var customer in clients)
            {
                Console.WriteLine("Customer id: {0}\n First order date: {1}", customer.CustomerID, customer.Orders.First().OrderDate);
            }
        }

        [Category("Homework")]
        [Title("Task 5")]
        [Description("Gets a list of all customers with the date of becoming a client ordered by year, month, orders tour, name.")]
        public void Linq05()
        {
            var customers = dataSource.Customers;
            var clients = customers.Where(c => c.Orders.Count() > 0);

            var resultCustomers = clients.OrderBy(c => c.CustomerID)
                                         .OrderByDescending(c => c.Orders.Sum(x => x.Total))
                                         .OrderBy(c => c.Orders.First().OrderDate.Month)
                                         .OrderBy(customer => customer.Orders.First().OrderDate.Year);

            foreach (var customer in resultCustomers)
            {
                Console.WriteLine("year: {0}, month: {1}, Total: {2}, Name: {3}",
                    customer.Orders.First().OrderDate.Year,
                    customer.Orders.First().OrderDate.Month,
                    customer.Orders.Sum(x => x.Total),
                    customer.CustomerID);
            }
        }

        [Category("Homework")]
        [Title("Task 6")]
        [Description("Gets a list of customers with non-decimal postcode or the region is not specified or there is no parentheses in phone number")]
        public void Linq06()
        {
            var customers = dataSource.Customers;
            var resultCustomers = customers.Where(customer => ContainsLetters(customer.PostalCode) ||
                customer.Region == "" || customer.Region == null || !customer.Phone.Contains("("));

            bool ContainsLetters(string postalCode)
            {
                return int.TryParse(postalCode, out int number);
            }

            foreach (var customer in resultCustomers)
            {
                Console.WriteLine("Customer id {0}", customer.CustomerID);
                Console.WriteLine("Customer postcode {0}", customer.PostalCode);
                Console.WriteLine("Customer region {0}", customer.Region);
                Console.WriteLine("Customer phone number {0}", customer.Phone);
            }
        }

        [Category("Homework")]
        [Title("Task 7")]
        [Description("Groups all products by categories. Inside that - by existance in stock. Inside that - sorted by price")]
        public void Linq07()
        {
            var products = dataSource.Products;
            var productgroups = products.GroupBy(p => p.Category)
                                        .Select(o => o.GroupBy(u => u.UnitsInStock)
                                        .Select(c => c.OrderBy(p => p.UnitPrice)));

            foreach (var product in productgroups)
            {
                foreach (var itemgroup in product)
                {
                    foreach (var item in itemgroup)
                    {
                        Console.WriteLine("Category: {0}\n Units in stock: {1}\n Price: {2}", item.Category, item.UnitsInStock, item.UnitPrice);
                    }
                }
            }
        }

        [Category("Homework")]
        [Title("Task 8")]
        [Description("Groups all products into 3 groups: cheap, middle, expensive")]
        public void Linq08()
        {
            var products = dataSource.Products;

            var productgroupCheap = products.Where(x => x.UnitPrice < 5);
            var productgroupMiddle = products.Where(x => x.UnitPrice >= 5 || x.UnitPrice < 20);
            var productgroupExpensive = products.Where(x => x.UnitPrice > 20);

            Console.WriteLine("Cheap group");
            foreach (var item in productgroupCheap)
            {
                Console.WriteLine("{0}", item.ProductID);
                Console.WriteLine("{0}", item.UnitPrice);
            }

            Console.WriteLine("Middle group");
            foreach (var item in productgroupMiddle)
            {
                Console.WriteLine("{0}", item.ProductID);
                Console.WriteLine("{0}", item.UnitPrice);
            }

            Console.WriteLine("Expensive group");
            foreach (var item in productgroupExpensive)
            {
                Console.WriteLine("{0}", item.ProductID);
                Console.WriteLine("{0}", item.UnitPrice);
            }
        }

        [Category("Homework")]
        [Title("Task 9")]
        [Description("Gets the average order price for all customers in specified city, average amount of orders per customer from each city.")]
        public void Linq09()
        {
            var customers = dataSource.Customers;

            var clients = customers.Where(c => c.Orders.Count() > 0);
            var avgOrdersTotal = clients.GroupBy(c => c.City)
                                       .Select(group => new
                                       {
                                           city = group.Key,
                                           avg = group.Average(customer => customer.Orders.Average(order => order.Total)),
                                           avgAmount = group.Average(customer => customer.Orders.Count())
                                       });

            foreach (var avg in avgOrdersTotal)
            {
                Console.WriteLine("City: {0}, Average total order price: {1:0.00}, Average amount: {2: 0.00}", 
                    avg.city, avg.avg, avg.avgAmount);
            }
        }

        [Category("Homework")]
        [Title("Task 10_1")]
        [Description("Gets average customer statistics by months")]
        public void Linq10_1()
        {
            var customers = dataSource.Customers;
            var resultGroup = customers.Select(c => c.Orders.GroupBy(o => o.OrderDate.Month)
                                .Select(group => new
                                {
                                    month = group.Key,
                                    count = group.Count(),
                                    custId = c.CustomerID
                                }));

            foreach (var group in resultGroup)
            {
                foreach (var subGroup in group)
                {
                    Console.WriteLine("CustomerID: {0}, Month: {1}, Count: {2}",
                        subGroup.custId, subGroup.month, subGroup.count);
                }
            }
        }

        [Category("Homework")]
        [Title("Task 10_2")]
        [Description("Gets average customer statistics by years")]
        public void Linq10_2()
        {
            var customers = dataSource.Customers;
            var resultGroup = customers.Select(c => c.Orders.GroupBy(o => o.OrderDate.Year)
                                .Select(group => new
                                {
                                    year = group.Key,
                                    count = group.Count(),
                                    custId = c.CustomerID
                                }));

            foreach (var group in resultGroup)
            {
                foreach (var subGroup in group)
                {
                    Console.WriteLine("CustomerID: {0}, Year: {1}, Count: {2}",
                        subGroup.custId, subGroup.year, subGroup.count);
                }
            }
        }

        [Category("Homework")]
        [Title("Task 10_3")]
        [Description("Gets average customer statistics by years and months")]
        public void Linq10_3()
        {
            var customers = dataSource.Customers;
            var resultGroup = customers.Select(c => c.Orders.GroupBy(o => o.OrderDate.Year)
                                .Select(group => group.GroupBy(y => y.OrderDate.Month)
                                .Select(gp => new
                                {
                                    custId = c.CustomerID,
                                    year = group.Key,
                                    month = gp.Key,
                                    count = gp.Count(),
                                })));

            foreach (var group in resultGroup)
            {
                foreach (var yearGroup in group)
                {
                    foreach (var monthGroup in yearGroup)
                    {
                        Console.WriteLine("CustomerID: {0}, Year: {1}, Month: {2}, Count: {3}",
                                          monthGroup.custId, monthGroup.year, monthGroup.month, monthGroup.count);
                    }
                }
            }
        }
    }
}
