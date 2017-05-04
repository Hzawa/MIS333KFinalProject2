namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seed : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Disputes", new[] { "TransactionID" });
            DropPrimaryKey("dbo.Disputes");
            AddColumn("dbo.Payees", "Type", c => c.Int(nullable: false));
            AlterColumn("dbo.Disputes", "TransactionID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Disputes", "DisputeID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Disputes", "DisputeID");
            CreateIndex("dbo.Disputes", "TransactionID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Disputes", new[] { "TransactionID" });
            DropPrimaryKey("dbo.Disputes");
            AlterColumn("dbo.Disputes", "DisputeID", c => c.String());
            AlterColumn("dbo.Disputes", "TransactionID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Payees", "Type");
            AddPrimaryKey("dbo.Disputes", "TransactionID");
            CreateIndex("dbo.Disputes", "TransactionID");
        }
    }
}
