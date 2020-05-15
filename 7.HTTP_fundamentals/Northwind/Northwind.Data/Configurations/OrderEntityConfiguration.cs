using Northwind.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Northwind.Data.Configurations
{
    public class OrderEntityConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderEntityConfiguration()
        {
            this.HasOptional<Shipper>(s => s.Shipper).WithMany(g => g.Orders).HasForeignKey<int?>(s => s.ShipVia);
            this.Property(p => p.Freight).HasColumnType("money");
            this.Property(p => p.ShipName).HasMaxLength(40);
            this.Property(p => p.ShipAddress).HasMaxLength(60);
            this.Property(p => p.ShipCity).HasMaxLength(15);
            this.Property(p => p.ShipRegion).HasMaxLength(15);
            this.Property(p => p.ShipPostalCode).HasMaxLength(10);
            this.Property(p => p.ShipCountry).HasMaxLength(15);
        }
    }
}
