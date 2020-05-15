using Northwind.Data.Configurations;
using Northwind.Data.Entities;
using System.Data.Entity;
using Northwind.Data.Migrations;

namespace Northwind.Data
{
    public class NorthwindDataContext : DbContext
    {
        public NorthwindDataContext() : base("NorthwindDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NorthwindDataContext, Configuration>());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CreditDetail> CreditDetails { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Territory> Territories { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SupplierEntityConfiguration());
            modelBuilder.Configurations.Add(new ShipperEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductEntityConfiguration());
            modelBuilder.Configurations.Add(new OrderEntityConfiguration());
            modelBuilder.Configurations.Add(new EmployeeEntityConfiguration());
            modelBuilder.Configurations.Add(new CustomerEntityConfiguration());
            modelBuilder.Configurations.Add(new CategoryEntityConfiguration());
            modelBuilder.Configurations.Add(new CreditDetailConfiguration());
            modelBuilder.Configurations.Add(new RegionEntityConfiguration());
            modelBuilder.Configurations.Add(new TerritoryEntityConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailsEntityConfiguration());
        }
    }
}
