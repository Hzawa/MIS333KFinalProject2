namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 128),
                        AccountType = c.Int(nullable: false),
                        AccountID = c.String(),
                        Name = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Active = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.String(nullable: false, maxLength: 128),
                        TransactionType = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comments = c.String(),
                        Date = c.DateTime(nullable: false),
                        AccountID = c.String(maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.Accounts", t => t.AccountID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.Disputes",
                c => new
                    {
                        TransactionID = c.String(nullable: false, maxLength: 128),
                        DisputeID = c.String(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.Transactions", t => t.TransactionID)
                .Index(t => t.TransactionID);
            
            CreateTable(
                "dbo.Payees",
                c => new
                    {
                        PayeeID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.PayeeID);
            
            AddColumn("dbo.AspNetUsers", "MiddleInitial", c => c.String());
            AddColumn("dbo.AspNetUsers", "LName", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Street", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "City", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "State", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Zip", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserType", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "DefaultAccountID", c => c.String());
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "FName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "CustomerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Disputes", "TransactionID", "dbo.Transactions");
            DropForeignKey("dbo.Transactions", "AccountID", "dbo.Accounts");
            DropIndex("dbo.Disputes", new[] { "TransactionID" });
            DropIndex("dbo.Transactions", new[] { "AccountID" });
            DropIndex("dbo.Accounts", new[] { "CustomerID" });
            AlterColumn("dbo.AspNetUsers", "FName", c => c.String());
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetUsers", "DefaultAccountID");
            DropColumn("dbo.AspNetUsers", "UserType");
            DropColumn("dbo.AspNetUsers", "Zip");
            DropColumn("dbo.AspNetUsers", "State");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Street");
            DropColumn("dbo.AspNetUsers", "Birthday");
            DropColumn("dbo.AspNetUsers", "LName");
            DropColumn("dbo.AspNetUsers", "MiddleInitial");
            DropTable("dbo.Payees");
            DropTable("dbo.Disputes");
            DropTable("dbo.Transactions");
            DropTable("dbo.Accounts");
        }
    }
}
