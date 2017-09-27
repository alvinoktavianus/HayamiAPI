namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTransactionReturHdDt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionReturDts",
                c => new
                    {
                        TransReturDtID = c.Int(nullable: false, identity: true),
                        TransReturHdID = c.Int(nullable: false),
                        ProductHdID = c.Int(nullable: false),
                        ProductSize = c.String(maxLength: 10),
                        ReturQty = c.Int(nullable: false),
                        ReturType = c.String(maxLength: 1),
                        ReturStatus = c.String(maxLength: 1),
                        CreatedAt = c.DateTime(),
                        UpdDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.TransReturDtID)
                .ForeignKey("dbo.TransactionReturHds", t => t.TransReturHdID, cascadeDelete: true)
                .Index(t => t.TransReturHdID);
            
            CreateTable(
                "dbo.TransactionReturHds",
                c => new
                    {
                        TransReturHdID = c.Int(nullable: false, identity: true),
                        CounterID = c.Int(nullable: false),
                        TransReturNo = c.String(maxLength: 25),
                        ReturStatus = c.String(maxLength: 1),
                        ReturDesc = c.String(maxLength: 255),
                        ActionDate = c.DateTime(),
                        CreatedAt = c.DateTime(),
                        UpdDate = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.TransReturHdID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionReturDts", "TransReturHdID", "dbo.TransactionReturHds");
            DropIndex("dbo.TransactionReturDts", new[] { "TransReturHdID" });
            DropTable("dbo.TransactionReturHds");
            DropTable("dbo.TransactionReturDts");
        }
    }
}
