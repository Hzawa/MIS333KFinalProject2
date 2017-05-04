namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "ReceivingBankAccountID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Transactions", "ReceivingBankAccountID");
            AddForeignKey("dbo.Transactions", "ReceivingBankAccountID", "dbo.BankAccounts", "BankAccountID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ReceivingBankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.Transactions", new[] { "ReceivingBankAccountID" });
            DropColumn("dbo.Transactions", "ReceivingBankAccountID");
        }
    }
}
