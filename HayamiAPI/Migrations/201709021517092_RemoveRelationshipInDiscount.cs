namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRelationshipInDiscount : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Discounts", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Discounts", new[] { "CustomerID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Discounts", "CustomerID");
            AddForeignKey("dbo.Discounts", "CustomerID", "dbo.Customers", "CustomerID", cascadeDelete: true);
        }
    }
}
