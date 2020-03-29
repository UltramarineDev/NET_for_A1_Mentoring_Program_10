using Northwind.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Northwind.Data.Configurations
{
    class OrderDetailsEntityConfiguration : EntityTypeConfiguration<OrderDetails>
    {
        public OrderDetailsEntityConfiguration()
        {
            this.HasKey(c => new { c.OrderId, c.ProductId });
            this.Property(p => p.UnitPrice).HasColumnType("money");
            this.ToTable("OrderDetails");
        }
    }
}
