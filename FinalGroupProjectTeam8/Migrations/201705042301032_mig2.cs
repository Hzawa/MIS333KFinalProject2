namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppUserPayees", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AppUserPayees", "Payee_PayeeID", "dbo.Payees");
            DropIndex("dbo.AppUserPayees", new[] { "AppUser_Id" });
            DropIndex("dbo.AppUserPayees", new[] { "Payee_PayeeID" });
            DropTable("dbo.AppUserPayees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AppUserPayees",
                c => new
                    {
                        AppUser_Id = c.String(nullable: false, maxLength: 128),
                        Payee_PayeeID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.AppUser_Id, t.Payee_PayeeID });
            
            CreateIndex("dbo.AppUserPayees", "Payee_PayeeID");
            CreateIndex("dbo.AppUserPayees", "AppUser_Id");
            AddForeignKey("dbo.AppUserPayees", "Payee_PayeeID", "dbo.Payees", "PayeeID", cascadeDelete: true);
            AddForeignKey("dbo.AppUserPayees", "AppUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
