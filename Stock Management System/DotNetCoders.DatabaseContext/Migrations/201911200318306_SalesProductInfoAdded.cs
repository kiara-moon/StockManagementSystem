namespace DotNetCoders.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesProductInfoAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalesProductInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        MRP = c.Double(nullable: false),
                        DiscountAmount = c.Double(nullable: false),
                        PayableAmount = c.Double(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesProductInfoes", "ProductId", "dbo.Products");
            DropIndex("dbo.SalesProductInfoes", new[] { "ProductId" });
            DropTable("dbo.SalesProductInfoes");
        }
    }
}
