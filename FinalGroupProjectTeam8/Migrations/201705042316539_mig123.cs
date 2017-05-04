namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payees", "Payee_PayeeID", c => c.String(maxLength: 128));
            AddColumn("dbo.Payees", "AppUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Payees", "Payee_PayeeID");
            CreateIndex("dbo.Payees", "AppUser_Id");
            AddForeignKey("dbo.Payees", "Payee_PayeeID", "dbo.Payees", "PayeeID");
            AddForeignKey("dbo.Payees", "AppUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payees", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Payees", "Payee_PayeeID", "dbo.Payees");
            DropIndex("dbo.Payees", new[] { "AppUser_Id" });
            DropIndex("dbo.Payees", new[] { "Payee_PayeeID" });
            DropColumn("dbo.Payees", "AppUser_Id");
            DropColumn("dbo.Payees", "Payee_PayeeID");
        }
    }
}
