namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDiscountsAddDiscValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discounts", "DiscValue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discounts", "DiscValue");
        }
    }
}
