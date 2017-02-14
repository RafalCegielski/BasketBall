namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timeCharacter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "isBusy", c => c.Boolean(nullable: false));
            AddColumn("dbo.Characters", "busyEndTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "busyEndTime");
            DropColumn("dbo.Characters", "isBusy");
        }
    }
}
