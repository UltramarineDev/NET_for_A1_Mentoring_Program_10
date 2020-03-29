using Northwind.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Northwind.Data.Configurations
{
    public class RegionEntityConfiguration : EntityTypeConfiguration<Region>
    {
        public RegionEntityConfiguration()
        {
            this.Property(p => p.RegionDescription).HasMaxLength(50).IsRequired();
            this.ToTable("Regions");
        }
    }
}
