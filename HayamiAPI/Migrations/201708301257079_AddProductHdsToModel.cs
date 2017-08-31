namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductHdsToModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductHds",
                c => new
                    {
                        ProductHdID = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(),
                        ProductName = c.String(),
                        ProductDesc = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        TypeID = c.Int(nullable: false),
                        ModelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductHdID)
                .ForeignKey("dbo.Models", t => t.ModelID, cascadeDelete: true)
                .ForeignKey("dbo.Types", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.TypeID)
                .Index(t => t.ModelID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductHds", "TypeID", "dbo.Types");
            DropForeignKey("dbo.ProductHds", "ModelID", "dbo.Models");
            DropIndex("dbo.ProductHds", new[] { "ModelID" });
            DropIndex("dbo.ProductHds", new[] { "TypeID" });
            DropTable("dbo.ProductHds");
        }
    }
}
