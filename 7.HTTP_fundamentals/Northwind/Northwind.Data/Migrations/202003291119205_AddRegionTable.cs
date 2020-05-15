namespace Northwind.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRegionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Region",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        RegionDescription = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RegionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Region");
        }
    }
}
