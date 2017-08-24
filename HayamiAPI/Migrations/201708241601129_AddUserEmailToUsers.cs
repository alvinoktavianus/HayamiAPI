namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserEmailToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserEmail", c => c.String(maxLength: 100));
            CreateIndex("dbo.Users", "UserEmail", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "UserEmail" });
            DropColumn("dbo.Users", "UserEmail");
        }
    }
}
