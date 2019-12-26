namespace DotNetCoders.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesInfoAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalesInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Code = c.String(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.SalesProductInfoes", "SalesInfoId", c => c.Int(nullable: false));
            CreateIndex("dbo.SalesProductInfoes", "SalesInfoId");
            AddForeignKey("dbo.SalesProductInfoes", "SalesInfoId", "dbo.SalesInfoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesProductInfoes", "SalesInfoId", "dbo.SalesInfoes");
            DropForeignKey("dbo.SalesInfoes", "CustomerId", "dbo.Customers");
            DropIndex("dbo.SalesInfoes", new[] { "CustomerId" });
            DropIndex("dbo.SalesProductInfoes", new[] { "SalesInfoId" });
            DropColumn("dbo.SalesProductInfoes", "SalesInfoId");
            DropTable("dbo.SalesInfoes");
        }
    }
}
