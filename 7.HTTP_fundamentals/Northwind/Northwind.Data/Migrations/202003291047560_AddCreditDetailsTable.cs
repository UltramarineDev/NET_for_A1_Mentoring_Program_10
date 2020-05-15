namespace Northwind.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreditDetailsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditDetails",
                c => new
                    {
                        CreditDetailId = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(maxLength: 5, fixedLength: true),
                        CardNumber = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(),
                        CardHolderName = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.CreditDetailId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditDetails", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CreditDetails", new[] { "CustomerId" });
            DropTable("dbo.CreditDetails");
        }
    }
}
