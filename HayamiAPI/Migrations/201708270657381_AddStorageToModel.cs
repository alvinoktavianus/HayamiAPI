namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStorageToModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        StorageID = c.Int(nullable: false, identity: true),
                        StorageName = c.String(),
                        StorageCapacity = c.Int(nullable: false),
                        StorageStock = c.Int(nullable: false),
                        StoragePrior = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.StorageID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Storages");
        }
    }
}
