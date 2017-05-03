namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mostRecent2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BankAccounts", "BankAccountID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BankAccounts", "BankAccountID", c => c.String());
        }
    }
}
