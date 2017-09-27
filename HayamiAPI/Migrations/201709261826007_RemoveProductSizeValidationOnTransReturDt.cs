namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProductSizeValidationOnTransReturDt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransactionReturDts", "ProductSize", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransactionReturDts", "ProductSize", c => c.String(maxLength: 10));
        }
    }
}
