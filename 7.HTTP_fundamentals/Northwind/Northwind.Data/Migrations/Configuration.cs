using Northwind.Data.Entities;
using System;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using Region = Northwind.Data.Entities.Region;

namespace Northwind.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<NorthwindDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NorthwindDataContext context)
        {
           // SeedDatabase(context);
        }

        private void SeedDatabase(NorthwindDataContext context)
        {
            var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            context.Categories.AddOrUpdate(
                i => i.CategoryId,
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Beverages",
                    Description = "Soft drinks, coffees, teas"
                    // Picture = (byte[])(new ImageConverter()).ConvertTo(Image.FromFile(Path.Combine(projectDirectory, "Images\\Beverages.jpg")), typeof(byte[]))
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Condiments",
                    Description = "Sweet and savory sauces, relishes, spreads"
                   // Picture = (byte[])(new ImageConverter()).ConvertTo(Image.FromFile(Path.Combine(projectDirectory, "Images\\Condiments.jpg")), typeof(byte[]))
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Seafood",
                    Description = "Seaweed and fish"
                    // Picture = (byte[])(new ImageConverter()).ConvertTo(Image.FromFile(Path.Combine(projectDirectory, "Images\\Seafood.jpg")), typeof(byte[]))
                }
                );

            context.Shippers.AddOrUpdate(
                i => i.ShipperId,
                new Shipper() { ShipperId = 1, CompanyName = "Speedy Express ChangedData", Phone = "(504) 555-9831" },
                new Shipper() { ShipperId = 2, CompanyName = "United Package ChangedHere", Phone = "(504) 555-3199" },
                new Shipper() { ShipperId = 3, CompanyName = "Federal Shipping", Phone = "(503) 555-9931" }
                );

            context.Employees.AddOrUpdate(
                i => i.EmployeeId,
                new Employee()
                {
                    EmployeeId = 1,
                    LastName = "Davolio",
                    FirstName = "Nancy",
                    Title = "Sales Representative",
                    TitleOfCourtesy = "Ms.",
                    BirthDate = new DateTime(1948, 08, 12),
                    HireDate = new DateTime(1992, 05, 01),
                    Address = "507 - 20th Ave. E.Apt. 2A",
                    City = "Seattle",
                    Region = "Western",
                    PostalCode = "98122",
                    Country = "USA",
                    HomePhone = "(206) 555-9857",
                    Extension = "5467",
                    Notes = "Education includes a BA in psychology from Colorado State University in 1970.",
                    ReportsTo = 2,
                   // Photo = (byte[])(new ImageConverter()).ConvertTo(Image.FromFile(Path.Combine(projectDirectory, "Images\\Employee1.jpg")), typeof(byte[])),
                    PhotoPath = "http://accweb/emmployees/davolio.bmp",
                },
                new Employee()
                {
                    EmployeeId = 2,
                    LastName = "Fuller",
                    FirstName = "Andrew",
                    Title = "Vice President, Sales",
                    TitleOfCourtesy = "Dr.",
                    BirthDate = new DateTime(1952, 02, 19),
                    HireDate = new DateTime(1992, 08, 14),
                    Address = "908 W. Capital Way",
                    City = "Tacoma",
                    Region = "Western",
                    PostalCode = "98401",
                    Country = "USA",
                    HomePhone = "(206) 555-9482",
                    Extension = "3457",
                    Notes = "Andrew received his BTS commercial in 1974 and a Ph.D. in international marketing from the University of Dallas in 1981.  He is fluent in French and Italian and reads German.  He joined the company as a sales representative, was promoted to sales manager in January 1992 and to vice president of sales in March 1993.  Andrew is a member of the Sales Management Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association.",
                    ReportsTo = null,
                   // Photo = (byte[])(new ImageConverter()).ConvertTo(Image.FromFile(Path.Combine(projectDirectory, "Images\\Employee2.jpg")), typeof(byte[])),
                    PhotoPath = "http://accweb/emmployees/fuller.bmp",
                },
                new Employee()
                {
                    EmployeeId = 3,
                    LastName = "Leverling",
                    FirstName = "Janet",
                    Title = "Sales Representative",
                    TitleOfCourtesy = "Ms.",
                    BirthDate = new DateTime(1963, 08, 30),
                    HireDate = new DateTime(1992, 04, 01),
                    Address = "722 Moss Bay Blvd.",
                    City = "Kirkland",
                    Region = "Western",
                    PostalCode = "98033",
                    Country = "UK",
                    HomePhone = "(206) 555-3412",
                    Extension = "3355",
                    Notes = "Janet has a BS degree in chemistry from Boston College (1984).  She has also completed a certificate program in food retailing management.  Janet was hired as a sales associate in 1991 and promoted to sales representative in February 1992.",
                    ReportsTo = 2,
                  //  Photo = (byte[])(new ImageConverter()).ConvertTo(Image.FromFile(Path.Combine(projectDirectory, "Images\\Employee3.jpg")), typeof(byte[])),
                    PhotoPath = "http://accweb/emmployees/leverling.bmp",
                }
                );

            context.Customers.AddOrUpdate(
                i => i.CustomerId,
                new Customer()
                {
                    CustomerId = "ALFKI",
                    CompanyName = "Alfreds Futterkiste",
                    ContactName = "Maria Anders",
                    ContactTitle = "Sales Representative",
                    Address = "Obere Str. 57",
                    City = "Berlin",
                    Region = null,
                    PostalCode = "12209",
                    Country = "Germany",
                    Phone = "030-0074321",
                    Fax = "030-0076545"
                },
                new Customer()
                {
                    CustomerId = "ANATR",
                    CompanyName = "Ana Trujillo Emparedados y helados",
                    ContactName = "Ana Trujillo",
                    ContactTitle = "Owner",
                    Address = "Avda. de la Constitucion 2222",
                    City = "Mexico D.F.",
                    Region = null,
                    PostalCode = "05021",
                    Country = "Mexico",
                    Phone = "(5) 555-4729",
                    Fax = "(5) 555-3745"
                },
                new Customer()
                {
                    CustomerId = "ANTON",
                    CompanyName = "Antonio Moreno Taqueria",
                    ContactName = "Antonio Moreno",
                    ContactTitle = "Owner",
                    Address = "Mataderos  2312",
                    City = "Mexico D.F.",
                    Region = null,
                    PostalCode = "05023",
                    Country = "Mexico",
                    Phone = "(5) 555-3932",
                    Fax = null
                });

            context.Orders.AddOrUpdate(
                i => i.OrderId,
                new Order()
                {
                    OrderId = 1,
                    CustomerId = "ANTON",
                    EmployeeId = 1,
                    OrderDate = new DateTime(1996, 07, 04),
                    RequiredDate = new DateTime(1996, 08, 01),
                    ShippedDate = new DateTime(1996, 07, 16),
                    ShipVia = 2,
                    Freight = 32.38M,
                    ShipName = "Vins et alcools Chevalier",
                    ShipAddress = "59 rue de l'Abbaye",
                    ShipCity = "Reims",
                    ShipRegion = null,
                    ShipPostalCode = "51100",
                    ShipCountry = "France"
                },
                new Order()
                {
                    OrderId = 2,
                    CustomerId = "ANATR",
                    EmployeeId = 3,
                    OrderDate = new DateTime(1996, 07, 05),
                    RequiredDate = new DateTime(1996, 08, 16),
                    ShippedDate = new DateTime(1996, 07, 10),
                    ShipVia = 1,
                    Freight = 11.61M,
                    ShipName = "Toms Spezialitäten",
                    ShipAddress = "Luisenstr. 48",
                    ShipCity = "Münster",
                    ShipRegion = null,
                    ShipPostalCode = "44087",
                    ShipCountry = "Germany"
                },
                new Order()
                {
                    OrderId = 3,
                    CustomerId = "ALFKI",
                    EmployeeId = 2,
                    OrderDate = new DateTime(1996, 07, 08),
                    RequiredDate = new DateTime(1996, 08, 5),
                    ShippedDate = new DateTime(1996, 07, 12),
                    ShipVia = 1,
                    Freight = 65.83M,
                    ShipName = "Hanari Carnes",
                    ShipAddress = "Rua do Paço, 67",
                    ShipCity = "Rio de Janeiro",
                    ShipRegion = null,
                    ShipPostalCode = "05454-876",
                    ShipCountry = "Brazil"
                });

            context.Regions.AddOrUpdate(
                i => i.RegionId,
                new Region
                {
                    RegionId = 1,
                    RegionDescription = "Eastern"
                });


            context.Territories.AddOrUpdate(
                i => i.TerritoryId,
                new Territory
                {
                    TerritoryId = "01581",
                    TerritoryDescription = "Westboro",
                    RegionId = 1
                },
                new Territory
                {
                    TerritoryId = "01730",
                    TerritoryDescription = "Bedford",
                    RegionId = 1
                }
                );

            context.Suppliers.AddOrUpdate(
                i => i.SupplierId,
                new Supplier()
                {
                    SupplierId = 1,
                    CompanyName = "Exotic Liquids",
                    ContactName = "Charlotte Cooper",
                    ContactTitle = "Purchasing Manager",
                    Address = "49 Gilbert St.",
                    City = "London",
                    PostalCode = "EC1 4SD",
                    Country = "UK",
                    Phone = "(171) 555-2222"
                },
                new Supplier()
                {
                    SupplierId = 2,
                    CompanyName = "New Orleans Cajun Delights",
                    ContactName = "Shelley Burke",
                    ContactTitle = "Order Administrator",
                    Address = "P.O. Box 78934",
                    City = "New Orleans",
                    PostalCode = "70117",
                    Country = "UK",
                    Phone = "(100) 555-4822",
                    Region = "LA"
                }
                );

            context.Products.AddOrUpdate(
                i => i.ProductId,
                new Product
                {
                    ProductId = 1,
                    ProductName = "Chai",
                    SupplierId = 1,
                    CategoryId = 1,
                    QuantityPerUnit = "10 boxes x 20 bags",
                    UnitPrice = 18.00M,
                    UnitsInStock = 39,
                    UnitsOnOrder = 0,
                    ReorderLevel = 10,
                    Discontinued = false
                },
                new Product
                {
                    ProductId = 2,
                    ProductName = "Aniseed Syrup",
                    SupplierId = 1,
                    CategoryId = 2,
                    QuantityPerUnit = "12 - 550 ml bottles",
                    UnitPrice = 10.00M,
                    UnitsInStock = 13,
                    UnitsOnOrder = 0,
                    ReorderLevel = 10,
                    Discontinued = false
                },
                new Product
                {
                    ProductId = 3,
                    ProductName = "Chang",
                    SupplierId = 1,
                    CategoryId = 1,
                    QuantityPerUnit = "24 - 12 oz bottles",
                    UnitPrice = 19.00M,
                    UnitsInStock = 17,
                    UnitsOnOrder = 40,
                    ReorderLevel = 10,
                    Discontinued = false
                },
                new Product
                {
                    ProductId = 4,
                    ProductName = "Chef Anton's Cajun Seasoning",
                    SupplierId = 2,
                    CategoryId = 2,
                    QuantityPerUnit = "48 - 6 oz jars",
                    UnitPrice = 22.00M,
                    UnitsInStock = 53,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = false
                });

            context.OrderDetails.AddOrUpdate(
                new OrderDetails
                {
                    OrderId = 1,
                    ProductId = 1,
                    UnitPrice = 18.00M,
                    Quantity = 12,
                    Discount = 0
                },
                new OrderDetails
                {
                    OrderId = 1,
                    ProductId = 2,
                    UnitPrice = 18.00M,
                    Quantity = 12,
                    Discount = 0
                },
                new OrderDetails
                {
                    OrderId = 2,
                    ProductId = 1,
                    UnitPrice = 18.00M,
                    Quantity = 12,
                    Discount = 0
                },
                new OrderDetails
                {
                    OrderId = 2,
                    ProductId = 3,
                    UnitPrice = 18.00M,
                    Quantity = 12,
                    Discount = 0
                },
                new OrderDetails
                {
                    OrderId = 2,
                    ProductId = 4,
                    UnitPrice = 18.00M,
                    Quantity = 12,
                    Discount = 0
                });
        }
    }
}
