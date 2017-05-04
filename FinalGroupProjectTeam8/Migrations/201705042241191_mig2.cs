namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppUserPayees", "Payee_PayeeID", "dbo.Payees");
            DropForeignKey("dbo.AppUserPayees", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AppUserPayees", new[] { "Payee_PayeeID" });
            DropIndex("dbo.AppUserPayees", new[] { "AppUser_Id" });
            DropTable("dbo.AppUserPayees");
        }
    }
}
