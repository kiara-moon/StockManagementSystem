namespace DotNetCoders.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseInfoChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PurchaseInfoes", "Code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseInfoes", "Code", c => c.String(nullable: false, maxLength: 4));
        }
    }
}
