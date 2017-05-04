namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mostRecent4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "ReceivingBankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.Transactions", new[] { "ReceivingBankAccountID" });
            DropColumn("dbo.Transactions", "ReceivingBankAccountID");
            DropColumn("dbo.Disputes", "DisputeType");
            DropColumn("dbo.Disputes", "CorrentAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Disputes", "CorrentAmount", c => c.Int(nullable: false));
            AddColumn("dbo.Disputes", "DisputeType", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "ReceivingBankAccountID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Transactions", "ReceivingBankAccountID");
            AddForeignKey("dbo.Transactions", "ReceivingBankAccountID", "dbo.BankAccounts", "BankAccountID");
        }
    }
}
