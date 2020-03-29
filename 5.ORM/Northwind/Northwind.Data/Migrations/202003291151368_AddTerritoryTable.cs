namespace Northwind.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTerritoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Territories",
                c => new
                    {
                        TerritoryId = c.String(nullable: false, maxLength: 20),
                        TerritoryDescription = c.String(nullable: false, maxLength: 50),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TerritoryId)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Territories", "RegionId", "dbo.Regions");
            DropIndex("dbo.Territories", new[] { "RegionId" });
            DropTable("dbo.Territories");
        }
    }
}
