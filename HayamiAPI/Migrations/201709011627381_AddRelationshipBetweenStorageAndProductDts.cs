namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationshipBetweenStorageAndProductDts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductDts", "StorageID", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductDts", "StorageID");
            AddForeignKey("dbo.ProductDts", "StorageID", "dbo.Storages", "StorageID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductDts", "StorageID", "dbo.Storages");
            DropIndex("dbo.ProductDts", new[] { "StorageID" });
            DropColumn("dbo.ProductDts", "StorageID");
        }
    }
}
