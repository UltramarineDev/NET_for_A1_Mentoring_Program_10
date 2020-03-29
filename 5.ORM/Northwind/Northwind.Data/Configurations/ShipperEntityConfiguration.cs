using Northwind.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Northwind.Data.Configurations
{
    public class ShipperEntityConfiguration: EntityTypeConfiguration<Shipper>
    {
        public ShipperEntityConfiguration()
        {
            this.Property(p => p.CompanyName).HasMaxLength(40).IsRequired();
            this.Property(p => p.Phone).HasMaxLength(24);
        }
    }
}
