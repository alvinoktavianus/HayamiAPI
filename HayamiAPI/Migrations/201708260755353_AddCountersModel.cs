namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountersModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Counters",
                c => new
                    {
                        CounterID = c.Int(nullable: false, identity: true),
                        CounterName = c.String(),
                        CounterAddr = c.String(),
                        CounterCity = c.String(),
                        CounterPosCode = c.String(),
                        CounterPhone = c.String(),
                        CounterEmail = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.CounterID)
                .Index(t => t.CounterEmail, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Counters", new[] { "CounterEmail" });
            DropTable("dbo.Counters");
        }
    }
}
