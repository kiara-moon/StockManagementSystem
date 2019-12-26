namespace DotNetCoders.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PurchaseInfoes", "Code", c => c.String(nullable: false, maxLength: 4));
            AlterColumn("dbo.PurchaseInfoes", "InvoiceNo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseInfoes", "InvoiceNo", c => c.String());
            AlterColumn("dbo.PurchaseInfoes", "Code", c => c.String());
        }
    }
}
