namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
            
            AddColumn("dbo.Disputes", "CorrectAmount", c => c.Int(nullable: false));
            AddColumn("dbo.Disputes", "RequestDeletion", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Disputes", "Comments", c => c.String(nullable: false));
            DropColumn("dbo.Disputes", "CorrentAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Disputes", "CorrentAmount", c => c.Int(nullable: false));
            DropForeignKey("dbo.AppUserPayees", "Payee_PayeeID", "dbo.Payees");
            DropForeignKey("dbo.AppUserPayees", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AppUserPayees", new[] { "Payee_PayeeID" });
            DropIndex("dbo.AppUserPayees", new[] { "AppUser_Id" });
            AlterColumn("dbo.Disputes", "Comments", c => c.String());
            DropColumn("dbo.Disputes", "RequestDeletion");
            DropColumn("dbo.Disputes", "CorrectAmount");
            DropTable("dbo.AppUserPayees");
        }
    }
}
