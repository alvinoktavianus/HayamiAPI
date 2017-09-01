namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToManyRelationshipProductHdDt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDts",
                c => new
                    {
                        ProductDtID = c.Int(nullable: false, identity: true),
                        ProductSize = c.String(),
                        ProductQty = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        ProductHdID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductDtID)
                .ForeignKey("dbo.ProductHds", t => t.ProductHdID, cascadeDelete: true)
                .Index(t => t.ProductHdID);
            
            AddColumn("dbo.ProductHds", "ProductDtID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductDts", "ProductHdID", "dbo.ProductHds");
            DropIndex("dbo.ProductDts", new[] { "ProductHdID" });
            DropColumn("dbo.ProductHds", "ProductDtID");
            DropTable("dbo.ProductDts");
        }
    }
}
