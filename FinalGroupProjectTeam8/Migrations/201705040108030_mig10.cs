namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payees", "Name", c => c.String());
            AlterColumn("dbo.Payees", "Street", c => c.String());
            AlterColumn("dbo.Payees", "City", c => c.String());
            AlterColumn("dbo.Payees", "State", c => c.String());
            AlterColumn("dbo.Payees", "Zip", c => c.String());
            AlterColumn("dbo.Payees", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payees", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Payees", "Zip", c => c.String(nullable: false));
            AlterColumn("dbo.Payees", "State", c => c.String(nullable: false));
            AlterColumn("dbo.Payees", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Payees", "Street", c => c.String(nullable: false));
            AlterColumn("dbo.Payees", "Name", c => c.String(nullable: false));
        }
    }
}
