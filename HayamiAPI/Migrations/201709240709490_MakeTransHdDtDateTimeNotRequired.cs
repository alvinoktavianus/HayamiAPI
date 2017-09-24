namespace HayamiAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeTransHdDtDateTimeNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransactionDts", "CreatedAt", c => c.DateTime());
            AlterColumn("dbo.TransactionDts", "ActionDate", c => c.DateTime());
            AlterColumn("dbo.TransactionDts", "UpdDate", c => c.DateTime());
            AlterColumn("dbo.TransactionHds", "TransDate", c => c.DateTime());
            AlterColumn("dbo.TransactionHds", "CreatedAt", c => c.DateTime());
            AlterColumn("dbo.TransactionHds", "UpdDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransactionHds", "UpdDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TransactionHds", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TransactionHds", "TransDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TransactionDts", "UpdDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TransactionDts", "ActionDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TransactionDts", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
