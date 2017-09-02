namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDiscountToModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        DiscountID = c.Int(nullable: false, identity: true),
                        DiscDivide = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DiscountID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Discounts", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Discounts", new[] { "CustomerID" });
            DropTable("dbo.Discounts");
        }
    }
}
