namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MergeBranch3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductDts", "StorageID", "dbo.Storages");
            DropIndex("dbo.ProductDts", new[] { "StorageID" });
        }
        
        public override void Down()
        {
        }
    }
}
