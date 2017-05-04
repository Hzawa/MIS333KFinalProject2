namespace FinalGroupProjectTeam8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quickmig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Disputes", "CorrectAmount", c => c.Int(nullable: false));
            AddColumn("dbo.Disputes", "RequestDeletion", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Disputes", "Comments", c => c.String(nullable: false));
            DropColumn("dbo.Disputes", "CorrentAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Disputes", "CorrentAmount", c => c.Int(nullable: false));
            AlterColumn("dbo.Disputes", "Comments", c => c.String());
            DropColumn("dbo.Disputes", "RequestDeletion");
            DropColumn("dbo.Disputes", "CorrectAmount");
        }
    }
}
