namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeForeignKeyToCustomers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "Counter_CounterID", "dbo.Counters");
            DropIndex("dbo.Customers", new[] { "Counter_CounterID" });
            RenameColumn(table: "dbo.Customers", name: "Counter_CounterID", newName: "CounterID");
            AlterColumn("dbo.Customers", "CounterID", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "CounterID");
            AddForeignKey("dbo.Customers", "CounterID", "dbo.Counters", "CounterID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "CounterID", "dbo.Counters");
            DropIndex("dbo.Customers", new[] { "CounterID" });
            AlterColumn("dbo.Customers", "CounterID", c => c.Int());
            RenameColumn(table: "dbo.Customers", name: "CounterID", newName: "Counter_CounterID");
            CreateIndex("dbo.Customers", "Counter_CounterID");
            AddForeignKey("dbo.Customers", "Counter_CounterID", "dbo.Counters", "CounterID");
        }
    }
}
