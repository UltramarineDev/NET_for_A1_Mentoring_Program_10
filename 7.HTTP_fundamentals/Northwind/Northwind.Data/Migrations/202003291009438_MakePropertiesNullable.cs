namespace Northwind.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakePropertiesNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "UnitPrice", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.Products", "UnitsInStock", c => c.Short());
            AlterColumn("dbo.Products", "UnitsOnOrder", c => c.Short());
            AlterColumn("dbo.Products", "ReorderLevel", c => c.Short());
            AlterColumn("dbo.Orders", "OrderDate", c => c.DateTime());
            AlterColumn("dbo.Orders", "RequiredDate", c => c.DateTime());
            AlterColumn("dbo.Orders", "ShippedDate", c => c.DateTime());
            AlterColumn("dbo.Orders", "Freight", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.Employees", "BirthDate", c => c.DateTime());
            AlterColumn("dbo.Employees", "HireDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "HireDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "BirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "Freight", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Orders", "ShippedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "RequiredDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "OrderDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "ReorderLevel", c => c.Short(nullable: false));
            AlterColumn("dbo.Products", "UnitsOnOrder", c => c.Short(nullable: false));
            AlterColumn("dbo.Products", "UnitsInStock", c => c.Short(nullable: false));
            AlterColumn("dbo.Products", "UnitPrice", c => c.Decimal(nullable: false, storeType: "money"));
        }
    }
}
