namespace BasketBallMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGameStyle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "gameStyle", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "gameStyle");
        }
    }
}
