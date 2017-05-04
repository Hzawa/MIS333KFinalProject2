namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "ReceivingBankAccountID", c => c.String(maxLength: 128));
            AddColumn("dbo.Disputes", "DisputeType", c => c.Int(nullable: false));
            AddColumn("dbo.Disputes", "CorrentAmount", c => c.Int(nullable: false));
            CreateIndex("dbo.Transactions", "ReceivingBankAccountID");
            AddForeignKey("dbo.Transactions", "ReceivingBankAccountID", "dbo.BankAccounts", "BankAccountID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ReceivingBankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.Transactions", new[] { "ReceivingBankAccountID" });
            DropColumn("dbo.Disputes", "CorrentAmount");
            DropColumn("dbo.Disputes", "DisputeType");
            DropColumn("dbo.Transactions", "ReceivingBankAccountID");
        }
    }
}
