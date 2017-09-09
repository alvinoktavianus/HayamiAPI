namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTransactionAndDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionDts",
                c => new
                    {
                        TransDtID = c.Int(nullable: false, identity: true),
                        TransHdID = c.Int(nullable: false),
                        ProductHdID = c.Int(nullable: false),
                        ProductSize = c.String(),
                        TotalPrice = c.Decimal(nullable: false, precision: 23, scale: 6),
                        Qty = c.Int(nullable: false),
                        QtyOri = c.Int(nullable: false),
                        ReceiveQty = c.Int(nullable: false),
                        AddDiscountType = c.String(maxLength: 1),
                        AddDiscountValue = c.Decimal(nullable: false, precision: 23, scale: 6),
                        AddDiscountDesc = c.String(),
                        DiscountID = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        RejectDesc = c.String(),
                        ActionDate = c.DateTime(nullable: false),
                        FgStatus = c.String(maxLength: 1),
                        FgStatusStr = c.String(maxLength: 1),
                        UpdDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.TransDtID)
                .ForeignKey("dbo.Discounts", t => t.DiscountID, cascadeDelete: true)
                .ForeignKey("dbo.TransactionHds", t => t.TransHdID, cascadeDelete: true)
                .Index(t => t.TransHdID)
                .Index(t => t.DiscountID);
            
            CreateTable(
                "dbo.TransactionHds",
                c => new
                    {
                        TransHdID = c.Int(nullable: false, identity: true),
                        TransNo = c.String(),
                        TransDate = c.DateTime(nullable: false),
                        CounterID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        FgStatus = c.String(maxLength: 1),
                        TotalDiscount = c.Decimal(nullable: false, precision: 23, scale: 6),
                        TotalPrice = c.Decimal(nullable: false, precision: 23, scale: 6),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.TransHdID)
                .ForeignKey("dbo.Counters", t => t.CounterID, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CounterID)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionDts", "TransHdID", "dbo.TransactionHds");
            DropForeignKey("dbo.TransactionHds", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.TransactionHds", "CounterID", "dbo.Counters");
            DropForeignKey("dbo.TransactionDts", "DiscountID", "dbo.Discounts");
            DropIndex("dbo.TransactionHds", new[] { "CustomerID" });
            DropIndex("dbo.TransactionHds", new[] { "CounterID" });
            DropIndex("dbo.TransactionDts", new[] { "DiscountID" });
            DropIndex("dbo.TransactionDts", new[] { "TransHdID" });
            DropTable("dbo.TransactionHds");
            DropTable("dbo.TransactionDts");
        }
    }
}
