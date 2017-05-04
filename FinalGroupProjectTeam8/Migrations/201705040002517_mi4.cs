namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mi4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "TransactionStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "TransactionStatus");
        }
    }
}
