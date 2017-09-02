namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRelationshipOnCustomers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "CounterID", "dbo.Counters");
            DropIndex("dbo.Customers", new[] { "CounterID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Customers", "CounterID");
            AddForeignKey("dbo.Customers", "CounterID", "dbo.Counters", "CounterID", cascadeDelete: true);
        }
    }
}
