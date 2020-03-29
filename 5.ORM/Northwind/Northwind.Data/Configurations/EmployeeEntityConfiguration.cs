using Northwind.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Northwind.Data.Configurations
{
    public class EmployeeEntityConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeEntityConfiguration()
        {
            this.Property(p => p.LastName).HasMaxLength(20).IsRequired();
            this.Property(p => p.FirstName).HasMaxLength(10).IsRequired();
            this.Property(p => p.Title).HasMaxLength(30);
            this.Property(p => p.TitleOfCourtesy).HasMaxLength(25);
            this.Property(p => p.Address).HasMaxLength(60);
            this.Property(p => p.City).HasMaxLength(15);
            this.Property(p => p.Region).HasMaxLength(15);
            this.Property(p => p.PostalCode).HasMaxLength(10);
            this.Property(p => p.Country).HasMaxLength(15);
            this.Property(p => p.HomePhone).HasMaxLength(24);
            this.Property(p => p.Extension).HasMaxLength(4);
            this.Property(p => p.Photo).HasColumnType("image");
            this.Property(p => p.Notes).HasColumnType("ntext");
            this.Property(p => p.PhotoPath).HasMaxLength(255);
            this.HasOptional<Employee>(s => s.EmployeeBoss).WithMany(g => g.Employees).HasForeignKey<int?>(s => s.ReportsTo);
        }
    }
}
