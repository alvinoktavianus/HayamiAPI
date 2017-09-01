namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SynchronizedRelationshipOnProductHd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductHds", "ProductDtID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductHds", "ProductDtID", c => c.Int(nullable: false));
        }
    }
}
