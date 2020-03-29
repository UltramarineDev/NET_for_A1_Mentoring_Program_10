using Northwind.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Northwind.Data.Configurations
{
    public class SupplierEntityConfiguration: EntityTypeConfiguration<Supplier>
    {
        public SupplierEntityConfiguration()
        {
            this.Property(p => p.HomePage).HasColumnType("ntext");
            this.Property(p => p.CompanyName).HasMaxLength(40).IsRequired();
            this.Property(p => p.ContactName).HasMaxLength(30);
            this.Property(p => p.ContactTitle).HasMaxLength(30);
            this.Property(p => p.Address).HasMaxLength(60);
            this.Property(p => p.City).HasMaxLength(15);
            this.Property(p => p.Region).HasMaxLength(15);
            this.Property(p => p.PostalCode).HasMaxLength(10);
            this.Property(p => p.Country).HasMaxLength(15);
            this.Property(p => p.Phone).HasMaxLength(24);
            this.Property(p => p.Fax).HasMaxLength(24);
        }
    }
}
