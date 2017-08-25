namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTypesModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        TypeID = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                        TypePrice = c.Decimal(nullable: false, precision: 23, scale: 6),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.TypeID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Types");
        }
    }
}
