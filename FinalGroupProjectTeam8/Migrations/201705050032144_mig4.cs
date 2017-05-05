namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payees", "Payee_PayeeID", "dbo.Payees");
            DropForeignKey("dbo.Payees", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Payees", new[] { "Payee_PayeeID" });
            DropIndex("dbo.Payees", new[] { "AppUser_Id" });
            CreateTable(
                "dbo.AppUserPayees",
                c => new
                    {
                        AppUser_Id = c.String(nullable: false, maxLength: 128),
                        Payee_PayeeID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.AppUser_Id, t.Payee_PayeeID })
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Payees", t => t.Payee_PayeeID, cascadeDelete: true)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Payee_PayeeID);
            
            DropColumn("dbo.Payees", "Payee_PayeeID");
            DropColumn("dbo.Payees", "AppUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payees", "AppUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Payees", "Payee_PayeeID", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AppUserPayees", "Payee_PayeeID", "dbo.Payees");
            DropForeignKey("dbo.AppUserPayees", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AppUserPayees", new[] { "Payee_PayeeID" });
            DropIndex("dbo.AppUserPayees", new[] { "AppUser_Id" });
            DropTable("dbo.AppUserPayees");
            CreateIndex("dbo.Payees", "AppUser_Id");
            CreateIndex("dbo.Payees", "Payee_PayeeID");
            AddForeignKey("dbo.Payees", "AppUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Payees", "Payee_PayeeID", "dbo.Payees", "PayeeID");
        }
    }
}
