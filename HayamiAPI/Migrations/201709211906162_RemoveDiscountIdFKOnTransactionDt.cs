namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDiscountIdFKOnTransactionDt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransactionDts", "DiscountID", "dbo.Discounts");
            DropIndex("dbo.TransactionDts", new[] { "DiscountID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.TransactionDts", "DiscountID");
            AddForeignKey("dbo.TransactionDts", "DiscountID", "dbo.Discounts", "DiscountID", cascadeDelete: true);
        }
    }
}
