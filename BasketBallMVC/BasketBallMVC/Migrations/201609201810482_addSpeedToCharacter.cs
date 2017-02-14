namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSpeedToCharacter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "Speed", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "Speed");
        }
    }
}
