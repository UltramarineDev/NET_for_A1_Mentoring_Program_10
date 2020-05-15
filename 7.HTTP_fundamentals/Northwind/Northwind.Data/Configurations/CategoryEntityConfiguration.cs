using Northwind.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Northwind.Data.Configurations
{
    public class CategoryEntityConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryEntityConfiguration()
        {
            this.Property(p => p.CategoryName).HasMaxLength(15).IsRequired();
            this.Property(p => p.Description).HasColumnType("ntext");
            this.Property(p => p.Picture).HasColumnType("image");
        }
    }
}
