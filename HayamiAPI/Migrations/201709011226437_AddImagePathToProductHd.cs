namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePathToProductHd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductHds", "ImagePath1", c => c.String());
            AddColumn("dbo.ProductHds", "ImagePath2", c => c.String());
            AddColumn("dbo.ProductHds", "ImagePath3", c => c.String());
            AddColumn("dbo.ProductHds", "ImagePath4", c => c.String());
            AddColumn("dbo.ProductHds", "ImagePath5", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductHds", "ImagePath5");
            DropColumn("dbo.ProductHds", "ImagePath4");
            DropColumn("dbo.ProductHds", "ImagePath3");
            DropColumn("dbo.ProductHds", "ImagePath2");
            DropColumn("dbo.ProductHds", "ImagePath1");
        }
    }
}
