namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mostRecent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payees", "AppUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Payee_PayeeID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Payees", "AppUser_Id1");
            CreateIndex("dbo.AspNetUsers", "Payee_PayeeID");
            AddForeignKey("dbo.Payees", "AppUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Payee_PayeeID", "dbo.Payees", "PayeeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Payee_PayeeID", "dbo.Payees");
            DropForeignKey("dbo.Payees", "AppUser_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Payee_PayeeID" });
            DropIndex("dbo.Payees", new[] { "AppUser_Id1" });
            DropColumn("dbo.AspNetUsers", "Payee_PayeeID");
            DropColumn("dbo.Payees", "AppUser_Id1");
        }
    }
}
