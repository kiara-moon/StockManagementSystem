namespace DotNetCoders.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseInfosAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Code = c.String(),
                        InvoiceNo = c.String(),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            AddColumn("dbo.PurchaseProductInfoes", "PurchaseInfoId", c => c.Int(nullable: false));
            CreateIndex("dbo.PurchaseProductInfoes", "PurchaseInfoId");
            CreateIndex("dbo.PurchaseProductInfoes", "ProductId");
            AddForeignKey("dbo.PurchaseProductInfoes", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PurchaseProductInfoes", "PurchaseInfoId", "dbo.PurchaseInfoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseInfoes", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.PurchaseProductInfoes", "PurchaseInfoId", "dbo.PurchaseInfoes");
            DropForeignKey("dbo.PurchaseProductInfoes", "ProductId", "dbo.Products");
            DropIndex("dbo.PurchaseInfoes", new[] { "SupplierId" });
            DropIndex("dbo.PurchaseProductInfoes", new[] { "ProductId" });
            DropIndex("dbo.PurchaseProductInfoes", new[] { "PurchaseInfoId" });
            DropColumn("dbo.PurchaseProductInfoes", "PurchaseInfoId");
            DropTable("dbo.PurchaseInfoes");
        }
    }
}
