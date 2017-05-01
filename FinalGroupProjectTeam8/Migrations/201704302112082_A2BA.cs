namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A2BA : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Accounts", newName: "BankAccounts");
            RenameColumn(table: "dbo.Transactions", name: "AccountID", newName: "BankAccountID");
            RenameIndex(table: "dbo.Transactions", name: "IX_AccountID", newName: "IX_BankAccountID");
            AddColumn("dbo.BankAccounts", "BankAccountID", c => c.String());
            DropColumn("dbo.BankAccounts", "AccountID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankAccounts", "AccountID", c => c.String());
            DropColumn("dbo.BankAccounts", "BankAccountID");
            RenameIndex(table: "dbo.Transactions", name: "IX_BankAccountID", newName: "IX_AccountID");
            RenameColumn(table: "dbo.Transactions", name: "BankAccountID", newName: "AccountID");
            RenameTable(name: "dbo.BankAccounts", newName: "Accounts");
        }
    }
}
