using Northwind.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Northwind.Data.Configurations
{
    public class CreditDetailConfiguration : EntityTypeConfiguration<CreditDetail>
    {
        public CreditDetailConfiguration()
        {
            this.Property(p => p.CardHolderName).HasMaxLength(40);
        }
    }
}
