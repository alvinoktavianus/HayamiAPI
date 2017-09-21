namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveForeignKeyOnTransactionHd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransactionHds", "CounterID", "dbo.Counters");
            DropForeignKey("dbo.TransactionHds", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.ProductHds", "TypeID", "dbo.Types");
            DropIndex("dbo.ProductHds", new[] { "TypeID" });
            DropIndex("dbo.TransactionHds", new[] { "CounterID" });
            DropIndex("dbo.TransactionHds", new[] { "CustomerID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.TransactionHds", "CustomerID");
            CreateIndex("dbo.TransactionHds", "CounterID");
            CreateIndex("dbo.ProductHds", "TypeID");
            AddForeignKey("dbo.ProductHds", "TypeID", "dbo.Types", "TypeID", cascadeDelete: true);
            AddForeignKey("dbo.TransactionHds", "CustomerID", "dbo.Customers", "CustomerID", cascadeDelete: true);
            AddForeignKey("dbo.TransactionHds", "CounterID", "dbo.Counters", "CounterID", cascadeDelete: true);
        }
    }
}
