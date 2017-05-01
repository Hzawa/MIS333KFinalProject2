namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "PayeeID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Transactions", "PayeeID");
            AddForeignKey("dbo.Transactions", "PayeeID", "dbo.Payees", "PayeeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "PayeeID", "dbo.Payees");
            DropIndex("dbo.Transactions", new[] { "PayeeID" });
            DropColumn("dbo.Transactions", "PayeeID");
        }
    }
}
