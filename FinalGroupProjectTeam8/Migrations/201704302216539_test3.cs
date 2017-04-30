namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BankAccounts", name: "CustomerID", newName: "UserID");
            RenameIndex(table: "dbo.BankAccounts", name: "IX_CustomerID", newName: "IX_UserID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BankAccounts", name: "IX_UserID", newName: "IX_CustomerID");
            RenameColumn(table: "dbo.BankAccounts", name: "UserID", newName: "CustomerID");
        }
    }
}
