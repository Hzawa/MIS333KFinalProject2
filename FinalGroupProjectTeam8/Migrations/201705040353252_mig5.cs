namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Disputes", "DisputeType", c => c.Int(nullable: false));
            AddColumn("dbo.Disputes", "CorrentAmount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Disputes", "CorrentAmount");
            DropColumn("dbo.Disputes", "DisputeType");
        }
    }
}
