using Northwind.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Northwind.Data.Configurations
{
    public class ProductEntityConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductEntityConfiguration()
        {
            this.Property(p => p.ProductName).HasMaxLength(40).IsRequired();
            this.Property(p => p.QuantityPerUnit).HasMaxLength(20);
            this.Property(p => p.UnitPrice).HasColumnType("money");
        }
    }
}
