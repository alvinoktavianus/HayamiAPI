namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerTableAndController : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustCode = c.String(),
                        CustName = c.String(),
                        CustAddr = c.String(),
                        CustCity = c.String(),
                        CustPosCode = c.String(),
                        CustPhone = c.String(),
                        CustEmail = c.String(maxLength: 100),
                        CustExp = c.String(),
                        CustDesc = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Counter_CounterID = c.Int(),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.Counters", t => t.Counter_CounterID)
                .Index(t => t.CustEmail, unique: true)
                .Index(t => t.Counter_CounterID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "Counter_CounterID", "dbo.Counters");
            DropIndex("dbo.Customers", new[] { "Counter_CounterID" });
            DropIndex("dbo.Customers", new[] { "CustEmail" });
            DropTable("dbo.Customers");
        }
    }
}
