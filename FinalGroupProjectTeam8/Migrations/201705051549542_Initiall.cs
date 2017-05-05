namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initiall : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        BankAccountID = c.String(nullable: false, maxLength: 128),
                        AccountType = c.Int(nullable: false),
                        Name = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Active = c.Boolean(nullable: false),
                        UserID = c.String(maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.BankAccountID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.String(nullable: false, maxLength: 128),
                        TransactionType = c.Int(nullable: false),
                        TransactionStatus = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comments = c.String(),
                        Date = c.DateTime(nullable: false),
                        BankAccountID = c.String(maxLength: 128),
                        PayeeID = c.String(maxLength: 128),
                        ReceivingBankAccountID = c.String(maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountID)
                .ForeignKey("dbo.Payees", t => t.PayeeID)
                .ForeignKey("dbo.BankAccounts", t => t.ReceivingBankAccountID)
                .Index(t => t.BankAccountID)
                .Index(t => t.PayeeID)
                .Index(t => t.ReceivingBankAccountID);
            
            CreateTable(
                "dbo.Disputes",
                c => new
                    {
                        DisputeID = c.String(nullable: false, maxLength: 128),
                        Comments = c.String(nullable: false),
                        DisputeType = c.Int(nullable: false),
                        CorrectAmount = c.Int(nullable: false),
                        RequestDeletion = c.Boolean(nullable: false),
                        TransactionID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DisputeID)
                .ForeignKey("dbo.Transactions", t => t.TransactionID)
                .Index(t => t.TransactionID);
            
            CreateTable(
                "dbo.Payees",
                c => new
                    {
                        PayeeID = c.String(nullable: false, maxLength: 128),
                        Type = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Zip = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PayeeID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FName = c.String(nullable: false),
                        MiddleInitial = c.String(),
                        LName = c.String(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Street = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Zip = c.String(nullable: false),
                        UserType = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        DefaultAccountID = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Transactions", "ReceivingBankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.Transactions", "PayeeID", "dbo.Payees");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AppUserPayees", "Payee_PayeeID", "dbo.Payees");
            DropForeignKey("dbo.AppUserPayees", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BankAccounts", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Disputes", "TransactionID", "dbo.Transactions");
            DropForeignKey("dbo.Transactions", "BankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.AppUserPayees", new[] { "Payee_PayeeID" });
            DropIndex("dbo.AppUserPayees", new[] { "AppUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Disputes", new[] { "TransactionID" });
            DropIndex("dbo.Transactions", new[] { "ReceivingBankAccountID" });
            DropIndex("dbo.Transactions", new[] { "PayeeID" });
            DropIndex("dbo.Transactions", new[] { "BankAccountID" });
            DropIndex("dbo.BankAccounts", new[] { "UserID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AppUserPayees");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Payees");
            DropTable("dbo.Disputes");
            DropTable("dbo.Transactions");
            DropTable("dbo.BankAccounts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
