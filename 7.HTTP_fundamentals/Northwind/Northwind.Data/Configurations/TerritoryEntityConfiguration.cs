using Northwind.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Northwind.Data.Configurations
{
    public class TerritoryEntityConfiguration : EntityTypeConfiguration<Territory>
    {
        public TerritoryEntityConfiguration()
        {
            this.Property(p => p.TerritoryId).HasMaxLength(20);
            this.Property(p => p.TerritoryDescription).HasMaxLength(50).IsRequired();
            this.Property(p => p.RegionId).IsRequired();
        }
    }
}
